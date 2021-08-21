<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ProPriceTrading.aspx.cs" Inherits="RealERPWEB.F_22_Sal.ProPriceTrading" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style12
        {
            width: 794px;
        }
                
        
        .style54
        {
            height: 1px;
        }
        .style55
        {
            width: 50px;
            height: 1px;
        }
        .style57
        {
            width: 268px;
            height: 1px;
        }
        .style58
        {
            width: 210px;
            height: 1px;
        }
        .style75
        {
            height: 1px;
            width: 112px;
        }
               
        .style87
        {
            width: 31px;
        }
        .style88
        {
            width: 12px;
        }
        .style89
        {
            width: 160px;
        }
        .style90
        {
            width: 25px;
        }
       
        .style92
        {
            width: 7px;
        }
       
        .style93
        {
            width: 95px;
        }
        .style95
        {
            width: 537px;
        }
       
        .style29
        {}
        .style97
        {
            width: 319px;
        }
        .style98
        {
            width: 1020px;
        }
       
        .style99
        {
            width: 81px;
        }
       
    </style>
    
    <link href="CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    
    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



     <script language="javascript" type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
  <script language="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>
  <script src="../Scripts/jquery.keynavigation.js" type="text/javascript"></script>
     <script type="text/javascript" language="javascript" src="../Scripts/KeyPress.js"></script>
   <script language="javascript" type="text/javascript">

       $(document).ready(function () {
           Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

       });
       function pageLoaded() {
           var gv = $('#<%=this.gvTrading.ClientID %>');
           gv.Scrollable();


           $("input, select").bind("keydown", function (event) {
               var k1 = new KeyPress();
               k1.textBoxHandler(event);

           });
           var gridview = $('#<%=this.gvTrading.ClientID %>');
           $.keynavigation(gridview);
       }
   </script>
    
    

    

    <table style="width: 61%;">
        <tr>
            <td class="style12" width="500">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="Product Pricing (Trading)" Width="686px"
                   STYLE="border-bottom:1px solid white;border-top:1px solid white;" ></asp:Label>
            </td>
            <td>    
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
                    onclick="lbtnPrint_Click" style="color: #FFFFFF" CssClass="button">PRINT</asp:LinkButton>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        </table>
        
                
                
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table style="width:100%;">
                           
                            <tr>
                                <td colspan="11">
                                    <asp:Panel ID="PanelGroup" runat="server" BorderColor="Yellow" 
                                        BorderStyle="Solid" BorderWidth="1px">
                                        <table style="width:100%;">
                                          
                                            <tr>
                                                <td class="style92">
                                                    &nbsp;</td>
                                                <td class="style87">
                                                    &nbsp;</td>
                                                <td class="style88">
                                                    &nbsp;</td>
                                                <td class="style89">
                                                    <asp:Label ID="Label7" runat="server" CssClass="style29" Font-Bold="True" 
                                                        Font-Size="12px" ForeColor="White" Text="Project Name:" Width="100px"></asp:Label>
                                                </td>
                                                <td class="style93">
                                                    <asp:TextBox ID="txtProCode" runat="server" BorderStyle="None" 
                                                        CssClass="txtboxformat" TabIndex="8" Width="80px"></asp:TextBox>
                                                </td>
                                                <td class="style99">
                                                    <asp:ImageButton ID="ibtnOk" runat="server" Height="18px" 
                                                        ImageUrl="~/Image/find_images.jpg" onclick="ibtnOk_Click" TabIndex="9" />
                                                </td>
                                                <td class="style90">
                                                    <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        style="color: #FFFFFF; text-align: right; " Text="Page Size:" Width="80px"></asp:Label>
                                                </td>
                                                <td class="style95">
                                                    <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                                        BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                                        onselectedindexchanged="ddlpagesize_SelectedIndexChanged" TabIndex="10">
                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                        <asp:ListItem Value="15">15</asp:ListItem>
                                                        <asp:ListItem Value="20">20</asp:ListItem>
                                                        <asp:ListItem Value="30">30</asp:ListItem>
                                                        <asp:ListItem Value="50">50</asp:ListItem>
                                                        <asp:ListItem Value="100">100</asp:ListItem>
                                                        <asp:ListItem Value="150">150</asp:ListItem>
                                                        <asp:ListItem Value="200">200</asp:ListItem>
                                                        <asp:ListItem Value="300">300</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="style95">
                                                    &nbsp;</td>
                                                <td class="style97">
                                                    &nbsp;</td>
                                                <td class="style98">
                                                    <asp:Label ID="lmsg" runat="server" BackColor="Red" Font-Bold="True" 
                                                        Font-Size="12px" ForeColor="Maroon" style="color: #FFFFFF"></asp:Label>
                                                </td>
                                                <td class="style95">
                                                    &nbsp;</td>
                                                <td class="style95">
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
                                <td colspan="11">
                                    <asp:GridView ID="gvTrading" runat="server" AllowPaging="True" 
                                        AutoGenerateColumns="False" 
                                        onpageindexchanging="gvTrading_PageIndexChanging" onrowdeleting="gvTrading_RowDeleting" 
                                     ShowFooter="True">
                                        <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:CommandField ShowDeleteButton="True" />
                                            <asp:TemplateField HeaderText="Code">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnTotal" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" onclick="lbtnTotal_Click">Total</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvpactCod" runat="server" Font-Size="11px" Height="16px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>' 
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Res Code" Visible="false">
                                               
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResCod" runat="server" Font-Size="11px" Height="16px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>' 
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Project Description">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lFinalUpdate" runat="server" Font-Bold="True" 
                                                        Font-Size="12px" ForeColor="White" onclick="lFinalUpdate_Click">Final Update</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="txtPactdesc" runat="server" AutoCompleteType="Disabled" 
                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>' 
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Description">
                                               
                                                <ItemTemplate>
                                                    <asp:Label ID="txtUnitdesc" runat="server" AutoCompleteType="Disabled" 
                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>' 
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit ">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgUnit" runat="server" AutoCompleteType="Disabled" 
                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>' 
                                                        Width="40px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Size">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvUSize" runat="server" BackColor="Transparent" 
                                                        BorderStyle="None" Font-Size="11px" Height="18px" style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                        Width="50px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lFUsize" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvRate" runat="server" BackColor="Transparent" 
                                                        BorderStyle="None" Font-Size="11px" Height="18px" style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                        Width="60px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvuamt" runat="server" Font-Size="11px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uamt")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Parking">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvPamt" runat="server" AutoCompleteType="Disabled" 
                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paramt")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="50px" ></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvPAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Utility">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvUtility" runat="server" AutoCompleteType="Disabled" 
                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utility")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="50px" ></asp:TextBox>
                                                </ItemTemplate>  
                                                 <FooterTemplate>
                                                    <asp:Label ID="lgvPUtility" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" style="text-align: right"></asp:Label>
                                                </FooterTemplate>                                             
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="BD">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvBD" runat="server" AutoCompleteType="Disabled" 
                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bd")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="70px" ></asp:TextBox>
                                                </ItemTemplate>  
                                                 <FooterTemplate>
                                                    <asp:Label ID="lgvFBD" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" style="text-align: right"></asp:Label>
                                                </FooterTemplate>                                           
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Commision">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvComm" runat="server" AutoCompleteType="Disabled" 
                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comm")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="70px" ></asp:TextBox>
                                                </ItemTemplate>  
                                                 <FooterTemplate>
                                                    <asp:Label ID="lgvFComm" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" style="text-align: right"></asp:Label>
                                                </FooterTemplate>                                           
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Others Cost">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvOthCost" runat="server" AutoCompleteType="Disabled" 
                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othamt")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="70px" ></asp:TextBox>
                                                </ItemTemplate>  
                                                 <FooterTemplate>
                                                    <asp:Label ID="lgvFOthCost" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" style="text-align: right"></asp:Label>
                                                </FooterTemplate>                                           
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvTamt" runat="server" Font-Size="11px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvTAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Storied">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvStrid" runat="server" AutoCompleteType="Disabled" 
                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Storied")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="50px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFStrid" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Floor No">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvFlr" runat="server" BackColor="Transparent" 
                                                        BorderStyle="None" Font-Size="11px" Height="18px" style="text-align: left" 
                                                       Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrno")) %>'
                                                        Width="70px" ></asp:TextBox>
                                                </ItemTemplate>
                                               
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Parking Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvPqty" runat="server" AutoCompleteType="Disabled" 
                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "parqty")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="60px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvfParkingqty" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Purchase Date">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvPurDat" runat="server" style="text-align: left; font-size:11px;" 
                                                    Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "purdat")).ToString("dd-MMM-yyyy")=="01-Jan-1900")?""
                                                        :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "purdat")).ToString("dd-MMM-yyyy")%>' 
                                                    Width="70px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtgvPurDat_CalendarExtender" runat="server" 
                                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvPurDat">
                                                </cc1:CalendarExtender>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Handover Date">
                                             <ItemTemplate>
                                                    <asp:TextBox ID="txtgvHanoDat" runat="server" BackColor="Transparent" 
                                                        BorderStyle="None" Font-Size="11px" Height="18px" style="text-align: left" 
                                                       Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "handodat")) %>'
                                                        Width="70px" ></asp:TextBox>
                                                </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Facing">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvUFacing" runat="server" BackColor="Transparent" 
                                                        BorderStyle="None" Font-Size="11px" Height="18px" style="text-align: left" 
                                                       Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "facing")) %>'
                                                        Width="70px" ></asp:TextBox>
                                                </ItemTemplate>
                                               
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Location">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvLoc" runat="server" BackColor="Transparent" 
                                                        BorderStyle="None" Font-Size="11px" Height="18px" style="text-align: left" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Location")) %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                               
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Developer Name">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvDevName" runat="server" BackColor="Transparent" 
                                                        BorderStyle="None" Font-Size="11px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "devname")) %>' 
                                                        Width="100px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Purchase Type">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvPType" runat="server" BackColor="Transparent" 
                                                        BorderStyle="None" Font-Size="11px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ptype")) %>' 
                                                        Width="100px"></asp:TextBox>
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
                                </td>
                            </tr>
                            <tr>
                                <td class="style54">
                                </td>
                                <td class="style55">
                                </td>
                                <td class="style75">
                                </td>
                                <td class="style57">
                                    
                                </td>
                                <td class="style54">
                                </td>
                                <td class="style54">
                                </td>
                                <td class="style58">
                                </td>
                                <td class="style54">
                                </td>
                                <td class="style54">
                                </td>
                                <td class="style54">
                                </td>
                                <td class="style54">
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
          
</asp:Content>

