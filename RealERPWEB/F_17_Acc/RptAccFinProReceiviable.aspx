<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptAccFinProReceiviable.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptAccFinProReceiviable" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .style41
        {
            width: 48px;
        }
        .style44
        {
            width: 62px;
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
        .style45
        {
            width: 13px;
        }
        
        .style40
        {
            color: #FFFFFF;
            text-align: center;
        }
        .style46
        {
            width: 74px;
        }
        .style47
        {
            width: 73px;
        }
        .style18
        {
            color: #FFFFFF;
            text-align: left;
        }
        .style48
        {
            width: 80px;
        }
        .style49
        {
            width: 11px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <table style="width: 91%;">
        <tr>
            <td class="style35">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="18px" ForeColor="Yellow"
                    Text="Account Receiviable - (Finished Project)" Width="500px" Style="border-bottom: 1px solid WHITE;
                    border-top: 1px solid WHITE;"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbljavascript" runat="server"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" Style="font-size: 11px"
                    Width="130px">
                    <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                    <asp:ListItem Value="HTML">HTML</asp:ListItem>
                    <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                    <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style38">
                <asp:LinkButton ID="lbtnPrint" runat="server" Font-Bold="True" OnClick="lbtnPrint_Click"
                    Style="color: #FFFFFF" CssClass="button">PRINT</asp:LinkButton>
            </td>
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
            <table style="width: 100%;">
                <tr>
                    <td colspan="12">
                        <asp:Panel ID="Panel1" runat="server" BorderStyle="Solid" BorderWidth="1px" BorderColor="Yellow" Width="850px">
                            <table style="width: 100%;">
                             <tr>
                                    <td class="style41">
                                        <asp:Label ID="Label8" runat="server" CssClass="style18" Font-Bold="True" 
                                            Font-Size="12px" Text="Project Name:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style44">
                                        <asp:TextBox ID="txtSrcPro" runat="server" BorderStyle="None" 
                                            CssClass="txtboxformat" Width="85px"></asp:TextBox>
                                    </td>
                                    <td class="style45">
                                        <asp:ImageButton ID="ibtnFindProject" runat="server" Height="18px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="ibtnFindProject_Click" />
                                    </td>
                                    <td class="style46">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" Font-Bold="True" 
                                            Font-Size="12px"  
                                            Width="300px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style47">
                                        <asp:LinkButton ID="lbtnShow" runat="server" BackColor="#003366" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CssClass="style40" 
                                            Font-Bold="True" Font-Size="12px" Height="16px" OnClick="lbtnShow_Click" 
                                            Text="Show" Width="56px"></asp:LinkButton>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style41">
                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" 
                                            Height="16px" style="color: #FFFFFF; text-align: left;" Text="Page Size:" 
                                            Width="80px"></asp:Label>
                                    </td>
                                    <td class="style44">
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                            BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                            onselectedindexchanged="ddlpagesize_SelectedIndexChanged" 
                                            style="margin-left: 0px" Width="87px">
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                            <asp:ListItem>600</asp:ListItem>
                                            <asp:ListItem>900</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style45">
                                        <asp:Label ID="Label6" runat="server" CssClass="style40" Font-Bold="True" 
                                            Font-Size="12px" Style="text-align: left" Text="Date:"></asp:Label>
                                    </td>
                                    <td class="style46">
                                        <asp:TextBox ID="txtdate" runat="server" CssClass="txtboxformat" 
                                            Font-Bold="True" Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" 
                                            Format="dd-MMM-yyyy " TargetControlID="txtdate" TodaysDateFormat="">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style47">
                                        &nbsp;</td>
                                    <td>
                                        <asp:Label ID="lblmsg" runat="server" BackColor="Red" CssClass="label3" 
                                            Font-Names="Verdana" Font-Size="12px" ForeColor="White" Height="16px"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                         <asp:Panel ID="Panel2" runat="server" BorderStyle="Solid" BorderWidth="1px" BorderColor="Yellow" Width="850px">
                                                        <table style="width:100%;">
                                                            <tr>
                                                                <td class="style46">
                                                                    <asp:Label ID="lblAmount" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                        ForeColor="White" Style="text-align: left;" Text="Amount:" Width="80px"></asp:Label>
                                                                </td>
                                                                <td class="style47">
                                                                    <asp:DropDownList ID="ddlSrchCash" runat="server" AutoPostBack="True" 
                                                                        Font-Bold="True" Font-Size="12px" 
                                                                        onselectedindexchanged="ddlSrchCash_SelectedIndexChanged" Width="200px">
                                                                        <asp:ListItem Value="">--Select--</asp:ListItem>
                                                                         <asp:ListItem Value="=">Equal</asp:ListItem>
                                                                        <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                                                        <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                                                        <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                                                        <asp:ListItem Value="&gt;=">Greater Then  Equal</asp:ListItem>
                                                                        <asp:ListItem Value="between">Between</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="style48">
                                                                    <asp:TextBox ID="txtAmountC1" runat="server" BorderStyle="None" 
                                                                        Font-Bold="True" Height="16px" style="text-align: right" Width="85px"></asp:TextBox>
                                                                </td>
                                                                <td class="style49">
                                                                    <asp:Label ID="lblToCash" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                        ForeColor="White" Style="text-align: right;" Text="To:" Visible="False"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAmountC2" runat="server" BorderStyle="None" 
                                                                        Font-Bold="True" Height="16px" style="text-align: right" Visible="False" 
                                                                        Width="80px"></asp:TextBox>
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
                </table>
               
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="AccRecDetails" runat="server">
                        <table style="width:100%;">
                            <tr>
                            <td colspan="12">
                                <asp:GridView ID="gvfinProject" runat="server" AllowPaging="True" 
                                    AutoGenerateColumns="False" 
                                    onpageindexchanging="gvfinProject_PageIndexChanging" ShowFooter="True" 
                                    Width="501px" onrowdatabound="gvfinProject_RowDataBound">
                                    <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo8" runat="server" Font-Bold="True" 
                                                    Style="text-align: right" 
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actcode" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvActcode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode1")) %>' 
                                                    Width="100px">     </asp:Label>
                                            </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ActDesc" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvActDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")) %>' 
                                                    Width="100px">     </asp:Label>
                                            </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rescode" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAcRes" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>' 
                                                    Width="100px">     </asp:Label>
                                            </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ResDesc" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>' 
                                                    Width="100px">     </asp:Label>
                                            </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <FooterTemplate>
                                                <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    ForeColor="White" Text="Total"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink id="HLgvActdesc" runat="server" Text='<%#  "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         "<B>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim()+"</B>": "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                          (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?   "<br>" 
                                                                          :(Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>":"")) + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "")
                                                                         
                                                                            %>' Width="350px" Font-Underline="False" ForeColor="Black" Target="_blank">
                                                            </asp:HyperLink>
                                            </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Total Value">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtamt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    ForeColor="White" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtamt" runat="server" Style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sales  Amt.">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFSalAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    ForeColor="White" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvacamt" runat="server" Style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salam")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Received Amt.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrecamt" runat="server" Style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recam")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lgvFRecAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    ForeColor="White" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Receiviable Amt.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrecabamt" runat="server" Style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balam")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFBalAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    ForeColor="White" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <FooterStyle HorizontalAlign="Right" />
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
                    </asp:View>
                    <asp:View ID="AccRecSum" runat="server">
                        <table style="width:100%;">
                            <tr>
                            <td colspan="12">
                                <asp:GridView ID="grvAccRecSum" runat="server" AllowPaging="True" 
                                    AutoGenerateColumns="False" 
                                    onpageindexchanging="grvAccRecSum_PageIndexChanging" ShowFooter="True" 
                                    Width="501px">
                                    <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo8" runat="server" Font-Bold="True" 
                                                    Style="text-align: right" 
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <FooterTemplate>
                                                <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    ForeColor="White" Text="Total"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAcDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))   %>' Width="300px">
                                                            </asp:Label>
                                            </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Value">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtamt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    ForeColor="White" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtamt" runat="server" Style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Un-sold Amt.">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFusold" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    ForeColor="White" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvusold" runat="server" Style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usold")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sold  Amt.">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFSalAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    ForeColor="White" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvacamt" runat="server" Style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salam")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Received Amt.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrecamt" runat="server" Style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recam")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lgvFRecAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    ForeColor="White" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Receiviable Amt.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrecamt" runat="server" Style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balam")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFBalAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    ForeColor="White" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <FooterStyle HorizontalAlign="Right" />
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
                    </asp:View>
            </asp:MultiView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

