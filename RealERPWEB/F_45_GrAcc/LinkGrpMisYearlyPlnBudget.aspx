
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkGrpMisYearlyPlnBudget.aspx.cs" Inherits="RealERPWEB.F_45_GrAcc.LinkGrpMisYearlyPlnBudget" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style18
        {
            width: 53px;
        }
        
        .style52
        {
            width: 72px;
        }
        .style53
        {
            width: 467px;
        }
        .style54
        {
            width: 170px;
        }
        
        .style17
        {
            color: #FFFFFF;
        }
        .style55
        {
            width: 57px;
        }
        .style56
        {
            width: 61px;
        }
        .style57
        {
            width: 269px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            var gv1 = $('#<%=this.gvySalbgd.ClientID %>');

            gv1.Scrollable();
        }

    </script>
    <table style="width: 61%;">
        <tr>
            <td class="style12" width="500">
                <asp:Label ID="lblHeader" runat="server" Font-Bold="True" Font-Size="18px" ForeColor="Yellow"
                    Text="YEARLY SALES & EXPENDITURE STATUS" Width="686px" Style="border-bottom: 1px solid white;
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
                <asp:LinkButton ID="lbtnPrint" runat="server" BackColor="#000066" BorderColor="White"
                    BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="White"
                    OnClick="lbtnPrint_Click" Style="text-align: center; height: 17px;" Width="60px">PRINT</asp:LinkButton>
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
                        <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style55">
                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="12px" Height="16px"
                                            Style="text-align: LEFT;" Text="Year:" Width="60px" CssClass="style17"></asp:Label>
                                    </td>
                                    <td class="style55">
                                        <asp:DropDownList ID="ddlyear" runat="server" AutoPostBack="True" Font-Bold="True"
                                            Font-Size="12px" TabIndex="11" Width="100px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style56">
                                        <asp:LinkButton ID="lbtnYearbgd" runat="server" BackColor="#000066" BorderColor="White"
                                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            OnClick="lbtnYearbgd_Click" Style="text-align: center; height: 17px;" Width="60px">Ok</asp:LinkButton>
                                    </td>
                                    <td align="left" class="style64">
                                        &nbsp;
                                    </td>
                                    <td align="left" class="style64">
                                        &nbsp;
                                    </td>
                                    <td align="left" class="style66">
                                        &nbsp;
                                    </td>
                                    <td class="style4">
                                        &nbsp;
                                    </td>
                                    <td class="style69">
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
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="style57">
                                        <asp:Label ID="lblmsg02" runat="server" BackColor="Red" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White"></asp:Label>
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
                                <tr>
                                    <td class="style55">
                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" Height="16px"
                                            Style="color: #FFFFFF; text-align: right;" Text="Page Size:" Width="60px"></asp:Label>
                                    </td>
                                    <td class="style55">
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" BackColor="#CCFFCC"
                                            Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                            Style="margin-left: 0px" TabIndex="2" Width="100px">
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
                                    <td class="style56">
                                        &nbsp;
                                    </td>
                                    <td align="left" class="style64">
                                        &nbsp;
                                    </td>
                                    <td align="left" class="style64">
                                        &nbsp;
                                    </td>
                                    <td align="left" class="style66">
                                        &nbsp;
                                    </td>
                                    <td class="style4">
                                        &nbsp;
                                    </td>
                                    <td class="style69">
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
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="style57">
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
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="12">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="ViewYearly" runat="server">
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="12">
                                            <asp:GridView ID="gvySalbgd" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                                Width="531px" OnRowDataBound="gvySalbgd_RowDataBound">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="serialnoidy" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                                Width="30px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lbYearbgdTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" OnClick="lbYearbgdTotal_Click" Style="text-decoration: none;"> Total </asp:LinkButton></FooterTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDesc" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: left" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "grpdesc1").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                        "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc1")).Trim(): "") + "</B>"  + 
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                          (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc1")).Trim().Length>0 ?   "<br>" :"") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "")  %>'
                                                                Width="230px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lbtnYBgdUpdate" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" OnClick="lbtnYBgdUpdate_Click" Style="text-decoration: none;"> Update </asp:LinkButton></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblunit" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                                Width="50px"></asp:Label></ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvRate" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="50px"></asp:Label></ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvTQty" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label></ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Jan">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFQty1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="50px"></asp:Label></FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvqty1" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty1")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:TextBox></ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Feb">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvqty2" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty2")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:TextBox></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFqty2" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="50px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Mar">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvqty3" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty3")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:TextBox></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFqty3" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="50px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Apr">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvqty4" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty4")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:TextBox></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFqty4" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="50px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="May">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvqty5" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty5")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:TextBox></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFqty5" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="50px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Jun">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvqty6" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty6")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:TextBox></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFqty6" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="50px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Jul">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvqty7" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty7")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:TextBox></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFqty7" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="50px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Aug">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvqty8" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty8")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:TextBox></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFqty8" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="50px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sep">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvqty9" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty9")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:TextBox></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFqty9" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="50px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Oct">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvqty10" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty10")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:TextBox></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFqty10" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="50px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nov">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvqty11" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty11")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:TextBox></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFqty11" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="50px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Dec">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvqty12" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty12")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:TextBox></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFqty12" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="50px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#333333" />
                                                <PagerStyle ForeColor="White" HorizontalAlign="Left" />
                                                <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                                                    Height="20px" HorizontalAlign="Center" />
                                                <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                            </asp:GridView>
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
                            </asp:View>
                            <asp:View ID="YearlyBgdAmtBasis" runat="server">
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="12">
                                            <asp:GridView ID="gvBgdAmt" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                                Width="531px" OnPageIndexChanging="gvBgdAmt_PageIndexChanging" OnRowDataBound="gvBgdAmt_RowDataBound">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="serialnoidy" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                                Width="20px"></asp:Label></ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDesc" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: left" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "")  %>'
                                                                Width="230px"></asp:Label></ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblunit" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                                Width="50px"></asp:Label></ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvRate" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="50px"></asp:Label></ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvTAmt" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label></ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Jan">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvAmt1" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt1")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label></ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Feb">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvamt2" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt2")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFamt2" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Mar">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvamt3" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt3")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFamt3" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Apr">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvamt4" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt4")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFamt4" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="May">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvamt5" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt5")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFamt5" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Jun">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvamt6" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt6")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFamt6" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Jul">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvamt7" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt7")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFamt7" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Aug">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvamt8" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt8")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFamt8" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sep">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvamt9" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt9")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFamt9" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Oct">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvamt10" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt10")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFamt10" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nov">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvamt11" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt11")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFamt11" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Dec">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvamt12" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt12")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFamt12" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#333333" />
                                                <PagerStyle ForeColor="White" HorizontalAlign="Left" />
                                                <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                                                    Height="20px" HorizontalAlign="Center" />
                                                <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                            </asp:GridView>
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
                            </asp:View>
                            <asp:View ID="YearlyBgdQtyBasis" runat="server">
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="12">
                                            <asp:GridView ID="gvBgdQty" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                                Width="531px" OnPageIndexChanging="gvBgdQty_PageIndexChanging" OnRowDataBound="gvBgdQty_RowDataBound">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="serialnoidy" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                                Width="20px"></asp:Label></ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDesc" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: left" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "")  %>'
                                                                Width="230px"></asp:Label></ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblunit" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                                Width="50px"></asp:Label></ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvRate" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="50px"></asp:Label></ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvTQty" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label></ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Jan">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFQty1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="50px"></asp:Label></FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvqty1" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty1")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:Label></ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Feb">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvqty2" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty2")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFqty2" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="50px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Mar">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvqty3" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty3")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFqty3" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="50px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Apr">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvqty4" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty4")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFqty4" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="50px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="May">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvqty5" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty5")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFqty5" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="50px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Jun">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvqty6" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty6")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFqty6" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="50px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Jul">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvqty7" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty7")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFqty7" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="50px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Aug">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvqty8" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty8")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFqty8" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="50px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sep">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvqty9" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty9")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFqty9" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="50px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Oct">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvqty10" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty10")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFqty10" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="50px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nov">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvqty11" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty11")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFqty11" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="50px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Dec">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvqty12" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty12")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFqty12" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="50px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#333333" />
                                                <PagerStyle ForeColor="White" HorizontalAlign="Left" />
                                                <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                                                    Height="20px" HorizontalAlign="Center" />
                                                <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                            </asp:GridView>
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
                            </asp:View>
                            <asp:View ID="ViewYearlyIncome" runat="server">
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="12">
                                            <asp:GridView ID="grvYearltIncome" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                                Width="531px" onpageindexchanging="grvYearltIncome_PageIndexChanging" 
                                                onrowdatabound="grvYearltIncome_RowDataBound" >
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="serialnoidy" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                                Width="20px"></asp:Label></ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDesc" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: left" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "")  %>'
                                                                Width="230px"></asp:Label></ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblunit" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                                Width="50px"></asp:Label></ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                   
                                                    <asp:TemplateField HeaderText="Total Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvTAmt" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label></ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Jan">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvAmt1" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt1")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label></ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Feb">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvamt2" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt2")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFamt2" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Mar">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvamt3" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt3")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFamt3" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Apr">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvamt4" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt4")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFamt4" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="May">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvamt5" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt5")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFamt5" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Jun">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvamt6" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt6")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFamt6" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Jul">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvamt7" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt7")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFamt7" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Aug">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvamt8" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt8")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFamt8" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sep">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvamt9" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt9")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFamt9" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Oct">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvamt10" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt10")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFamt10" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nov">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvamt11" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt11")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFamt11" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Dec">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvamt12" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt12")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFamt12" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#333333" />
                                                <PagerStyle ForeColor="White" HorizontalAlign="Left" />
                                                <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                                                    Height="20px" HorizontalAlign="Center" />
                                                <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                            </asp:GridView>
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
                            </asp:View>
                        </asp:MultiView>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td class="style54">
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
