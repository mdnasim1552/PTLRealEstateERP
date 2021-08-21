<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkGrpMisProjectStatus.aspx.cs" Inherits="RealERPWEB.F_45_GrAcc.LinkGrpMisProjectStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style40
        {
            color: #FFFFFF;
            text-align: center;
        }
        .txtboxformat
        {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            font-size: 11px;
            font-weight: normal;
            margin-right: 0px;
        }
        .style41
        {
            width: 58px;
        }
        .style43
        {
            width: 9px;
        }
        .style58
        {
            width: 81px;
        }
        
        .style60
        {
            height: 21px;
        }
        .style61
        {
            width: 452px;
        }
        .style62
        {
            width: 31px;
        }
        .style63
        {
            width: 125px;
        }
        .style68
        {
        }
        .style69
        {
            width: 37px;
        }
        .style70
        {
            width: 222px;
        }
        .style71
        {
            width: 350px;
        }
        .style72
        {
            width: 915px;
        }
    </style>
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            var grvPrjStatus = $('#<%=this.grvPrjStatus.ClientID %>');
            var gvMonPorStatus = $('#<%=this.gvMonPorStatus.ClientID %>');
            var grvPrjturnover = $('#<%=this.grvPrjturnover.ClientID %>');
            var gvGPNPCal = $('#<%=this.gvGPNPCal.ClientID %>');
            grvPrjStatus.Scrollable();
            gvMonPorStatus.Scrollable();
            grvPrjturnover.Scrollable();
            gvGPNPCal.Scrollable();

        }

    </script>
    <table style="width: 99%;">
        <tr>
            <td class="style61">
                <asp:Label ID="lblHeader" runat="server" Font-Bold="True" Font-Size="18px" ForeColor="Yellow"
                    Text="Project Status Report" Width="500px" Style="border-bottom: 1px solid WHITE;
                    border-top: 1px solid WHITE;"></asp:Label>
            </td>
            <td class="style62">
                <asp:Label ID="lbljavascript" runat="server"></asp:Label>
            </td>
            <td class="style63">
                <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" Style="font-size: 11px"
                    Width="130px">
                    <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                    <asp:ListItem Value="HTML">HTML</asp:ListItem>
                    <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                    <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style98">
                <asp:LinkButton ID="lbtnPrint" runat="server" Font-Bold="True" OnClick="lbtnPrint_Click"
                    Style="color: #FFFFFF" CssClass="button">PRINT</asp:LinkButton>
            </td>
        </tr>
    </table>
    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                    <td colspan="12">
                        <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Width="1000px">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style41">
                                        <asp:Label ID="lblfrmDate" runat="server" CssClass="style40" Font-Bold="True" Style="text-align: right"
                                            Text="Date:" Width="100px" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="style68" colspan="3">
                                        <asp:Label ID="lblAsDate" runat="server" BackColor="#000066" 
                                            BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="Yellow" Text="A. Sales" Width="250px"></asp:Label>
                                    </td>
                                    <td class="style69">
                                        <asp:LinkButton ID="lbtnShow" runat="server" BackColor="#003366" BorderColor="White"
                                            BorderStyle="Solid" BorderWidth="1px" CssClass="style40" Font-Bold="True" Font-Size="12px"
                                            OnClick="lbtnShow_Click" Style="text-decoration: none; height: 17px;" Width="44px">Show</asp:LinkButton>
                                    </td>
                                    <td class="style72">
                                        <asp:CheckBox ID="chkconsolidate" runat="server" BackColor="#000066" BorderColor="Yellow"
                                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                                            Text="Consolidate" Visible="False" Width="90px" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="style71">
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
                                    <td class="style41">
                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" Style="color: #FFFFFF;
                                            text-align: right;" Text="Page Size:" Width="100px"></asp:Label>
                                    </td>
                                    <td class="style70">
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" BackColor="#CCFFCC"
                                            Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                            Width="80px">
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
                                    <td class="style43">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="style69">
                                        &nbsp;
                                    </td>
                                    <td class="style72">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="style71">
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
            </table>
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="PrjReport" runat="server">
                    <table>
                        <tr>
                            <td colspan="12">
                                <asp:GridView ID="grvPrjStatus" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                    Style="margin-right: 0px" OnPageIndexChanging="grvPrjStatus_PageIndexChanging"
                                    OnRowDataBound="grvPrjStatus_RowDataBound">
                                    <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="serialno" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                    Width="30px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actcode" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblActcode" runat="server" Style="text-align: right" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                    Width="30px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Group Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgrpdesc" runat="server" Font-Size="11PX" Font-Bold="false" Font-Underline="false"
                                                    ForeColor="Black" Style="text-align: left" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>" %>'
                                                    Width="120px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgrpProjectdesc" runat="server" Font-Size="11PX" Font-Bold="false"
                                                    Font-Underline="false" ForeColor="Black" Style="text-align: left" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc1")) + "</B>" %>'
                                                    Width="120px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Description">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvDesc" runat="server" Font-Size="11PX" Target="_blank" Font-Bold="false"
                                                    Font-Underline="false" ForeColor="Black" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim()%>'
                                                    Width="150px"></asp:HyperLink></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Project Description">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvDesc" runat="server" Font-Size="11PX" Target="_blank" Font-Bold="false" Font-Underline="false" ForeColor="Black"
                                            style="text-align: left" 
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "pactdesc1").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                        "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc1")).Trim(): "")+"</B>"  + 
                                                                         (DataBinder.Eval(Container.DataItem, "pactdesc").ToString().Trim().Length>0 ? 
                                                                          (Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc1")).Trim().Length>0 ?   "<br>" :"") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim(): "")%>'
                                            Width="250px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                               </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Location">
                                            <HeaderTemplate>
                                                <table style="width: 47%;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Location" Width="80px"></asp:Label>
                                                        </td>
                                                        <td class="style60">
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtntbCdataExel" runat="server" BackColor="#000066" BorderColor="White"
                                                                BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="White" Style="text-align: center"
                                                                Width="90px">Export Exel</asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlocation" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prjloc")) %>'
                                                    Width="150px"></asp:Label></ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Land Area(Katha)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvLarea" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lanarea")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label></ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nature of Land" FooterText="Total: ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvNLand" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "natland")) %>'
                                                    Width="80px"></asp:Label></ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Sales Value">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTSVal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px">
                                                </asp:Label></FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvTSVal" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsalamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="This Month">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTmonSVal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvTmonSVal" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "msalamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Received against Sales">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTReSVal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvTReSVal" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trecamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Non-Operating Income">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFNOI" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvNOI" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "noiamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Received">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFRecamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRecamt" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Balance (Sale Value-Received against Sales)">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFBRecSalamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBRecSalamt" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balsalrec")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Construction + Admin + Selling Exp.">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFExpAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvExpAmt" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "texpamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Advance">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPAdvAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPAdvAmt" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tpadvamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Land Cost(Non-Refundable)">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFLCNFAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvLCNFAmt" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tlcamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Overhead">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFOvmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOvmt" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tovamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bank Interest">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFIAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvIAmt" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tbankinamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Actual Expenses">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtExp" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtExp" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tactamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Liabilities">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFLibAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvLibAmt" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tliamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Loan from Head Office">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFLframt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvLframt" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lfrmhoff")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Loan to Head Office">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFLtoamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvLtoamt" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ltohoff")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Refundable Loan">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFRLamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRLamt" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "treloanamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="serialnodup" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                    Width="30px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#333333" />
                                    <PagerSettings Position="Top" />
                                    <PagerStyle HorizontalAlign="Left" ForeColor="White" />
                                    <RowStyle VerticalAlign="Bottom" />
                                    <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                                        Height="20px" HorizontalAlign="Center" />
                                    <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                    <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="CollBreakDown" runat="server">
                    <table>
                        <tr>
                            <td colspan="12">
                                <asp:GridView ID="grvCollBrkDown" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                    Style="margin-right: 0px" AllowPaging="True" OnPageIndexChanging="grvCollBrkDown_PageIndexChanging"
                                    OnRowDataBound="grvCollBrkDown_RowDataBound">
                                    <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="serialno" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                    Width="30px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actcode" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="Actcode" runat="server" Style="text-align: right" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                    Width="30px">
                                                </asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actdesc" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="ActDesc" runat="server" Style="text-align: right" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc1")) %>'
                                                    Width="30px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Description" FooterText="Total: ">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvDesc" runat="server" Font-Size="11PX" Font-Underline="false"
                                                    ForeColor="Black" Target="_blank" Style="text-align: left" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "pactdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+ 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim(): "")  %>'
                                                    Width="250px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Sales Value">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTSVal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvTSVal" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsalamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cleared Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtclramt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtclramt" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tclramt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Returned Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtretamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtretamt" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "retcheque")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fresh & Deposit Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtframt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtframt" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcheque")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Post Dated Cheque">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtpdamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtpdamt" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pcheque")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Received">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtmat" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtmat" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tmat")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Non-Operating Income">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFnoiamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvnoiamt" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "noiamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="STD Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFstdamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvstdamt" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stdamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cancel Unit Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFcuamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcuamt" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cuamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFgtotal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgtotal" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gtotal")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#333333" />
                                    <PagerSettings Position="Top" />
                                    <PagerStyle HorizontalAlign="Left" ForeColor="White" />
                                    <RowStyle VerticalAlign="Bottom" />
                                    <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                                        Height="20px" HorizontalAlign="Center" />
                                    <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                    <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="ViewMonProStatus" runat="server">
                    <asp:GridView ID="gvMonPorStatus" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                        Width="616px">
                        <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.">
                                <ItemTemplate>
                                    <asp:Label ID="serialno" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                        Width="25px"></asp:Label></ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField FooterText="Total" HeaderText=" Description">
                                <HeaderTemplate>
                                    <table style="width: 150px">
                                        <tr>
                                            <td class="style58">
                                                <asp:Label ID="Label5" runat="server" Font-Bold="True" Height="16px" Text="Description"
                                                    Width="70px"></asp:Label>
                                            </td>
                                            <td class="style60">
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:HyperLink ID="hlbtnCdataExel" runat="server" BackColor="#000066" BorderColor="White"
                                                    BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="White" Style="text-align: center"
                                                    Width="80px">Export Exel</asp:HyperLink>
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvActDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                        Width="180px"></asp:Label></ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="R1">
                                <FooterTemplate>
                                    <asp:Label ID="lgvFR1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                        Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvR1" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r1")).ToString("#,##0;(#,##0); ")%>'
                                        Width="65px"></asp:Label></ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="R2">
                                <ItemTemplate>
                                    <asp:Label ID="lgvR2" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r2")).ToString("#,##0;(#,##0); ")%>'
                                        Width="65px"></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFR2" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                        Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="R3">
                                <ItemTemplate>
                                    <asp:Label ID="lgvR3" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r3")).ToString("#,##0;(#,##0); ") %>'
                                        Width="65px"></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFR3" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                        Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="R4">
                                <ItemTemplate>
                                    <asp:Label ID="lgvR4" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r4")).ToString("#,##0;(#,##0); ") %>'
                                        Width="65px"></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFR4" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                        Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="R5">
                                <ItemTemplate>
                                    <asp:Label ID="lgvR5" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r5")).ToString("#,##0;(#,##0); ") %>'
                                        Width="65px"></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFR5" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                        Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="R6">
                                <ItemTemplate>
                                    <asp:Label ID="lgvR6" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r6")).ToString("#,##0;(#,##0); ") %>'
                                        Width="65px"></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFR6" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                        Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="R7">
                                <ItemTemplate>
                                    <asp:Label ID="lgvR7" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r7")).ToString("#,##0;(#,##0); ") %>'
                                        Width="65px"></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFR7" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                        Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="R8">
                                <ItemTemplate>
                                    <asp:Label ID="lgvR8" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r8")).ToString("#,##0;(#,##0); ") %>'
                                        Width="65px"></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFR8" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                        Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="R9">
                                <ItemTemplate>
                                    <asp:Label ID="lgvR9" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r9")).ToString("#,##0;(#,##0); ")%>'
                                        Width="65px"></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFR9" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                        Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="R10">
                                <ItemTemplate>
                                    <asp:Label ID="lgvR10" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r10")).ToString("#,##0;(#,##0); ") %>'
                                        Width="65px"></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFR10" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                        Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="R11">
                                <ItemTemplate>
                                    <asp:Label ID="lgvR11" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r11")).ToString("#,##0;(#,##0); ")%>'
                                        Width="65px"></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFR11" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                        Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="R12">
                                <ItemTemplate>
                                    <asp:Label ID="lgvR12" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r12")).ToString("#,##0;(#,##0); ") %>'
                                        Width="65px"></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFR12" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                        Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="R13">
                                <ItemTemplate>
                                    <asp:Label ID="lgvR13" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r13")).ToString("#,##0;(#,##0); ")%>'
                                        Width="65px"></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFR13" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                        Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="R14">
                                <ItemTemplate>
                                    <asp:Label ID="lgvR14" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r14")).ToString("#,##0;(#,##0); ") %>'
                                        Width="65px"></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFR14" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                        Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="R15">
                                <ItemTemplate>
                                    <asp:Label ID="lgvR15" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r15")).ToString("#,##0;(#,##0); ") %>'
                                        Width="65px"></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFR15" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                        Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="R16">
                                <ItemTemplate>
                                    <asp:Label ID="lgvR16" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r16")).ToString("#,##0;(#,##0); ") %>'
                                        Width="65px"></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFR16" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                        Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="R17">
                                <ItemTemplate>
                                    <asp:Label ID="lgvR17" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r17")).ToString("#,##0;(#,##0); ") %>'
                                        Width="65px"></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFR17" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                        Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="R18">
                                <ItemTemplate>
                                    <asp:Label ID="lgvR18" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r18")).ToString("#,##0;(#,##0); ") %>'
                                        Width="65px"></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFR18" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                        Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="R19">
                                <ItemTemplate>
                                    <asp:Label ID="lgvR19" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r19")).ToString("#,##0;(#,##0); ")%>'
                                        Width="65px"></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFR19" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                        Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="R20">
                                <ItemTemplate>
                                    <asp:Label ID="lgvR20" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r20")).ToString("#,##0;(#,##0); ")%>'
                                        Width="65px"></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFR20" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                        Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Cost">
                                <FooterTemplate>
                                    <asp:Label ID="lgvFtoCost" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                        Style="text-align: right" Width="75px"></asp:Label></FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvtocost" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toramt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="75px"></asp:Label></ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Collection">
                                <FooterTemplate>
                                    <asp:Label ID="lgvFtoCollection" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="White" Style="text-align: right" Width="75px"></asp:Label></FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvtoCollection" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tocollamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="75px"></asp:Label></ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Net Position">
                                <FooterTemplate>
                                    <asp:Label ID="lgvFnetposition" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="White" Style="text-align: right" Width="75px"></asp:Label></FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvnetposition" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="75px"></asp:Label></ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" Description">
                                <ItemTemplate>
                                    <asp:Label ID="lgvActDescdup" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                        Width="150px"></asp:Label></ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lgvserial" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                        Width="30px"></asp:Label></ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#333333" />
                        <PagerSettings Position="Top" />
                        <PagerStyle ForeColor="White" HorizontalAlign="Left" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                            Height="20px" HorizontalAlign="Center" />
                        <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                        <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                    </asp:GridView>
                </asp:View>
                <asp:View ID="ViewTurnover" runat="server">
                    <asp:GridView ID="grvPrjturnover" runat="server" AutoGenerateColumns="False" OnRowDataBound="grvPrjturnover_RowDataBound"
                        ShowFooter="True" Style="margin-right: 0px">
                        <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="serialnotvr" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                        Width="30px"></asp:Label></ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:Label ID="lblgrpProjectdesctvr" runat="server" Font-Bold="false" Font-Size="11PX"
                                        Font-Underline="false" ForeColor="Black" Style="text-align: left" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc1")) + "</B>" %>'
                                        Width="120px"></asp:Label></ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Project Description">
                                <ItemTemplate>
                                    <asp:Label ID="lgvProjectdesctvr" runat="server" Font-Bold="false" Font-Size="11PX"
                                        Font-Underline="false" ForeColor="Black" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))  %>'
                                        Width="180px"></asp:Label></ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Orginal Revenue">
                                <FooterTemplate>
                                    <asp:Label ID="lgvFOrsalam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                        Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvOrsalam" runat="server" Font-Size="11PX" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "osalam")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label></ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Revised Revenue">
                                <FooterTemplate>
                                    <asp:Label ID="lgvFrsalam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                        Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvrsalam" runat="server" Font-Size="11PX" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rsalam")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label></ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="% Change">
                                <ItemTemplate>
                                    <asp:Label ID="lgvperchange" runat="server" Font-Size="11PX" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perchange")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="50px"></asp:Label></ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sold Amount">
                                <FooterTemplate>
                                    <asp:Label ID="lgvFsoldam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                        Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvsoldam" runat="server" Font-Size="11PX" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "soldam")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label></ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unsold Amount">
                                <FooterTemplate>
                                    <asp:Label ID="lgvFusoldam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                        Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvusoldam" runat="server" Font-Size="11PX" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usoldam")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label></ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Received against Sales">
                                <FooterTemplate>
                                    <asp:Label ID="lgvFreceivedam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                        Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvreceivedam" runat="server" Font-Size="11PX" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidam")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label></ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Receivable">
                                <FooterTemplate>
                                    <asp:Label ID="lgvFreceiable" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                        Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvreceiable" runat="server" Font-Size="11PX" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "receivable")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label></ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Available Fund">
                                <FooterTemplate>
                                    <asp:Label ID="lgvFavailable" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                        Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvavailable" runat="server" Font-Size="11PX" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "available")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label></ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#333333" />
                        <PagerSettings Position="Top" />
                        <PagerStyle ForeColor="White" HorizontalAlign="Left" />
                        <RowStyle VerticalAlign="Bottom" />
                        <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                            Height="20px" HorizontalAlign="Center" />
                        <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                        <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                    </asp:GridView>
                </asp:View>
                <asp:View ID="ViewGPNPCalculation" runat="server">
                    <asp:GridView ID="gvGPNPCal" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                        Style="margin-right: 0px" Width="396px" OnRowDataBound="gvGPNPCal_RowDataBound">
                        <PagerSettings Position="Top" />
                        <RowStyle BackColor="#D2FFF7" Font-Size="12px" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Height="16px" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Project Name <br/> A">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvPDesc" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "pactdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+ 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim(): "")  %>'
                                        Width="250px"></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Advanced Sales <br/> B">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvAdvSal" runat="server" BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "advsamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFAdvSal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"></asp:Label></FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sales <br/> C">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSales" runat="server" BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "samt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFSales" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"></asp:Label></FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Construction <br/> D">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvconstamt" runat="server" BorderStyle="None" Font-Size="12px"
                                        Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "constamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFconstamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"></asp:Label></FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Land <br/> E">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvlandamt" runat="server" BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "landamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFlandamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"></asp:Label></FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product Cost <br/> F=(D+E)">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvprodamt" runat="server" BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prodamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFprodamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"></asp:Label></FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Gross Profit <br/> G=(C-F)">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvgpamt" runat="server" BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gpamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFgpamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"></asp:Label></FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GP % on Cost <br/> H=(G/(F+M))">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvgpcostper" runat="server" BorderStyle="None" Font-Size="12px"
                                        Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gpcostper")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:Label></ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Budget GP in % <br/> I">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvgpper" runat="server" BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gpper")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:Label></ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Head office Overhead <br/> J">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvhovamt" runat="server" BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "hovamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFhovamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"></asp:Label></FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bank Interest <br/> K">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvtbankinamt" runat="server" BorderStyle="None" Font-Size="12px"
                                        Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tbankinamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFtbankinamt" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="White"></asp:Label></FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Overhead & Interest <br/> L=(J+K) ">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvtothamt" runat="server" BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tothamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFtothamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"></asp:Label></FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Cost <br/> M=(F+L)">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvTCost" runat="server" BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcostamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFTCost" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"></asp:Label></FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NP before NOI <br/> N=(G-L)">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvnpbnoi" runat="server" BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "npbnoi")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFnpbnoi" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"></asp:Label></FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NP % on Cost <br/> O=(N/(F+L))">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvnpcostper" runat="server" BorderStyle="None" Font-Size="12px"
                                        Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "npcostper")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label></ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Budget NP <br/> in %  <br/> P">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvnpper" runat="server" BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "npper")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label></ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NOI <br/> Q">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvnoiamt" runat="server" BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "noiamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFnoiamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"></asp:Label></FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NP % after NOI <br/> R=((N+Q)/(F+L))">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvnpnoiper" runat="server" BorderStyle="None" Font-Size="12px"
                                        Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "npnoiper")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label></ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Project Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvDesc" runat="server" BorderStyle="None" Font-Size="12px" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                        Width="180px"></asp:Label></ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle BackColor="#666633" HorizontalAlign="Left" />
                        <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                            Height="20px" HorizontalAlign="Center" />
                        <FooterStyle BackColor="#333300" ForeColor="#FFFFCC" />
                        <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                        <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                    </asp:GridView>
                </asp:View>
                <asp:View ID="ViewGPNPSummary" runat="server">
                    <asp:GridView ID="gvGpNpSum" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                        Style="margin-right: 0px" Width="396px" OnRowDataBound="gvGpNpSum_RowDataBound">
                        <PagerSettings Position="Top" />
                        <RowStyle BackColor="#D2FFF7" Font-Size="12px" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Height="16px" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvPDesc" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "pactdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+ 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim(): "")  %>'
                                        Width="250px"></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Upto">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvopamt" runat="server" BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="100px"></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFAdvSal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"></asp:Label></FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Current Period">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvcuramt" runat="server" BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="100px"></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFSales" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"></asp:Label></FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvtamt" runat="server" BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="100px"></asp:Label></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFconstamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"></asp:Label></FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle BackColor="#666633" HorizontalAlign="Left" />
                        <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                            Height="20px" HorizontalAlign="Center" />
                        <FooterStyle BackColor="#333300" ForeColor="#FFFFCC" />
                        <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                        <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                    </asp:GridView>
                </asp:View>
            </asp:MultiView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
