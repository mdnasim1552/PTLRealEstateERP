<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptReqAdjust.aspx.cs" Inherits="RealERPWEB.F_14_Pro.RptReqAdjust" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style8
        {
            width: 74px;
        }
        .style9
        {
            width: 9px;
        }
        .style10
        {
            width: 72px;
        }
        .style11
        {
            width: 444px;
        }
        .style12
        {
            width: 99px;
        }
        .style13
        {
            width: 121px;
        }
        .style14
        {
            width: 75px;
        }
        .style15
        {
            width: 1px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
   <script language="javascript" type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
  <script language="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>
  <script type="text/javascript" language="javascript" src="../Scripts/KeyPress.js"></script>
  
   <script language="javascript" type="text/javascript">

       $(document).ready(function () {
           Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

       });
       function pageLoaded() {
           var gv = $('#<%=this.gvReqAdjStatus.ClientID %>');
           gv.Scrollable();

           $("input, select").bind("keydown", function (event) {
               var k1 = new KeyPress();
               k1.textBoxHandler(event);

           });
       }
   </script>
  
  <table style="width: 97%;">
        <tr>
            <td class="style11">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="Requisition Adjustment" Width="400px"
                   STYLE="border-bottom:1px solid white;border-top:1px solid white;" ></asp:Label>
            </td>
            <td class="style13">
                                    <asp:Label ID="lbljavascript" runat="server"></asp:Label>
            </td>
            <td class="style12">
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
                            <td class="style14">
                                <asp:Label ID="lbldatefrm" runat="server" Font-Bold="True" Font-Size="12px" 
                                    style="text-align: left; color: #FFFFFF;" Text="Date:" Width="70px"></asp:Label>
                            </td>
                            <td class="style8">
                                <asp:TextBox ID="txtFDate" runat="server" BorderStyle="None" 
                                    CssClass="txtboxformat" Width="80px"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                                    Format="dd-MMM-yyyy" TargetControlID="txtFDate">
                                </cc1:CalendarExtender>
                            </td>
                            <td class="style15">
                                <asp:Label ID="lbldateto" runat="server" Font-Bold="True" Font-Size="12px" 
                                    style="text-align: right; color: #FFFFFF;" Text="To:"></asp:Label>
                            </td>
                            <td class="style10">
                                <asp:TextBox ID="txttodate" runat="server" BorderStyle="None" 
                                    CssClass="txtboxformat" Font-Bold="False" Width="80px" TabIndex="1"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" 
                                    Format="dd-MMM-yyyy" TargetControlID="txttodate" TodaysDateFormat="">
                                </cc1:CalendarExtender>
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
                        </tr>
                        <tr>
                            <td class="style14">
                                <asp:Label ID="lblProjectName" runat="server" Font-Bold="True" Font-Size="12px" 
                                    style="text-align: left; color: #FFFFFF;" Text="Project Name:" Width="80px"></asp:Label>
                            </td>
                            <td class="style8">
                                <asp:TextBox ID="txtSrcProject" runat="server" CssClass="txtboxformat" 
                                    Font-Bold="True" Width="80px"></asp:TextBox>
                            </td>
                            <td class="style15">
                                <asp:ImageButton ID="imgbtnFindProject" runat="server" Height="17px" 
                                    ImageUrl="~/Image/find_images.jpg" onclick="imgbtnFindProject_Click" 
                                    Width="16px" />
                            </td>
                            <td class="style10">
                                <asp:DropDownList ID="ddlProjectName" runat="server" Font-Bold="True" 
                                    Font-Size="12px" Width="300px">
                                </asp:DropDownList>
                                <cc1:ListSearchExtender ID="ddlProjectName_ListSearchExtender2" runat="server" 
                                    QueryPattern="Contains" TargetControlID="ddlProjectName">
                                </cc1:ListSearchExtender>
                                
                            </td>
                            <td>
                                <asp:LinkButton ID="lbtnOk0" runat="server" BackColor="#003366" 
                                    BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                    Font-Size="12px" ForeColor="White" onclick="lbtnOk_Click" 
                                    style="text-align: center; " TabIndex="4" Width="50px">Ok</asp:LinkButton>
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
                        <tr>
                            <td class="style14">
                                <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" 
                                    style="text-align: left; color: #FFFFFF;" Text="Size:" Width="70px"></asp:Label>
                            </td>
                            <td class="style8">
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                    BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                    onselectedindexchanged="ddlpagesize_SelectedIndexChanged" TabIndex="2" 
                                    Width="80px">
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
                            <td class="style15">
                                <asp:Label ID="lblReq" runat="server" Font-Bold="True" Font-Size="12px" 
                                    style="text-align: right; color: #FFFFFF;" Text="MRF No:" Width="50px"></asp:Label>
                            </td>
                            <td class="style10">
                                <asp:TextBox ID="txtMRFNO" runat="server" BorderStyle="None" 
                                    CssClass="txtboxformat" TabIndex="3" Width="80px"></asp:TextBox>
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
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="12">
                                                                    <asp:GridView ID="gvReqAdjStatus" runat="server" AllowPaging="True" 
                                                                        
                    AutoGenerateColumns="False" onpageindexchanging="gvReqAdjStatus_PageIndexChanging" 
                                                                        ShowFooter="True" 
                    Width="853px" PageSize="15">
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
                                                                             <asp:TemplateField HeaderText="Project Description">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblgvprojectdesc" runat="server" Height="16px" 
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>' 
                                                                                        Width="150px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Adjustment No">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lgvAdjNo" runat="server" 
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "adjstno")) %>' 
                                                                                        Width="85px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                            </asp:TemplateField>

                                                                               <asp:TemplateField HeaderText="Adjustment Date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lgvAdydat" runat="server" 
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "adjstdat")) %>' 
                                                                                        Width="65px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                            </asp:TemplateField>

                                                                              <asp:TemplateField HeaderText="Req No">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lgvReqNo" runat="server" 
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>' 
                                                                                        Width="85px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                            </asp:TemplateField>

                                                                               <asp:TemplateField HeaderText="Req. Date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lgvReqdat" runat="server" 
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat")) %>' 
                                                                                        Width="65px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                            </asp:TemplateField>

                                                                              <asp:TemplateField HeaderText="MRF No">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lgvMrfNo" runat="server" 
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>' 
                                                                                        Width="75px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Unit">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lgvUnit" runat="server" 
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>' 
                                                                                        Width="30px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Material Description">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lgvmatDesc" runat="server" 
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>' 
                                                                                        Width="150px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                              <asp:TemplateField HeaderText="Adjustment Qty">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lgvAdjQty" runat="server" style="text-align: right"  
                                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adjstqty")).ToString("#,##0;(#,##0); ") %>' 
                                                                                        Width="70px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                            
                                                                               <asp:TemplateField HeaderText="Entry User">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lgvUser" runat="server" 
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>' 
                                                                                        Width="70px"></asp:Label>
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


