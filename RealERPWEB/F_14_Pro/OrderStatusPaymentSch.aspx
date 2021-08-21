<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="OrderStatusPaymentSch.aspx.cs" Inherits="RealERPWEB.F_14_Pro.OrderStatusPaymentSch" %>

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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-group">
                                <div class="col-md-4 pading5px asitCol4">
                                    <asp:Label ID="lbldatefrm" runat="server" CssClass="smLbl_to" Text="From"></asp:Label>
                                    <asp:TextBox ID="txtFDate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtFDate_CalendarExtender" runat="server"
                                        Format="dd-MMM-yyyy" TargetControlID="txtFDate" Enabled="true"></cc1:CalendarExtender>


                                    <asp:Label ID="lbldateto" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                    <asp:TextBox ID="txttodate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" ToolTip="(dd-MM-yyyy)"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                        Format="dd-MMM-yyyy" TargetControlID="txttodate" Enabled="true"></cc1:CalendarExtender>
                                    
                                     <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_OnClick">Ok</asp:LinkButton>
                                </div>
                            </div>
                        </fieldset>
                        
                        
                        
                          <asp:GridView ID="gvOrederStatus" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" Width="431px" CssClass="table table-striped table-hover table-bordered grvContentarea">
                            <RowStyle Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                         <asp:Label ID="lblgvSlNo0" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                              

                                 <asp:TemplateField HeaderText="Order No.">
                                      
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lblorderno" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="12px"
                                            Style="text-align: left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                            Width="90px" ></asp:Label>
                                    </ItemTemplate>
                                     
                                      
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Order Date">
                                    
                                    
                                     
                                    <ItemTemplate>
                                        <asp:Label ID="lblorderdat" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: left; background-color: Transparent"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "orderdat")).ToString("dd-MMM-yyyy") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                      
                                </asp:TemplateField>
                                
                                 <asp:TemplateField HeaderText="Project Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprj" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                      
                                </asp:TemplateField>
                                
                                  <asp:TemplateField HeaderText="Supplier Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsup" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                      
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="12px"
                                            Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "orderamt")).ToString("#,##0.00;#,##0.00; ") %>'
                                            Width="90px" ></asp:Label>
                                    </ItemTemplate>
                                      <%--<FooterTemplate>
                                                <asp:Label ID="lblFamount" runat="server" Font-Bold="True" Font-Size="12px" Width="90px"
                                                    ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>--%>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                
                                 <asp:TemplateField HeaderText="Paid Amt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblpaid" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="12px"
                                            Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payment")).ToString("#,##0.00;#,##0.00; ") %>'
                                            Width="90px" ></asp:Label>
                                    </ItemTemplate>
                                      <%--<FooterTemplate>
                                                <asp:Label ID="lblFamount" runat="server" Font-Bold="True" Font-Size="12px" Width="90px"
                                                    ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>--%>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                
                                
                                 <asp:TemplateField HeaderText="Payable Amt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblpayable" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="12px"
                                            Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payable")).ToString("#,##0.00;#,##0.00; ") %>'
                                            Width="90px" ></asp:Label>
                                    </ItemTemplate>
                                      <%--<FooterTemplate>
                                                <asp:Label ID="lblFamount" runat="server" Font-Bold="True" Font-Size="12px" Width="90px"
                                                    ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>--%>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                                   
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                        
                        

                    </div>


                </div>
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

