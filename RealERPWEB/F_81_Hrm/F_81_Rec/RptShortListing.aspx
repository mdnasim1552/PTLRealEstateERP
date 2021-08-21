<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptShortListing.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_81_Rec.RptShortListing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 908px; border-bottom: #d2f4c0 2px outset; height: 0px;">
        <tr>
            <td class="style57">
                <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="18px"
                    Style="border: 2px inset #ffcc99; color: maroon; background-color: #fffbf1;"
                    Text="Short Listing Information Input/Edit Screen" Width="450px" BorderStyle="Inset"
                    BackColor="Transparent" BorderWidth="2px"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbljavascript" runat="server"></asp:Label>
            </td>
            <td class="style58">
                <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" Style="font-size: 11px"
                    Width="130px">
                    <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                    <asp:ListItem Value="HTML">HTML</asp:ListItem>
                    <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                    <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style59">
                <asp:LinkButton ID="lnkPrint" runat="server" Font-Bold="True" Font-Italic="False"
                    OnClick="lnkPrint_Click" CssClass="button" Font-Names="Verdana">PRINT</asp:LinkButton>
            </td>
            <td class="style16">
                &nbsp;
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
            <asp:Panel ID="Panel3" runat="server" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px">
                <table style="width: 900px">
                    <tr>
                        <td class="style78">
                            <asp:Label ID="lblfrmdate" runat="server" Font-Bold="True" Font-Size="12px" 
                                ForeColor="White" style="text-align: left" Text="From:" Width="80px"></asp:Label>
                        </td>
                        <td class="style42">
                            <asp:TextBox ID="txtfromdate" runat="server" CssClass="txtboxformat" 
                                Width="80px"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" 
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate">
                            </cc1:CalendarExtender>
                        </td>
                        <td class="style34" align="right">
                            <asp:Label ID="lbltodate" runat="server" Font-Bold="True" Font-Size="12px" 
                                ForeColor="White" style="text-align: right" Text="To:"></asp:Label>
                        </td>
                        <td class="style43">
                            <asp:TextBox ID="txttodate" runat="server" BorderStyle="None" Width="80px"></asp:TextBox>
                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" 
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate">
                            </cc1:CalendarExtender>
                        </td>
                        <td class="style34">
                            &nbsp;</td>
                        <td class="style34">
                            &nbsp;</td>
                        <td class="style90">
                            &nbsp;</td>
                        <td class="style89">
                            &nbsp;</td>
                        <td class="style46">
                            &nbsp;</td>
                        <td class="style19">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style78">
                            <asp:Label ID="lblpreAdv" runat="server" CssClass="style15" Font-Bold="True" 
                                Font-Size="12px" Height="16px" Style="text-align: left" Text="ADV List:" 
                                Width="83px"></asp:Label>
                        </td>
                        <td class="style42">
                            <asp:TextBox ID="txtSrchPre" runat="server" BorderStyle="None" Font-Bold="True" 
                                Font-Size="12px" TabIndex="11" Width="80px"></asp:TextBox>
                        </td>
                        <td align="right" class="style34">
                            <asp:ImageButton ID="ImgbtnFindAdv" runat="server" Height="19px" 
                                ImageUrl="~/Image/find_images.jpg" OnClick="ImgbtnFindAdv_Click" TabIndex="12" 
                                Width="16px" />
                        </td>
                        <td class="style43">
                            <asp:DropDownList ID="ddlPrevAdvList" runat="server" Font-Size="12px" 
                                TabIndex="13" Width="350px">
                            </asp:DropDownList>
                        </td>
                        <td class="style34">
                            &nbsp;</td>
                        <td class="style34">
                            &nbsp;</td>
                        <td class="style90">
                            &nbsp;
                        </td>
                        <td class="style89">
                            &nbsp;
                        </td>
                        <td class="style46">
                            &nbsp;
                        </td>
                        <td class="style19">
                        </td>
                    </tr>
                    <tr>
                        <td class="style78">
                            <asp:Label ID="lblResList" runat="server" Font-Bold="True" Font-Size="12px" Style="text-align: left;
                                color: #FFFFFF;" Text="Post List:" Width="98px"></asp:Label>
                        </td>
                        <td class="style42">
                            <asp:TextBox ID="txtPostSearch" runat="server" BorderStyle="None" Font-Bold="True"
                                Font-Size="12px" TabIndex="15" Width="80px"></asp:TextBox>
                        </td>
                        <td align="right" class="style34">
                            <asp:ImageButton ID="ImgbtnFindPost" runat="server" BorderStyle="None" Height="19px"
                                ImageUrl="~/Image/find_images.jpg" OnClick="ImgbtnFindPost_Click" TabIndex="16"
                                Width="16px" />
                        </td>
                        <td class="style43">
                            <asp:DropDownList ID="ddlPOSTList" runat="server" AutoPostBack="True" Font-Size="12px"
                                Style="border-right: midnightblue 1px solid; border-top: midnightblue 1px solid;
                                border-left: midnightblue 1px solid; border-bottom: midnightblue 1px solid; background-color: #fffbf1"
                                TabIndex="17" Width="350px">
                            </asp:DropDownList>
                        </td>
                        <td align="right" class="style87">
                            <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" BorderColor="White"
                                BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                Height="16px" OnClick="lbtnOk_Click" Style="text-align: center;" TabIndex="4"
                                Width="50px">Ok</asp:LinkButton>
                        </td>
                        <td class="style91">
                            &nbsp;
                        </td>
                        <td class="style90">
                            &nbsp;
                        </td>
                        <td class="style89">
                            <asp:Label ID="lblmsg1" runat="server" __designer:wfdid="w4" BackColor="Red" Font-Bold="True"
                                Font-Size="12px" ForeColor="White" Height="18px" Style="font-size: 12px; text-align: left"></asp:Label>
                        </td>
                        <td class="style46">
                            &nbsp;
                        </td>
                        <td class="style19">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table style="width: 100%;">
                <tr>
                    <td class="style18" colspan="13">
                        <asp:Panel ID="PanelAddCan" runat="server" BorderColor="Yellow" BorderStyle="Solid"
                            BorderWidth="1px">
                            <table style="width: 100%; height: 10px;">
                                <tr>
                                    <td class="style71">
                                        &nbsp;</td>
                                    <td class="style76">
                                        &nbsp;</td>
                                    <td class="style77">
                                        &nbsp;
                                    </td>
                                    <td colspan="4">
                                        &nbsp;
                                    </td>
                                    <td class="style107">
                                        &nbsp;
                                    </td>
                                    <td class="style85">
                                        &nbsp;</td>
                                    <td class="style104">
                                        &nbsp;
                                    </td>
                                    <td class="style110">
                                        &nbsp;
                                    </td>
                                    <td class="style109">
                                        &nbsp;
                                    </td>
                                    <td class="style79">
                                        &nbsp;
                                    </td>
                                    <td class="style108">
                                        &nbsp;
                                    </td>
                                    <td class="style53">
                                        &nbsp;
                                    </td>
                                    <td class="style53">
                                        &nbsp;
                                    </td>
                                    <td class="style53">
                                        &nbsp;
                                    </td>
                                    <td class="style53">
                                        &nbsp;
                                    </td>
                                    <td class="style53">
                                        &nbsp;
                                    </td>
                                    <td class="style53">
                                        &nbsp;
                                    </td>
                                    <td class="style53">
                                        &nbsp;
                                    </td>
                                    <td class="style53">
                                        &nbsp;
                                    </td>
                                    <td class="style53">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="style53">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="Panel2" runat="server" Visible="False">
                            <table style="width: 100%; height: 133px;">
                                <tr>
                                    <td style="height: auto;" colspan="12" valign="top">
                                        <asp:GridView ID="gvSListInfo" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            ShowFooter="True" Width="16px" PageSize="15" 
                                            OnRowDataBound="gvSListInfo_RowDataBound" 
                                          >
                                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo0" runat="server" Height="16px" Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                        
                                                <asp:TemplateField HeaderText="Post Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPostCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postcode")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sl Number" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvissue" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "listisu")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Col1">
                                                    <FooterTemplate>
                                                       
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvCol1" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "col1").ToString() %>' Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Col2">
                                                    <ItemTemplate>
                                                        

                                                             <asp:Label ID="lgvCol2" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "col2").ToString() %>' Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Col3">
                                                    <ItemTemplate>
                                                     


                                                             <asp:Label ID="lgvCol3" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "col3").ToString() %>' Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Col4">
                                                    <ItemTemplate>
                                                       

                                                             <asp:Label ID="lgvCol4" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "col4").ToString() %>' Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Col5">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvCol5" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "col5").ToString() %>' Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Col6">
                                                    <ItemTemplate>
                                                        
                                                         <asp:Label ID="lgvCol6" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "col6").ToString() %>' Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Col7">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvCol7" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "col7").ToString() %>' Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Col8">
                                                    <ItemTemplate>
                                                         <asp:Label ID="lgvCol8" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "col8").ToString() %>' Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Col9">
                                                    <ItemTemplate>
                                                      
                                                       <asp:Label ID="lgvCol9" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "col9").ToString() %>' Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Col10">
                                                    <ItemTemplate>
                                                      
                                                       <asp:Label ID="lgvCol10" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "col10").ToString() %>' Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Col11">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvCol11" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "col11").ToString() %>' Width="120px"></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Col12">
                                                    <ItemTemplate>
                                                      
                                                       <asp:Label ID="lgvCol12" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "col12").ToString() %>' Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Col13">
                                                    <ItemTemplate>
                                                      
                                                       <asp:Label ID="lgvCol13" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "col13").ToString() %>' Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                
                                                
                                              

                                            </Columns>
                                            <FooterStyle BackColor="#333333" />
                                            <PagerSettings Mode="NumericFirstLast" />
                                            <PagerStyle Font-Size="12px" ForeColor="White" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                                                Height="20px" HorizontalAlign="Center" />
                                            <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                            <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="PanelAddInt" runat="server" BorderColor="Yellow" BorderStyle="Solid"
                                            BorderWidth="1px" Width="1169px">
                                            <table style="width: 100%; height: 10px;">
                                                <tr>
                                                    <td class="style71">
                                                        &nbsp;</td>
                                                    <td class="style76">
                                                        &nbsp;</td>
                                                    <td class="style77">
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td class="style111">
                                                        &nbsp;</td>
                                                    <td class="style85">
                                                        &nbsp;
                                                        </td>
                                                    <td class="style104">
                                                        &nbsp;
                                                    </td>
                                                    <td class="style112">
                                                        &nbsp;</td>
                                                    <td class="style53">
                                                        &nbsp;</td>
                                                    <td class="style53">
                                                        &nbsp;</td>
                                                    <td class="style53">
                                                        &nbsp;</td>
                                                    <td class="style53">
                                                        &nbsp;</td>
                                                    <td class="style53">
                                                        &nbsp;</td>
                                                    <td class="style53">
                                                        &nbsp;
                                                    </td>
                                                    <td class="style53">
                                                        &nbsp;
                                                    </td>
                                                    <td class="style53">
                                                        &nbsp;
                                                    </td>
                                                    <td class="style53">
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td class="style53">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: auto;" colspan="12" valign="top">
                                        <asp:GridView ID="gvIntInfo" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            ShowFooter="True" Width="16px" PageSize="15" 
                                           >
                                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo0" runat="server" Height="16px" Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                              
                                                <asp:TemplateField HeaderText="Postcode Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPostCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postcode")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Int Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvIntCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "intcode")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name of Interviewer">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvIntDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "intdesc"))   %>'
                                                            Width="150px">
                                                            
                                                            
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Date of Interview">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvIntDat" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intdat")).ToString("dd-MMM-yyyy") %>' Width="80px"></asp:Label>
                                                       

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Remarks">
                                                    
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvRem" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "remarks").ToString() %>' Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#333333" />
                                            <PagerSettings Mode="NumericFirstLast" />
                                            <PagerStyle Font-Size="12px" ForeColor="White" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                                                Height="20px" HorizontalAlign="Center" />
                                            <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                            <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style71">
                                        &nbsp;
                                    </td>
                                    <td class="style43" colspan="5">
                                        &nbsp;
                                    </td>
                                    <td align="left" valign="top" class="style83">
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
                                    <td class="style53">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td class="style19">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
