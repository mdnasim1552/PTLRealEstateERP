<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="YearlyPlanningSt.aspx.cs" Inherits="RealERPWEB.F_05_Busi.YearlyPlanningSt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


   
    
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
            <div class="card">
                <div class="card-header">
                    <div class="row">

                      

                                    <div class="col-md-2">
                                      <asp:Label ID="Label1" runat="server" CssClass="form-label" Text="Year:"></asp:Label>

                                        <asp:DropDownList ID="ddlyear" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"
                                            TabIndex="11">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-1" style="margin-top:22px;">
                                        <asp:LinkButton ID="lbtnYearbgd" runat="server" Text="Ok" OnClick="lbtnYearbgd_Click" CssClass="btn btn-sm btn-primary"></asp:LinkButton>
                                    </div>
                                 
                         
                            

                                    <div class="col-md-1">
                                    <asp:Label ID="lblPage" runat="server" CssClass="form-label" Text="Page Size" Visible="False"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm "
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False">
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
                                    </div>
                                    <div class="col-md-4 d-none">
                                        <div class="msgHandSt">
                                            <asp:Label ID="lblmsg02" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="ConfirmMessage" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                               

                            </div>
                    

                        <asp:MultiView ID="MultiView1" runat="server">

                            <asp:View ID="ViewYearlyIncome" runat="server">
                                 <div class="table-responsive">
                                 <asp:GridView ID="grvYearltIncome" runat="server" AutoGenerateColumns="False" ShowFooter="True" OnPageIndexChanging="grvYearltIncome_PageIndexChanging"
                                    OnRowDataBound="grvYearltIncome_RowDataBound" CssClass="table-striped table-responsive table-hover table-bordered grvContentarea">

                              
                                   <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="serialnoidy" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                    Width="10px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDesc" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                             <FooterTemplate>
                                                <table>
                                                    <tr>
                                                        <td><label style="text-align: right;font-weight: bold;font-size: 12px">Total Inflow=</label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><label style="text-align: right;font-weight: bold;font-size: 12px">Total Outflow=</label></td>
                                                    </tr>
                                                </table>

                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvperc" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perc")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvTAmt" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTotalInflow" runat="server"  Style="text-align: right"  Font-Size="12px" Width="80"  Font-Bold="True" 
                                                   ></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                             <asp:Label ID="lblTotalOutflow" style="text-align: right" runat="server" Width="80"  Font-Bold="True"  Font-Size="12px"  
                                                   ></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                             <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Jan">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAmt1" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt1")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                             <FooterTemplate>
                                                 <table>
                                                     <tr>
                                                         <td>
                                                             <asp:Label ID="lgvFamt1i" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td>
                                                             <asp:Label ID="lgvFamt1o" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                                         </td>
                                                     </tr>
                                                 </table>
                                                
                                            </FooterTemplate>
                                           
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Feb">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvamt2" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt2")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <table>
                                                    <tr><td>
                                                        <asp:Label ID="lgvFamt2i" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                                    </td></tr>
                                                    <tr><td>
                                                        <asp:Label ID="lgvFamt2o" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                                    </td></tr>
                                                </table>
                                                
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mar">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvamt3" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt3")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                 <table>
                                                    <tr><td>
                                                        <asp:Label ID="lgvFamt3i" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                                    </td></tr>
                                                    <tr><td>
                                                        <asp:Label ID="lgvFamt3o" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                                    </td></tr>
                                                </table>
                                                
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Apr">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvamt4" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt4")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                 <table>
                                                    <tr><td>
                                                         <asp:Label ID="lgvFamt4i" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                                    </td></tr>
                                                    <tr><td>
                                                         <asp:Label ID="lgvFamt4o" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                                    </td></tr>
                                                </table>
                                               
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="May">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvamt5" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt5")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                 <table>
                                                    <tr><td>
                                                         <asp:Label ID="lgvFamt5i" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                                    </td></tr>
                                                    <tr><td>
                                                         <asp:Label ID="lgvFamt5o" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                                    </td></tr>
                                                </table>
                                               
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Jun">
                                            <ItemTemplate>
                                                
                                                <asp:Label ID="lblgvamt6" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt6")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                 <table>
                                                    <tr><td>
                                                        <asp:Label ID="lgvFamt6i" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                                    </td></tr>
                                                    <tr><td>
                                                        <asp:Label ID="lgvFamt6o" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                                    </td></tr>
                                                </table>
                                                
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Jul">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvamt7" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt7")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                 <table>
                                                    <tr><td>
                                                        <asp:Label ID="lgvFamt7i" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                                    </td></tr>
                                                    <tr><td>
                                                        <asp:Label ID="lgvFamt7o" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                                    </td></tr>
                                                </table>
                                                
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Aug">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvamt8" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt8")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                 <table>
                                                    <tr><td>
                                                         <asp:Label ID="lgvFamt8i" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                                    </td></tr>
                                                    <tr><td>
                                                         <asp:Label ID="lgvFamt8o" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                                    </td></tr>
                                                </table>
                                               
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sep">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvamt9" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt9")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                 <table>
                                                    <tr><td><asp:Label ID="lgvFamt9i" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right" Width="80px"></asp:Label></td></tr>
                                                    <tr><td>
                                                        <asp:Label ID="lgvFamt9o" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                                    </td></tr>
                                                </table>
                                                
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Oct">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvamt10" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt10")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                 <table>
                                                    <tr><td><asp:Label ID="lgvFamt10i" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right" Width="80px"></asp:Label></td></tr>
                                                    <tr><td>
                                                        <asp:Label ID="lgvFamt10o" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                                    </td></tr>
                                                </table>
                                                
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nov">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvamt11" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt11")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                 <table>
                                                    <tr><td>
                                                        <asp:Label ID="lgvFamt11i" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                                    </td></tr>
                                                    <tr><td>
                                                        <asp:Label ID="lgvFamt11o" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                                    </td></tr>
                                                </table>
                                                
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dec">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvamt12" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt12")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                 <table>
                                                    <tr>
                                                        <td>
                                                             <asp:Label ID="lgvFamt12i" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                             <asp:Label ID="lgvFamt12o" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                               
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                   <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                                     </div>


                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

