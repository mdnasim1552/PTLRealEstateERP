
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkGrpAccount02.aspx.cs" Inherits="RealERPWEB.F_45_GrAcc.LinkGrpAccount02" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">


    .style50
    {
        color: white;
    }
        .style51
        {
            width: 40px;
        }
        .style52
        {
            width: 69px;
        }
        .style53
        {
            width: 71px;
        }
        .style55
        {
            width: 593px;
        }
        .style58
        {
            width: 81px;
        }
                        
        .style61
        {
            width: 668px;
        }
        .style63
        {
            width: 716px;
        }
        .style64
        {
            width: 1945px;
        }
        .style65
        {
            width: 1236px;
        }
        .style66
        {
            width: 811px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <table style="width: 912px">
        <tr>
            <td class="style43">
                <asp:Label ID="lblHeadtitle" runat="server" BackColor="Blue" Font-Bold="True" 
                ForeColor="Yellow" style="font-weight: 700; color: #FFFF66; text-align: left" Text="PROJECT WISE COLLECTION BREAK DOWN REPORT" 
                    Width="450px"></asp:Label>
            </td>
            <td class="style47">
                        <asp:Label ID="lbljavascript" runat="server"></asp:Label>
                    <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" 
                        Style="font-size: 11px" Width="130px">
                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                    </asp:DropDownList>
            </td>
            <td class="style44">
                &nbsp;</td>
            <td>
                <asp:LinkButton ID="lnkPrint" runat="server" Font-Bold="True" 
                    Font-Italic="False" Font-Underline="True" 
                    onclick="lnkPrint_Click" 
                    
                    style="  border-left-width: 2px; border-left-color: #ffff00;   text-align: center; color: #FFFFFF;" 
                    CssClass="button">PRINT</asp:LinkButton>
            </td>
        </tr>
    </table>
    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="ViewBankConfirmation" runat="server">
                             <table style="width: 100%;">
                                 <tr>
                                     <td colspan="12">
                                         <asp:Panel ID="Panel2" runat="server">
                                             <table style="width: 100%;">
                                             
                                                 <tr>
                                                     <td class="style51">
                                                         &nbsp;</td>
                                                     <td class="style52">
                                                         &nbsp;</td>
                                                     <td class="style53">
                                                         <asp:Label ID="Label10" runat="server" CssClass="style50" Font-Bold="True" 
                                                             Font-Size="12px" style="text-align: left" Text="From:" Width="80px"></asp:Label>
                                                     </td>
                                                     <td>
                                                         <asp:Label ID="lblfrmdate" runat="server" CssClass="style50" Font-Bold="True" 
                                                             Font-Size="12px" style="text-align: left" Width="80px"></asp:Label>
                                                     </td>
                                                     <td class="style55">
                                                         <asp:Label ID="Label8" runat="server" CssClass="style50" Font-Bold="True" 
                                                             Font-Size="12px" style="text-align: left" Text="To:"></asp:Label>
                                                     </td>
                                                     <td class="style64">
                                                         <asp:Label ID="lbltodate" runat="server" CssClass="style50" Font-Bold="True" 
                                                             Font-Size="12px" Height="16px" style="text-align: left" Width="80px"></asp:Label>
                                                     </td>
                                                     <td class="style65">
                                                         &nbsp;</td>
                                                     <td class="style55">
                                                         &nbsp;</td>
                                                     <td class="style55">
                                                         &nbsp;</td>
                                                     <td class="style55">
                                                         &nbsp;</td>
                                                     <td class="style55">
                                                         &nbsp;</td>
                                                     <td class="style55">
                                                         &nbsp;</td>
                                                     <td class="style55">
                                                         &nbsp;</td>
                                                     <td class="style55">
                                                         &nbsp;</td>
                                                     <td class="style55">
                                                         &nbsp;</td>
                                                     <td class="style61">
                                                         &nbsp;</td>
                                                     <td class="style63">
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
                                       <asp:GridView ID="gvCABankBal" runat="server" AutoGenerateColumns="False" 
                                                      BackColor="#FFECEC" BorderColor="#66CCFF" BorderStyle="Solid" BorderWidth="3px" 
                                                      ShowFooter="True" onrowdatabound="gvCABankBal_RowDataBound">
                                                      <Columns>
                                                          <asp:TemplateField HeaderText="Sl.No.">
                                                              <ItemTemplate>
                                                                  <asp:Label ID="lblserialnoid4" runat="server" style="text-align: right" 
                                                                      Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                              </ItemTemplate>
                                                              <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                              <ItemStyle Font-Size="12px" />
                                                          </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="Code">
                                                              <ItemTemplate>
                                                                  <asp:Label ID="lblgvcodebank3" runat="server" CssClass="GridLebel" 
                                                                      Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>' 
                                                                      Width="90px"></asp:Label>
                                                              </ItemTemplate>
                                                          </asp:TemplateField>
                                                          <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                              FooterStyle-HorizontalAlign="Right" HeaderText="Description of Accounts">
                                                              <HeaderTemplate>
                                                                  <table style="width:100%;">
                                                                      <tr>
                                                                          <td class="style58" style="width:auto">
                                                                              <asp:Label ID="Label8" runat="server" Font-Bold="True" 
                                                                                  Text="Description of Accounts"></asp:Label>
                                                                          </td>
                                                                          <td>
                                                                              &nbsp;</td>
                                                                      </tr>
                                                                  </table>
                                                              </HeaderTemplate>
                                                              <ItemTemplate>
                                                                  <asp:HyperLink ID="HLgvDescbankcb" runat="server" __designer:wfdid="w38" 
                                                                      CssClass="GridLebelL" Font-Size="12px" Font-Underline="False" Target="_blank" 
                                                                      Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' 
                                                                      Width="400px"></asp:HyperLink>
                                                              </ItemTemplate>
                                                              <HeaderStyle HorizontalAlign="Left" />
                                                              <ItemStyle HorizontalAlign="left" />
                                                              <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                          </asp:TemplateField>
                                                          <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                              FooterStyle-HorizontalAlign="Right" HeaderText="Opening Balance" 
                                                              ItemStyle-HorizontalAlign="Right">
                                                              <ItemTemplate>
                                                                  <asp:Label ID="lblgvopnbalcb" runat="server" CssClass="GridLebel" 
                                                                      Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opndram")).ToString("#,##0;(#,##0); ") %>' 
                                                                      Width="90px"></asp:Label>
                                                              </ItemTemplate>
                                                              <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                              <ItemStyle HorizontalAlign="Right" />
                                                          </asp:TemplateField>
                                                          <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                              FooterStyle-HorizontalAlign="Right" HeaderText="Opening Liabilities" 
                                                              ItemStyle-HorizontalAlign="Right">
                                                              <ItemTemplate>
                                                                  <asp:Label ID="lblgvopnliabilitiescb" runat="server" CssClass="GridLebel" 
                                                                      Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opncram")).ToString("#,##0;(#,##0); ") %>' 
                                                                      Width="90px"></asp:Label>
                                                              </ItemTemplate>
                                                              <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                              <ItemStyle HorizontalAlign="Right" />
                                                          </asp:TemplateField>


                                                         
                                                          <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                              FooterStyle-HorizontalAlign="Right" HeaderText="Closing Balance" 
                                                              ItemStyle-HorizontalAlign="Right">
                                                              <ItemTemplate>
                                                                  <asp:Label ID="lblgvclobalbankcb" runat="server" CssClass="GridLebel" 
                                                                      Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closdram")).ToString("#,##0;(#,##0); ") %>' 
                                                                      Width="90px"></asp:Label>
                                                              </ItemTemplate>
                                                              <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                              <ItemStyle HorizontalAlign="Right" />
                                                          </asp:TemplateField>
                                                          <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                              FooterStyle-HorizontalAlign="Right" HeaderText="Closing Liabilities" 
                                                              ItemStyle-HorizontalAlign="Right">
                                                              <ItemTemplate>
                                                                  <asp:Label ID="lblgvcloliabilitiescb" runat="server" CssClass="GridLebel" 
                                                                      Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closcram")).ToString("#,##0;(#,##0); ") %>' 
                                                                      Width="90px"></asp:Label>
                                                              </ItemTemplate>
                                                              <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                              <ItemStyle HorizontalAlign="Right" />
                                                          </asp:TemplateField>
                                                         
                                                          <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                              FooterStyle-HorizontalAlign="Right" HeaderText="Withdrawn" 
                                                              ItemStyle-HorizontalAlign="Right">
                                                              <ItemTemplate>
                                                                  <asp:Label ID="lblgvnetReceived" runat="server" CssClass="GridLebel" 
                                                                      Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lrecamt")).ToString("#,##0;(#,##0); ") %>' 
                                                                      Width="90px"></asp:Label>
                                                              </ItemTemplate>
                                                              <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                              <ItemStyle HorizontalAlign="Right" />
                                                          </asp:TemplateField>
                                                          <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                              FooterStyle-HorizontalAlign="Right" HeaderText="Deposit" 
                                                              ItemStyle-HorizontalAlign="Right">
                                                              <ItemTemplate>
                                                                  <asp:Label ID="lblgvnetPayment" runat="server" CssClass="GridLebel" 
                                                                      Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lpayamt")).ToString("#,##0;(#,##0); ") %>' 
                                                                      Width="90px"></asp:Label>
                                                              </ItemTemplate>
                                                              <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                              <ItemStyle HorizontalAlign="Right" />
                                                          </asp:TemplateField>
                                                      </Columns>
                                                      <FooterStyle BackColor="#66CCFF" />
                                                      <HeaderStyle BackColor="#66CCFF" ForeColor="Black" />
                                                      <EditRowStyle BorderStyle="None" />
                                                      <AlternatingRowStyle BackColor="#F9F9F9" />
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
                             <asp:View ID="ViewSales" runat="server">
                             <table style="width: 100%;">
                                 <tr>
                                     <td colspan="12">
                                         <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px">
                                             <table style="width: 100%;">
                                             
                                                 <tr>
                                                     <td class="style51">
                                                         &nbsp;</td>
                                                     <td class="style52">
                                                         &nbsp;</td>
                                                     <td class="style53">
                                                         <asp:Label ID="Label1" runat="server" CssClass="style50" Font-Bold="True" 
                                                             Font-Size="12px" style="text-align: left" Text="From:" Width="80px"></asp:Label>
                                                     </td>
                                                     <td>
                                                         <asp:Label ID="sfrDate" runat="server" CssClass="style50" Font-Bold="True" 
                                                             Font-Size="12px" style="text-align: left" Width="80px"></asp:Label>
                                                     </td>
                                                     <td class="style55">
                                                         <asp:Label ID="Label3" runat="server" CssClass="style50" Font-Bold="True" 
                                                             Font-Size="12px" style="text-align: left" Text="To:"></asp:Label>
                                                     </td>
                                                     <td class="style64">
                                                         <asp:Label ID="stDate" runat="server" CssClass="style50" Font-Bold="True" 
                                                             Font-Size="12px" Height="16px" style="text-align: left" Width="80px"></asp:Label>
                                                     </td>
                                                     <td class="style66">
                                                         <asp:Label ID="ctl46" runat="server" CssClass="style50" Font-Bold="True" 
                                                             Font-Size="12px" Height="16px" style="text-align: left" Width="100px">Project Name:</asp:Label>
                                                     </td>
                                                     <td class="style55">
                                                         <asp:Label ID="lblPrijDesc" runat="server" CssClass="style50" Font-Bold="True" 
                                                             Font-Size="12px" Height="16px" style="text-align: left" Width="300px">Project Name:</asp:Label>
                                                     </td>
                                                     <td class="style55">
                                                         &nbsp;</td>
                                                     <td class="style55">
                                                         &nbsp;</td>
                                                     <td class="style55">
                                                         &nbsp;</td>
                                                     <td class="style55">
                                                         &nbsp;</td>
                                                     <td class="style55">
                                                         &nbsp;</td>
                                                     <td class="style55">
                                                         &nbsp;</td>
                                                     <td class="style55">
                                                         &nbsp;</td>
                                                     <td class="style61">
                                                         &nbsp;</td>
                                                     <td class="style63">
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
                                       <asp:GridView ID="gvDayWSale" runat="server" AutoGenerateColumns="False" 
                                        ShowFooter="True" Width="770px" AllowPaging="True" 
                                        onpageindexchanging="gvDayWSale_PageIndexChanging">
                                        <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True"
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Project Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDPactdesc" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>' 
                                                        Width="150px" Font-Bold="true"></asp:Label>
                                                </ItemTemplate>
                                                
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Customer Name">
                                                <ItemTemplate>
                                                        <asp:Label ID="lgvDcuname" runat="server" 
                                                            Text='<%#"<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))+"</b>"+"<br>"+
                                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "custadd")) %>' 
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description of Item">
                                                
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDResDesc" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>' 
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                     <asp:Label ID="lgvFditem" runat="server" Text="Total" Font-Bold="True" HorizontalAlign="Left" Font-Size="12px" 
                                                         ForeColor="White" style="text-align: right" Width="150px"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                    
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgUnit" runat="server" 
                                                            Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "munit"))
                                                                         %>' 
                                                            Width="35px"></asp:Label>
                                                    
                                                    
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                             
                                            <asp:TemplateField HeaderText="Unit Size">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvUSize" runat="server" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="55px" style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Price per SFT">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvUpsft" runat="server" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sftpr")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="55px" style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sales Team">
                                                    
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgDCper" runat="server" 
                                                            Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "conteam"))
                                                                         %>' 
                                                            Width="120px"></asp:Label>
                                                    
                                                    
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                             
                                            <asp:TemplateField HeaderText="Budgeted Amt.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDTAmt" runat="server" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tuamt")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="75px" style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                 <FooterTemplate>
                                                     <asp:Label ID="lgvFDTAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                         ForeColor="White" style="text-align: right" Width="75px"></asp:Label>
                                                 </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sold Amt">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDSAmt" runat="server" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "suamt")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="75px" style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                 <FooterTemplate>
                                                     <asp:Label ID="lgvFDSAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                         ForeColor="White" style="text-align: right" Width="75px"></asp:Label>
                                                 </FooterTemplate>
                                                  <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Sold Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDSchdate" runat="server" 
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "schdate")).ToString("dd-MMM-yyyy") %>' 
                                                        Width="65px" style="text-align: left"></asp:Label>
                                                </ItemTemplate>
                                          
                                          
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Cancel Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDCandate" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cudate")) %>' 
                                                        Width="65px" style="text-align: left"></asp:Label>
                                                </ItemTemplate>
                                          
                                          
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            
                                             <%--<asp:TemplateField HeaderText="Discount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDDisAmt" runat="server" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disamt")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="70px" style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                 <FooterTemplate>
                                                     <asp:Label ID="lgvFDDisAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                         ForeColor="White" style="text-align: right" Width="70px"></asp:Label>
                                                 </FooterTemplate>
                                                  <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>--%>


                                            
                                            <%-- <asp:TemplateField HeaderText="%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgDvDisPer" runat="server" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disper")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                        Width="60px" style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                 
                                                  <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>--%>
                                            
                                            
                                            
                                        </Columns>
                                        <FooterStyle BackColor="#333333" />
                                        <PagerSettings Position="TopAndBottom" />
                                        <PagerStyle HorizontalAlign="Left" ForeColor="White" />
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

