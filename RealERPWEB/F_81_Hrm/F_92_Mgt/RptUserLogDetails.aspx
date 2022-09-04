
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptUserLogDetails.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_92_Mgt.RptUserLogDetails" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    

    
    <link href="CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        .style12
        {
            width: 111px;
        }
        .style18
        {
            color: #FFFFFF;
            text-align: right;
        }
        .style19
        {
            width: 105px;
        }
        .style21
        {
            width: 92px;
        }
        .style22
        {
            width: 214px;
        }
        .style51
        {
            height: 2px;
        }
        .style52
        {
            height: 2px;
            width: 64px;
        }
        .style54
        {
            width: 92px;
            height: 2px;
        }
        .style55
        {
            width: 105px;
            height: 2px;
        }
        .style59
        {
            width: 64px;
        }
        .style60
        {
        }
        </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript"  language="javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>
     <script language="javascript" type="text/javascript" src="../../Scripts/ScrollableGridPlugin.js"></script>
  
   <script language="javascript" type="text/javascript">

       $(document).ready(function () {
           Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

       });
       function pageLoaded() {
           var gv = $('#<%=this.gvLogType.ClientID %>');
           gv.Scrollable();
       }
   </script>
    

    <table style="width: 98%;">
        <tr>
            <td class="style12" width="500">
                <asp:Label ID="HeaderText" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="INPUT LIST" Width="600px"
                   STYLE="border-bottom:1px solid blue;border-top:1px solid blue;" ></asp:Label>
            </td>
            <td class="style22">
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
                    <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                                        BorderWidth="1px">

                                       
                                    
                     <table style="width:100%;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label15" runat="server" CssClass="style18" Font-Bold="True" 
                                                        Font-Size="12px" Text="From:" Width="80px"></asp:Label>
                                                    </td>
                                                <td class="style59">
                                                    <asp:TextBox ID="txtfromdate" runat="server" CssClass="txtboxformat" Width="85px" 
                                                        BorderStyle="None"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" 
                                                        Format="dd-MMM-yyyy" TargetControlID="txtfromdate">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbltodate" runat="server" CssClass="label2" Height="16px" 
                                                        Text="To:" Width="5px"></asp:Label>
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txttodate" runat="server" AutoPostBack="True" 
                                                        BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Width="97px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" 
                                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td class="style21">
                                                    &nbsp;</td>
                                                <td class="style19">
                                                    </td>
                                                <td class="style19">
                                                </td>
                                                <td class="style19">
                                                </td>
                                                <td class="style19">
                                                </td>
                                                <td class="style19">
                                                </td>
                                                <td class="style19">
                                                </td>
                                                <td>
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td class="style51">
                                                    <asp:Label ID="Label4" runat="server" CssClass="style18" Font-Bold="True" 
                                                        Font-Size="12px" Text="Name:" Width="80px"></asp:Label>
                                                    </td>
                                                <td class="style52">
                                                    <asp:TextBox ID="txtSrcPro" runat="server" CssClass="txtboxformat" 
                                                        Width="85px" BorderStyle="None"></asp:TextBox>
                                                </td>
                                                <td class="style51">
                                                    <asp:ImageButton ID="ibtnFindProject" runat="server" Height="18px" 
                                                        ImageUrl="~/Image/find_images.jpg" onclick="ibtnFindProject_Click" />
                                                </td>
                                                <td valign="top" colspan="2">
                                                    <asp:DropDownList ID="ddlUserName" runat="server" Font-Bold="True" 
                                                        Font-Size="12px" 
                                                        Width="210px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="style54">
                                                    <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" BorderColor="White"
                                                BorderStyle="Solid" BorderWidth="1px" ForeColor="White" onclick="lbtnOk_Click" 
                                                        Style="font-size: small; font-weight: 700; height: 17px; text-align: center;" 
                                                        Font-Underline="False" Width="30px">Ok</asp:LinkButton>

                                                        

                                                </td>
                                                <td class="style55">
                                                    </td>
                                                <td class="style55">
                                                </td>
                                                <td class="style55">
                                                </td>
                                                <td class="style55">
                                                </td>
                                                <td class="style55">
                                                </td>
                                                <td class="style55">
                                                </td>
                                                <td class="style51">
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblPage" runat="server" CssClass="style18" Font-Bold="True" 
                                                        Font-Size="12px" style="text-align: right" Text="Size:" Visible="False" 
                                                        Width="80px"></asp:Label>
                                                    </td>
                                                <td class="style60">
                                                    <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                                        BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                                        onselectedindexchanged="ddlpagesize_SelectedIndexChanged" Visible="False" 
                                                        Width="85px">
                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                        <asp:ListItem Value="20">20</asp:ListItem>
                                                        <asp:ListItem Value="30">30</asp:ListItem>
                                                        <asp:ListItem Value="50">50</asp:ListItem>
                                                        <asp:ListItem Value="100">100</asp:ListItem>
                                                        <asp:ListItem Value="150">150</asp:ListItem>
                                                        <asp:ListItem Value="200">200</asp:ListItem>
                                                        <asp:ListItem Value="300">300</asp:ListItem>
                                                    </asp:DropDownList>
                                                    </td>
                                                <td>
                                                    &nbsp;</td>
                                                <td valign="top">
                                                    &nbsp;</td>
                                                <td valign="top">
                                                    &nbsp;</td>
                                                <td class="style21">
                                                    </td>
                                                <td class="style19">
                                                    </td>
                                                <td class="style19">
                                                </td>
                                                <td class="style19">
                                                </td>
                                                <td class="style19">
                                                </td>
                                                <td class="style19">
                                                </td>
                                                <td class="style19">
                                                </td>
                                                <td>
                                                    </td>
                                            </tr>
                                        </table>
                                        </asp:Panel>

                        <asp:Panel ID="PanelVou" runat="server">
                            <table style="width:100%;">
                            
                            <tr>
                                <td colspan="11">
                                    <asp:GridView ID="gvLogType" runat="server" AutoGenerateColumns="False" 
                                        ShowFooter="True" AllowPaging="True" 
                                        onpageindexchanging="gvLogType_PageIndexChanging">
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
                                           <asp:TemplateField HeaderText="Employee Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgcType" runat="server" 
                                                        Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "empname").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")).Trim(): "")  %>' 
                                                        Width="250px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            
                                            
                                            <asp:TemplateField HeaderText="Entry User">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvEntryuser" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "entryuser")) %>'
                                                         Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Entry Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvEntryDat" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "entrydat")) %>'
                                                         Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Entry Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvEntryTime" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postedtime")) %>'
                                                         Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Entry IP Address">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvEntryIP" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postrmid")) %>'
                                                         ></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit/App. User">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvEdituser" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "edituser")) %>'
                                                         Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvEditDat" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "editdat")) %>'
                                                         Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit IP Address">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvdeleteuser" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "editrmid")) %>'
                                                         Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            
                                             
                                        </Columns>
                                        <FooterStyle BackColor="#333333" />
                                        <PagerStyle HorizontalAlign="Left" ForeColor="White" />
                                        <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                        <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                        <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            
                        </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            
</asp:Content>



