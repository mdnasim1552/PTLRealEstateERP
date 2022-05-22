
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptAccPDCStatus.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptAccPDCStatus" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style29
        {            width: 52px;
        }
        .style30
        {
            color: #FFFFFF;
        }
        .style27
        {
            width: 102px;
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
        .style60
        {
            width: 17px;
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

            var gv = $('#<%=this.gvPdcIsuDet.ClientID %>');
            var gv1 = $('#<%=this.gvPdcIsuSummary.ClientID %>');
            var gv2 = $('#<%=this.gvPdcIsuCostWise.ClientID %>');
            var gv3 = $('#<%=this.gvPDCDayWise.ClientID %>');

            gv.Scrollable();
            gv1.Scrollable();
            gv2.Scrollable();
            gv3.Scrollable();



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
            <table style="width:100%;">
                <tr>
                    <td colspan="12">
                        <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                            BorderWidth="1px">
                            <table style="width:100%;">
                                <tr>
                                    <td class="style29">
                                        <asp:Label ID="lblFdate" runat="server" CssClass="style30" Font-Bold="True" 
                                            Font-Size="12px" style="text-align: left" Text="As on Date:" Width="70px"></asp:Label>
                                    </td>
                                    <td class="style27">
                                        <asp:TextBox ID="txtfromdate" runat="server" BorderColor="#660033" 
                                            BorderStyle="Solid" BorderWidth="1px" CssClass="txtboxformat" Font-Bold="True" 
                                            Width="100px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="csefdate" runat="server" Format="dd-MMM-yyyy " 
                                            TargetControlID="txtfromdate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style65">
                                        &nbsp;</td>
                                    <td align="left" class="style64">
                                        &nbsp;</td>
                                    <td align="left" class="style64">
                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" 
                                            Height="16px" style="color: #FFFFFF; text-align: right;" Text="Page Size:" 
                                            Width="70px"></asp:Label>
                                    </td>
                                    <td align="left" class="style66">
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                            BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                            onselectedindexchanged="ddlpagesize_SelectedIndexChanged" 
                                            style="margin-left: 0px" TabIndex="2" Width="85px">
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
                                    <td class="style4">
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" Font-Underline="False" Height="16px" onclick="lbtnOk_Click" 
                                            style="color: #FFFFFF; text-align: center;" TabIndex="5" Width="29px">Ok</asp:LinkButton>
                                    </td>
                                    <td class="style69">
                                        <asp:RadioButtonList ID="rbtPayment" runat="server" BackColor="#003366" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" RepeatDirection="Horizontal" 
                                            Width="300px">
                                            <asp:ListItem>Day Wise</asp:ListItem>
                                            <asp:ListItem>Summary</asp:ListItem>
                                            <asp:ListItem>Details</asp:ListItem>                                            
                                            <asp:ListItem>Cost Wise</asp:ListItem>
                                        </asp:RadioButtonList>
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
                            <asp:View ID="ViewDayWisePDC" runat="server">
                                <asp:GridView ID="gvPDCDayWise" runat="server" 
                                    AutoGenerateColumns="False" 
                                    onpageindexchanging="gvPDCDayWise_PageIndexChanging" ShowFooter="True" 
                                    style="text-align: left" onrowdatabound="gvPDCDayWise_RowDataBound">
                                    <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" 
                                                    style="text-align: right" 
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bank Name">
                                                    <HeaderTemplate>
                                                        <table style="width:220px;">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Bank Name" 
                                                                        Width="100px"></asp:Label>
                                                                </td>
                                                                <td class="style60">
                                                                    <asp:HyperLink ID="hlbtnbtbCdataExel" runat="server" BackColor="#000066" 
                                                                        BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                                        ForeColor="White" style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvbankname" runat="server" Font-Bold="true" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc"))  %>' 
                                                            Width="220px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                          </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Issue #">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvvounum1" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>' 
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvPVDate" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat")) %>' 
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Acc. Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgactdesc" runat="server" 
                                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "")  %>' 
                                                            Width="200px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Issue Ref">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvisunum" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isunum")) %>' 
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cheque No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvchnono" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) %>' 
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cheque Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvchdat" runat="server" style="text-align: left" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequedat")) %>' 
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFTotal" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="White" style="text-align: right" Width="70px">Total:</asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvcramt" runat="server" style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cramt")).ToString("#,##0;(#,##0); ") %>' 
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFCrAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="White" style="text-align: right" Width="70px"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Party Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvParName" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>' 
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Bill No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvBill" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>' 
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                         
                                        
                                    </Columns>
                                    <FooterStyle BackColor="#333333" />
                                    <PagerStyle ForeColor="White" HorizontalAlign="Left" />
                                    <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                        ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                    <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                    <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="ViewDetails" runat="server">
                                <asp:GridView ID="gvPdcIsuDet" runat="server" AllowPaging="True" 
                                    AutoGenerateColumns="False" 
                                    onpageindexchanging="gvPdcIsuDet_PageIndexChanging" ShowFooter="True" 
                                    style="text-align: left" onrowdatabound="gvPdcIsuDet_RowDataBound">
                                    <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" 
                                                    style="text-align: right" 
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvResCod" runat="server" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>' 
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Accounts Head">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvResName" runat="server" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>' 
                                                    Width="300px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                        <asp:Label ID="lgv" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="White" style="text-align: right" Width="300px">Total</asp:Label>
                                           </FooterTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="Payment Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPayAmt" runat="server" style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                           
                                           <FooterTemplate>
                                                        <asp:Label ID="lgvFPayAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="White" style="text-align: right" Width="70px"></asp:Label>
                                           </FooterTemplate>

                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                       
                                         
                                         
                                        
                                    </Columns>
                                    <FooterStyle BackColor="#333333" />
                                    <PagerStyle ForeColor="White" HorizontalAlign="Left" />
                                    <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                        ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                    <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                    <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                </asp:GridView>
                            </asp:View>
                             <asp:View ID="ViewSummary" runat="server">
                                <asp:GridView ID="gvPdcIsuSummary" runat="server" AllowPaging="True" 
                                    AutoGenerateColumns="False" 
                                    onpageindexchanging="gvPdcIsuSummary_PageIndexChanging" ShowFooter="True" 
                                    style="text-align: left">
                                    <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" 
                                                    style="text-align: right" 
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Accounts Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvProCod" runat="server" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paycode")) %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Accounts Head">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvProName" runat="server" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydesc")) %>' 
                                                    Width="230px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                        <asp:Label ID="lgv" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="White" style="text-align: right" Width="230px">Total</asp:Label>
                                           </FooterTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="Payment Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPayAmt" runat="server" style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                           
                                           <FooterTemplate>
                                                        <asp:Label ID="lgvFPayAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="White" style="text-align: right" Width="70px"></asp:Label>
                                           </FooterTemplate>

                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                       
                                         
                                         
                                        
                                    </Columns>
                                    <FooterStyle BackColor="#333333" />
                                    <PagerStyle ForeColor="White" HorizontalAlign="Left" />
                                    <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                        ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                    <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                    <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                </asp:GridView>
                            </asp:View>
                              <asp:View ID="ViewPDCIsuCostWise" runat="server">
                                <asp:GridView ID="gvPdcIsuCostWise" runat="server" AutoGenerateColumns="False" 
                                OnRowDataBound="gvPdcIsuCostWise_RowDataBound" ShowFooter="True" Width="658px">
                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                          <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server"  Font-Bold="True" Height="16px" 
                                        Style="text-align: right" 
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="60px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvCode" runat="server" Height="16px" 
                                        Style="text-align: right" 
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode2")) %>' Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText=" Description">
                                <ItemTemplate>
                                    <asp:Label ID="lgcActDesc" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc2").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc2")).Trim(): "")  %>' 
                                                                            Width="350px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                          
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvAmt" runat="server" Style="text-align: right" 
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>' 
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                               <FooterTemplate>
                                    <asp:Label ID="lgvFTDrAmt" runat="server" ForeColor="White" Font-Size="12px" Style="text-align:right" ></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                        </Columns>
                                <FooterStyle BackColor="#333333" />
                                <PagerStyle HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                    ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                    </asp:GridView>
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


