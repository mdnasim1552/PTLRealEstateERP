<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="FeaProjectCost.aspx.cs" Inherits="RealERPWEB.F_02_Fea.FeaProjectCost" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style7
        {
            width: 75px;
        }

    .style50
    {
        color: white;
    }
        .style51
        {
            width: 57px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

        }
    </script>

 <table style="width: 912px">
        <tr>
            <td class="style43">
                <asp:Label ID="lblHeader" runat="server" BackColor="Blue" Font-Bold="True" 
                ForeColor="Yellow" style="font-weight: 700; color: #FFFF66; text-align: left" Text="PROJECT FEASIBILITY" 
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
            <table style="width:100%;">
                <tr>
                    <td colspan="14">
                        <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                            BorderWidth="1px" Width="995px">
                            <table style="width:100%;">
                                <tr>
                                    <td class="style7">
                                        <asp:Label ID="Label4" runat="server" CssClass="style50" Font-Bold="True" 
                                            Font-Size="12px" style="text-align: left" Text="Search:" Width="100px"></asp:Label>
                                    </td>
                                    <td class="style51">
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="txtboxformat" Width="80px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" onclick="lnkbtnSerOk_Click" 
                                            style="color: #FFFFFF; height: 17px; " TabIndex="3">Ok</asp:LinkButton>
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
                                </tr>
                                <tr>
                                    <td class="style7">
                                        <asp:CheckBox ID="chkAllRes" runat="server" AutoPostBack="True" 
                                            Font-Bold="True" Font-Size="12px" ForeColor="#660033" 
                                            oncheckedchanged="chkAllSInf_CheckedChanged" 
                                            style="color: #FFFFFF; text-align: left;" TabIndex="4" Text="Show All" 
                                            Width="100px" />
                                    </td>
                                    <td class="style51">
                                        &nbsp;</td>
                                    <td>
                                        <asp:Label ID="lmsg" runat="server" BackColor="Red" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" Style="color: #FFFFFF"></asp:Label>
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
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="14">
                        <asp:GridView ID="gvFeaPrjC" runat="server" AutoGenerateColumns="False" 
                            ShowFooter="True" Width="792px">
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnTotalCost" runat="server" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" onclick="lbtnTotalCost_Click">Total</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" 
                                            style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvItmCodc" runat="server" Height="16px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>' 
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project Name">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnfUpdateCost" runat="server" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" onclick="lbtnfUpdateCost_Click">Final Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpproject" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-size="11px" style="text-align: left" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mpactdesc"))%>' 
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                
                                <asp:TemplateField HeaderText="Flat No">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvflat" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-size="11px" style="text-align: left" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))%>' 
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Stored">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvstored" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-size="11px" style="text-align: left" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "stored"))%>' 
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" />
                                    
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Face">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvface" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-size="11px" style="text-align: left" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "face"))%>' 
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" />
                                 
                                </asp:TemplateField>
                              
                              

                                    
                                <asp:TemplateField HeaderText="Unit Size">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvusize" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-size="11px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>

                                         <FooterTemplate>
                                        <asp:Label ID="lgvFusize" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right"></asp:Label>
                                    </FooterTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>


                                 <asp:TemplateField HeaderText="Floor">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvfloor" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-size="11px" style="text-align: left" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flr"))%>' 
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" />
                                   
                                </asp:TemplateField>


                             
                                    
                                <asp:TemplateField HeaderText="Car Parking">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvcparking" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-size="11px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cparking")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Purchase Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvpurrate" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-size="11px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "purrate")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Purchase Amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvpuramt" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-size="11px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "puramt")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>

                                      <FooterTemplate>
                                        <asp:Label ID="lgvFpuramt" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right"></asp:Label>
                                    </FooterTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Car Parking">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvcpamt" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-size="11px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cpamt")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>

                                      <FooterTemplate>
                                        <asp:Label ID="lgvFcpamt" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>


                                 <asp:TemplateField HeaderText="Utility">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvutility" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-size="11px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utility")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>

                                    
                                      <FooterTemplate>
                                        <asp:Label ID="lgvFutility" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right"></asp:Label>
                                    </FooterTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>


                                  <asp:TemplateField HeaderText="BD">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvbdamt" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-size="11px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bdamt")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>

                                     <FooterTemplate>
                                        <asp:Label ID="lgvFbdamt" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right"></asp:Label>
                                    </FooterTemplate>


                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>



                                  <asp:TemplateField HeaderText="Commision">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvcommision" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-size="11px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comamt")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>

                                         <FooterTemplate>
                                        <asp:Label ID="lgvFcommision" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right"></asp:Label>
                                    </FooterTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>


                                 <asp:TemplateField HeaderText="Other Cost">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvothamt" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-size="11px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othamt")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                      <FooterTemplate>
                                        <asp:Label ID="lgvFothamt" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right"></asp:Label>
                                    </FooterTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>



                                
                                <asp:TemplateField HeaderText="Total Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtotalaamt" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-size="11px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toamt")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>

                                     <FooterTemplate>
                                        <asp:Label ID="lgvFtotalaamt" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right"></asp:Label>
                                    </FooterTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>







                                
                              
                            </Columns>
                            <FooterStyle BackColor="#333333" />
                            <PagerStyle HorizontalAlign="Center" />
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
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


