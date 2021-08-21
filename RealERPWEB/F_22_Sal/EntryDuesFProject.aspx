<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EntryDuesFProject.aspx.cs" Inherits="RealERPWEB.F_22_Sal.EntryDuesFProject" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">



 <style type="text/css">
        .style5
        {
            height: 28px;
        }
        .style17
        {
            color: #FFFFFF;
        }
         .HeaderStyle
        {
            text-transform:capitalize;
            
            }
                
                 .style101
        {
            BACKGROUND-COLOR: transparent;
            BORDER-TOP-STYLE: none; 
            BORDER-RIGHT-STYLE: none; 
            BORDER-LEFT-STYLE: none; 
            TEXT-ALIGN: right; 
            BORDER-BOTTOM-STYLE: none;
            font-size:11px
        }
         .style103
        {
            width: 26px;
        }
        .style104
        {
            width: 62px;
        }
        .style15
        {
            color: #FFFFFF;
            text-align: right;
        }
        .style108
        {
            width: 1668px;
        }
        .style27
        {
            color: #FFFFFF;
        }
        
        .style109
        {
            width: 69px;
        }
        .style110
        {
            width: 92px;
        }
        .style111
        {
            width: 106px;
        }
        .style113
        {
            width: 49px;
        }
        
        .style114
        {
            width: 420px;
        }
        
        .style115
        {
            width: 13px;
        }
        
        </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
  <script language="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>
  
   <script language="javascript" type="text/javascript">

       $(document).ready(function () {
           Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(PageLoad);

       });

       function PageLoad() {
           var gv = $('#<%=this.gvDuesFProject.ClientID %>');
           gv.Scrollable();
       }
   </script>
       <table style="width: 98%;">
        <tr>
            <td class="style12" width="500">
                <asp:Label ID="lblHeaderTitle" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="DUES COLLECTION - FINISHING PROJECT VIEW/EDIT" Width="500px"
                   STYLE="border-bottom:1px solid blue;border-top:1px solid blue;" ></asp:Label>
            </td>
            <td class="style31">
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
                    <td class="style5" colspan="12">
                        <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                            BorderWidth="1px">
                            <table style="width:100%;">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblProjectList" runat="server" CssClass="style17" 
                                            Font-Bold="True" Font-Size="12px" style="text-align: left;" 
                                            Text="Project Name:" Width="90px"></asp:Label>
                                    </td>
                                    <td class="style104">
                                        <asp:TextBox ID="txtProjectSearch" runat="server" BorderStyle="None" 
                                            Height="18px" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style103">
                                        <asp:ImageButton ID="ImgbtnFindProject" runat="server" Height="19px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="ImgbtnFindProject_Click" 
                                            TabIndex="1" Width="16px" />
                                    </td>
                                    <td class="style108">
                                        <asp:DropDownList ID="ddlProject" runat="server" CssClass="newStyle1" 
                                            Font-Bold="True" Font-Size="11px" Height="21px" TabIndex="2" Width="300px">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblProjectDesc" runat="server" BackColor="White" 
                                            CssClass="newStyle1" Font-Bold="True" Font-Size="12px" ForeColor="Blue" 
                                            Height="18px" style="font-weight: 700" Visible="False" Width="300px"></asp:Label>
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#000066" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CssClass="style15" 
                                            Font-Bold="True" Font-Size="12px" onclick="lbtnOk_Click" 
                                            style="text-align: center; height: 17px;" TabIndex="3" Width="50px">Ok</asp:LinkButton>
                                    </td>
                                    <td class="style111">
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                            <ProgressTemplate>
                                                <asp:Label ID="Label3" runat="server" BackColor="Blue" BorderColor="White" 
                                                    BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" 
                                                    ForeColor="Yellow" style="text-align: left" Text="Please wait . . . . . . ." 
                                                    Width="120px"></asp:Label>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
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
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="12">
                        <asp:Panel ID="PnlColoumn" runat="server" BorderColor="Yellow" 
                            BorderStyle="Solid" BorderWidth="1px" Visible="False">
                            <table style="width:100%;">
                                <tr>
                                    <td class="style111">
                                        <asp:Label ID="lbl01" runat="server" CssClass="style27" Font-Bold="True" 
                                            Font-Size="12px" Font-Underline="False" ForeColor="White" Height="16px" 
                                            style="font-weight: 700; text-align:left" Text="Start Date:" 
                                            Width="60px"></asp:Label>
                                    </td>
                                    <td class="style111">
                                        <asp:TextBox ID="txtschstddate" runat="server" CssClass="txtboxformat" 
                                            Font-Bold="True" Width="80px" TabIndex="4" BorderStyle="None"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtschstddate_CalendarExtender" runat="server" 
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtschstddate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style110">
                                        <asp:Label ID="lbl2" runat="server" CssClass="style27" Font-Bold="True" 
                                            Font-Size="12px" Font-Underline="False" ForeColor="White" Height="16px" 
                                            style="font-weight: 700; text-align:left" Text="End Date:" 
                                            Width="65px"></asp:Label>
                                    </td>
                                    <td class="style109">
                                        <asp:TextBox ID="txtschenddate" runat="server" CssClass="txtboxformat" 
                                            Font-Bold="True" Width="80px" TabIndex="5" BorderStyle="None"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtschenddate_CalendarExtender" runat="server" 
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtschenddate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style113">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" 
                                            style="color: #FFFFFF; text-align: right;" Text="Page Size:" Visible="False" 
                                            Width="60px"></asp:Label>
                                    </td>
                                    <td class="style115">
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                            BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                            onselectedindexchanged="ddlpagesize_SelectedIndexChanged" Visible="False" 
                                            Width="60px">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style114">
                                        <asp:LinkButton ID="lbtnGenerate" runat="server" BackColor="#000066" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CssClass="style15" 
                                            Font-Bold="True" Font-Size="12px" onclick="lbtnGenerate_Click" 
                                            style="text-align: center; height: 17px;" TabIndex="6" Width="50px">Generate</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblColGroup" runat="server" BackColor="#660033" Font-Size="20px" 
                                            ForeColor="White" style="text-align: center;" Width="26px" Height="20px">1</asp:Label>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbtngvP1" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" Height="16px" onclick="lbtngvP_Click" 
                                            style="text-align: center" Width="17px">1</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbtngvP2" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" onclick="lbtngvP_Click" style="text-align: center" 
                                            Width="17px">2</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbtngvP3" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" onclick="lbtngvP_Click" style="text-align: center" 
                                            Width="17px">3</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbtngvP4" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" onclick="lbtngvP_Click" style="text-align: center" 
                                            Width="17px">4</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbtngvP5" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" onclick="lbtngvP_Click" style="text-align: center" 
                                            Width="17px">5</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbtngvP6" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" onclick="lbtngvP_Click" style="text-align: center" 
                                            Width="17px">6</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbtngvP7" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" onclick="lbtngvP_Click" style="text-align: center" 
                                            Width="17px">7</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbtngvP8" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" onclick="lbtngvP_Click" style="text-align: center" 
                                            Width="17px">8</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbtngvP9" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" onclick="lbtngvP_Click" style="text-align: center" 
                                            Width="17px">9</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="12">
                        <asp:GridView ID="gvDuesFProject" runat="server" AutoGenerateColumns="False" 
                            HeaderStyle-CssClass="HeaderStyle"
                            Width="16px" ShowFooter="True" 
                            onpageindexchanging="gvDuesFProject_PageIndexChanging" AllowPaging="True">
                            <PagerStyle ForeColor="White" />
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnTotal" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" onclick="lbtnTotal_Click">Total</asp:LinkButton>
                                    </FooterTemplate>
                                    
                                    <ItemTemplate>
                                         <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False" Font-Size="12px" 
                                                                style="text-align: right" 
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px">
                                         </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Item Description ">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" onclick="lbtnUpdate_Click">Final Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvItemDesc" runat="server" Font-Size="12px" 
                                          
                                            
                                              Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usirdesc")) %>'   Width="180px">   </asp:Label>    
                                                                         
                                                                         
                                                                    
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                              
                                 <asp:TemplateField HeaderText="Sale Amt.">
                                     <FooterTemplate>
                                         <asp:Label ID="lblgvFsalam" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right"></asp:Label>
                                     </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvsalam" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salam")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                
                                  <asp:TemplateField HeaderText="Received Amt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvttrqty" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recam")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                         <asp:Label ID="lblgvFrecam" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Receivable">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvreceivable" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "receivable")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                         <asp:Label ID="lblgvFreceivable" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                                 <asp:TemplateField HeaderText="Total Allocation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvschdues" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toscham")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                         <asp:Label ID="lblgvFtoschdues" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField> 


                                <asp:TemplateField HeaderText="Over Dues">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvoverdues" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "overdue")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                         <asp:Label ID="lblFgvoverdues" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />

                                </asp:TemplateField>


                                 
                                 
                               


                                <asp:TemplateField HeaderText="YM1">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty001" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym1")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                         <asp:Label ID="lblgvFym1qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM2">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty002" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym2")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                         <asp:Label ID="lblgvFym2qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM3">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty003" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym3")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="55px"></asp:TextBox>
                                             
                                    </ItemTemplate>

                                    <FooterTemplate>
                                         <asp:Label ID="lblgvFym3qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM4">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty004" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym4")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="55px"></asp:TextBox>
                                             
                                    </ItemTemplate>
                                    <FooterTemplate>
                                         <asp:Label ID="lblgvFym4qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM5">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty005" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym5")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                         <asp:Label ID="lblgvFym5qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM6">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty006" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym6")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                         <asp:Label ID="lblgvFym6qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM7">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty007" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym7")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                         <asp:Label ID="lblgvFym7qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM8" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty008" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym8")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                         <asp:Label ID="lblgvFym8qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM9" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty009" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym9")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                         <asp:Label ID="lblgvFym9qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM10" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty010" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym10")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                         <asp:Label ID="lblgvFym10qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM11" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty011" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym11")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                         <asp:Label ID="lblgvFym11qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM12" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty012" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym12")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                         <asp:Label ID="lblgvFym12qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM13" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty013" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym13")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                         <asp:Label ID="lblgvFym13qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM14" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty014" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym14")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                         <asp:Label ID="lblgvFym14qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM15" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty015" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym15")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                         <asp:Label ID="lblgvFym15qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM16" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty016" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym16")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                         <asp:Label ID="lblgvFym16qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM17" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty017" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym17")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                         <asp:Label ID="lblgvFym17qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM18" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty018" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym18")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                         <asp:Label ID="lblgvFym18qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM19" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty019" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym19")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                         <asp:Label ID="lblgvFym19qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM20" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty020" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym20")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                         <asp:Label ID="lblgvFym20qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM21" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty021" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym21")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                      <FooterTemplate>
                                         <asp:Label ID="lblgvFym21qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM22" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty022" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym22")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                      <FooterTemplate>
                                         <asp:Label ID="lblgvFym22qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM23" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty023" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym23")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                      <FooterTemplate>
                                         <asp:Label ID="lblgvFym23qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM24" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty024" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym24")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                      <FooterTemplate>
                                         <asp:Label ID="lblgvFym24qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM25" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty025" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym25")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                      <FooterTemplate>
                                         <asp:Label ID="lblgvFym25qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM26" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty026" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym26")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                      <FooterTemplate>
                                         <asp:Label ID="lblgvFym26qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM27" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty027" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym27")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                      <FooterTemplate>
                                         <asp:Label ID="lblgvFym27qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM28" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty028" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym28")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                      <FooterTemplate>
                                         <asp:Label ID="lblgvFym28qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM29" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty029" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym29")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                      <FooterTemplate>
                                         <asp:Label ID="lblgvFym29qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM30" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty030" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym30")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                      <FooterTemplate>
                                         <asp:Label ID="lblgvFym30qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM31" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty031" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym31")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                         <asp:Label ID="lblgvFym31qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM32" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty032" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym32")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                         <asp:Label ID="lblgvFym32qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM33" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty033" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym33")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                         <asp:Label ID="lblgvFym33qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM34" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty034" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym34")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                         <asp:Label ID="lblgvFym34qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM35" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty035" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym35")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                         <asp:Label ID="lblgvFym35qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM36" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty036" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym36")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                         <asp:Label ID="lblgvFym36qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM37" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty037" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym37")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                         <asp:Label ID="lblgvFym37qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM38" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty038" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym38")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                         <asp:Label ID="lblgvFym38qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM39" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty039" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym39")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                         <asp:Label ID="lblgvFym39qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM40" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty040" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym40")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                         <asp:Label ID="lblgvFym40qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM41" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty041" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym41")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>

                                     <FooterTemplate>
                                         <asp:Label ID="lblgvFym41qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM42" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty042" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym42")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                         <asp:Label ID="lblgvFym42qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM43" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty043" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym43")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                         <asp:Label ID="lblgvFym43qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM44" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty044" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym44")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                         <asp:Label ID="lblgvFym44qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM45" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty045" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym45")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                         <asp:Label ID="lblgvFym45qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ym46" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty046" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym46")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                         <asp:Label ID="lblgvFym46qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM47" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty047" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym47")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                         <asp:Label ID="lblgvFym47qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM48" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty048" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym48")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                         <asp:Label ID="lblgvFym48qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM49" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty049" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym49")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                         <asp:Label ID="lblgvFym49qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YM50" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty050" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym50")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                         <asp:Label ID="lblgvFym50qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="YM51" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty051" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym51")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                         <asp:Label ID="lblgvFym51qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="YM52" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty052" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym52")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                         <asp:Label ID="lblgvFym52qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="YM53" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty053" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym53")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                         <asp:Label ID="lblgvFym53qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="YM54" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty054" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym54")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                         <asp:Label ID="lblgvFym54qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="YM55" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty055" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym55")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                         <asp:Label ID="lblgvFym55qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="YM56" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty056" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym56")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                         <asp:Label ID="lblgvFym56qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="YM57" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty057" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym57")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                         <asp:Label ID="lblgvFym57qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="YM58" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty058" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym58")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                         <asp:Label ID="lblgvFym58qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="YM59" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty059" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym59")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                         <asp:Label ID="lblgvFym59qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="YM60" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty060" runat="server" CssClass="style101" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ym60")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                         <asp:Label ID="lblgvFym60qty" runat="server" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="White" style="text-align: right; width:55px;"></asp:Label>
                                     </FooterTemplate>
                                      <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#333333" />
                            <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
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

