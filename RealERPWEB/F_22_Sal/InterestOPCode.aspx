<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="InterestOPCode.aspx.cs" Inherits="RealERPWEB.F_22_Sal.InterestOPCode" %>

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
        .style75
        {
            height: 1px;
            width: 112px;
        }
               
        .style91
        {
            width: 66px;
        }
       
        .style29
        {}
        .style92
        {
            width: 45px;
        }
       
        .style93
        {
            width: 429px;
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
  <%-- <script language="javascript" type="text/javascript">

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
    --%>
    

    

    <table style="width: 61%;">
        <tr>
            <td class="style12" width="500">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="Opening Interest Information" Width="686px"
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
                                    <asp:Panel ID="Panel1" runat="server">
                                        <table style="width:100%;">
                                          
                                            <tr>
                                                <td class="style92">
                                                    <asp:Label ID="Label5" runat="server" CssClass="style29" Font-Bold="True" 
                                                        Font-Size="12px" ForeColor="White" Text="Project Name:" Width="80px"></asp:Label>
                                                </td>
                                                <td class="style91">
                                                    <asp:TextBox ID="txtSrcPro" runat="server" CssClass="txtboxformat" 
                                                        Width="80px" BorderStyle="None"></asp:TextBox>
                                                </td>
                                                <td class="style28">
                                                    <asp:ImageButton ID="ibtnFindProject" runat="server" Height="18px" 
                                                        ImageUrl="~/Image/find_images.jpg" onclick="ibtnFindProject_Click" 
                                                        TabIndex="1" />
                                                </td>
                                                <td valign="top" class="style93">
                                                    <asp:DropDownList ID="ddlProjectName" runat="server" 
                                                        Font-Bold="True" Font-Size="12px" Width="350px" TabIndex="2">
                                                    </asp:DropDownList>
                                                 
                                               
                                                    <asp:Label ID="lblProjectdesc" runat="server" BackColor="White" 
                                                        Font-Size="12px" ForeColor="Blue" Height="16px" Visible="False" 
                                                        Width="350px"></asp:Label>

                                                           <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" 
                                                        BorderColor="White" BorderStyle="Solid" BorderWidth="1px" 
                                                        Font-Bold="True" Font-Size="12px" onclick="lbtnOk_Click" ForeColor="White"
                                                      >Ok</asp:LinkButton>
                                                    
                                                </td>
                                                <td>
                                                    <asp:Label ID="lmsg" runat="server" BackColor="Red" Font-Bold="True" 
                                                        Font-Size="12px" ForeColor="Maroon" style="color: #FFFFFF"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                           
                            <tr>
                                <td colspan="11">
                                    <asp:GridView ID="gvOPInt" runat="server" AllowPaging="True" 
                                        AutoGenerateColumns="False" 
                                        onpageindexchanging="gvOPInt_PageIndexChanging" onrowdeleting="gvOPInt_RowDeleting" 
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
                                                    <asp:LinkButton ID="lFinalUpdate" runat="server" Font-Bold="True" 
                                                        Font-Size="12px" ForeColor="White" onclick="lFinalUpdate_Click">Final Update</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCod" runat="server" Font-Size="11px" Height="16px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>' 
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description of Item">
                                                <FooterTemplate>
                                                    <asp:Label ID="lFTotal" runat="server" Font-Bold="True" Font-Size="12px" Text="Total: "
                                                        ForeColor="White" style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtIResdesc" runat="server" AutoCompleteType="Disabled" 
                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>' 
                                                        Width="180px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="Principal">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvPri" runat="server" BackColor="Transparent" 
                                                        BorderStyle="None" Font-Size="11px" Height="18px" style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "priamt")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lFPri" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            

                                            <asp:TemplateField HeaderText="Interest">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvIamt" runat="server" BackColor="Transparent" 
                                                        BorderStyle="None" Font-Size="11px" Height="18px" style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "intamt")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFIAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            

                                            <asp:TemplateField HeaderText="Overhead">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvOvAmt" runat="server" AutoCompleteType="Disabled" 
                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "oheadamt")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="80px" ></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFOAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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

