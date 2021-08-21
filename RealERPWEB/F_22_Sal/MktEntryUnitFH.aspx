<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="MktEntryUnitFH.aspx.cs" Inherits="RealERPWEB.F_22_Sal.MktEntryUnitFH" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style12
        {
            width: 794px;
        }
        .style28
        {
            width: 21px;
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
        .style61
        {
            width: 889px;
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
       
        .style91
        {
            width: 66px;
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
           var gv = $('#<%=this.gvUnit.ClientID %>');
           gv.Scrollable();


           $("input, select").bind("keydown", function (event) {
               var k1 = new KeyPress();
               k1.textBoxHandler(event);

           });
           var gridview = $('#<%=this.gvUnit.ClientID %>');
           $.keynavigation(gridview);
       }
   </script>
    
    

    

    <table style="width: 61%;">
        <tr>
            <td class="style12" width="500">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="FINISHING PROJECT UNIT FIXATION INFORMATION VIEW/EDIT" Width="686px"
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
                                    <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px">
                                        <table style="width:100%;">
                                            <tr>
                                                <td>
                                                    </td>
                                                <td class="style91">
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Project Name:" 
                                                        CssClass="style29" Width="80px" Font-Size="12px" ForeColor="White"></asp:Label>
                                                </td>
                                                <td class="style28">
                                                    </td>
                                                <td class="style61">
                                                    <asp:Label ID="lblProjectmDesc" runat="server" BackColor="White" 
                                                        Font-Size="12px" ForeColor="Blue" Visible="False" Width="350px"></asp:Label>
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
                                                <td>
                                                    </td>
                                                <td>
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                                <td class="style91">
                                                    <asp:TextBox ID="txtSrcPro" runat="server" CssClass="txtboxformat" 
                                                        Width="80px" BorderStyle="None"></asp:TextBox>
                                                </td>
                                                <td class="style28">
                                                    <asp:ImageButton ID="ibtnFindProject" runat="server" Height="18px" 
                                                        ImageUrl="~/Image/find_images.jpg" onclick="ibtnFindProject_Click" 
                                                        TabIndex="1" />
                                                </td>
                                                <td valign="top" class="style61">
                                                    <asp:DropDownList ID="ddlProjectName" runat="server" 
                                                        Font-Bold="True" Font-Size="12px" Width="350px" TabIndex="2">
                                                    </asp:DropDownList>
                                                 
                                               
                                                    <asp:Label ID="lblProjectdesc" runat="server" BackColor="White" 
                                                        Font-Size="12px" ForeColor="Blue" Height="16px" Visible="False" 
                                                        Width="350px"></asp:Label>

                                                           <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" 
                                                        BorderColor="White" BorderStyle="Solid" BorderWidth="1px" 
                                                        Font-Bold="True" Font-Size="12px" onclick="lbtnOk_Click" ForeColor="White" 
                                                        Height="16px" style="text-align: center" Width="32px"
                                                      >Ok</asp:LinkButton>
                                                    
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
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="11">
                                    <asp:Panel ID="PanelGroup" runat="server" BorderColor="Yellow" 
                                        BorderStyle="Solid" BorderWidth="1px" Visible="False">
                                        <table style="width:100%;">
                                           
                                            <tr>
                                                <td class="style92">
                                                    &nbsp;</td>
                                                <td class="style87">
                                                    <asp:CheckBox ID="chkAllSInf" runat="server" AutoPostBack="True" 
                                                        Font-Bold="True" Font-Size="12px" ForeColor="#660033" Height="16px" 
                                                        oncheckedchanged="chkAllSInf_CheckedChanged" style="color: #FFFFFF" 
                                                        Text="Show All" Width="80px" TabIndex="7" />
                                                </td>
                                                <td class="style88">
                                                    &nbsp;</td>
                                                <td class="style89">
                                                    <asp:Label ID="Label7" runat="server" CssClass="style29" Font-Bold="True" 
                                                        Font-Size="12px" ForeColor="White" Text="Res Desc." Width="60px"></asp:Label>
                                                </td>
                                                <td class="style93">
                                                    <asp:TextBox ID="txtResDesc" runat="server" BorderStyle="None" 
                                                        CssClass="txtboxformat" TabIndex="8" Width="80px"></asp:TextBox>
                                                </td>
                                                <td class="style99">
                                                    <asp:ImageButton ID="ibtnResDesc" runat="server" Height="18px" 
                                                        ImageUrl="~/Image/find_images.jpg" onclick="ibtnResDesc_Click" TabIndex="9" />
                                                </td>
                                                <td class="style90">
                                                    <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        style="color: #FFFFFF; text-align: right; " Text="Page Size:" Width="80px"></asp:Label>
                                                </td>
                                                <td class="style95">
                                                    <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                                        BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                                        onselectedindexchanged="ddlpagesize_SelectedIndexChanged" TabIndex="10" 
                                                        Width="100px">
                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                        <asp:ListItem Value="15">15</asp:ListItem>
                                                        <asp:ListItem Value="20">20</asp:ListItem>
                                                        <asp:ListItem Value="30">30</asp:ListItem>
                                                        <asp:ListItem Value="50">50</asp:ListItem>
                                                        <asp:ListItem Value="100">100</asp:ListItem>
                                                        <asp:ListItem Value="150">150</asp:ListItem>
                                                        <asp:ListItem Value="200">200</asp:ListItem>
                                                        <asp:ListItem Value="300">300</asp:ListItem>
                                                        <asp:ListItem Value="600">600</asp:ListItem>
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
                                    <asp:GridView ID="gvUnit" runat="server" AllowPaging="True" 
                                        AutoGenerateColumns="False" onpageindexchanging="gvUnit_PageIndexChanging" 
                                        onrowdatabound="gvUnit_RowDataBound" onrowdeleting="gvUnit_RowDeleting" 
                                     ShowFooter="True">
                                        <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
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
                                                    <asp:Label ID="lblgvItmCod" runat="server" Font-Size="11px" Height="16px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode")) %>' 
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description of Item">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lFinalUpdate" runat="server" Font-Bold="True" 
                                                        Font-Size="12px" ForeColor="White" onclick="lFinalUpdate_Click">Final Update</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtItemdesc" runat="server" AutoCompleteType="Disabled" 
                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>' 
                                                        Width="370px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                       
                                       <asp:TemplateField HeaderText="Unit ">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgUnitnum" runat="server" AutoCompleteType="Disabled" 
                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "munit")) %>' 
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
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "urate")).ToString("#,##0.00;(#,##0.00); ") %>' 
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
                                           
                                                   
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvRemarks" runat="server" BackColor="Transparent" 
                                                        BorderStyle="None" Font-Size="11px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "urmrks")) %>' 
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
                                    <cc1:ListSearchExtender ID="ListSearchExtender1" runat="server" 
                                        QueryPattern="Contains" TargetControlID="ddlProjectName">
                                    </cc1:ListSearchExtender>
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

