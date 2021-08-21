<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptMonthWiseSupplierBill.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptMonthWiseSupplierBill" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            <%--var gvSpledger = $('#<%=this.gvSpledger.ClientID %>');
            var gvSPayment = $('#<%=this.gvSPayment.ClientID %>');
            var gvAllSupPay = $('#<%=this.gvAllSupPay.ClientID %>');
            var gvSPayment02 = $('#<%=this.gvSPayment02.ClientID %>');
            gvSpledger.Scrollable();
            gvSPayment.Scrollable();
            gvAllSupPay.Scrollable();
            gvSPayment02.Scrollable();--%>


            var gv = $('#<%=this.grvMWiseSupBill.ClientID %>');
            gv.Scrollable();
        }

    </script>




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">

                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                        <asp:TextBox ID="txtDateFrom" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDateFrom" Enabled="true"></cc1:CalendarExtender>


                                        <asp:Label ID="lbltoDate" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                        <asp:TextBox ID="txtDateto" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" ToolTip="(dd-MM-yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateto_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDateto" Enabled="true"></cc1:CalendarExtender>

                                    </div>
                                    <div class="col-md-2">
                                         <asp:LinkButton ID="lnkbtnok2" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkbtnok2_Click">Ok</asp:LinkButton>

                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName">Page</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass="ddlPage">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>




                                    </div>
                                    






                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="table-responsive table">
                                <asp:GridView ID="grvMWiseSupBill" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"  AllowPaging="true"
                                    ShowFooter="True" Width="832px" OnPageIndexChanging="grvMWiseSupBill_PageIndexChanging">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMSSlNo" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Name of Supplier">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMSName" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>

                                      
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        
                                         <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProName" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                             <FooterTemplate>
                                              <asp:Label ID="lblprjname" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="70px"> Total :  </asp:Label>
                                             </FooterTemplate>
                                             
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        

                                         <asp:TemplateField HeaderText="Order No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblorderno" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1")) %>'
                                                    Width="70"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Order </br>Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblamt" runat="server" Style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "orderamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblFordamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>

                                             <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Tax">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltax" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tax")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblFtax" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                             <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        
                                        <asp:TemplateField HeaderText="Advanced</br>Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lbladvamt" runat="server" Style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "advamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblFadv" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                              <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                         <%--<asp:TemplateField HeaderText="Partial</br>Payment">
                                            <ItemTemplate>
                                                <asp:Label ID="lblppayment" runat="server" Style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ppayment")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblFPpayment" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                              <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>--%>

                                         
                                        
                                        <asp:TemplateField HeaderText="Payment">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpay" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblFpay" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                             <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        
                                          
                                        <asp:TemplateField HeaderText="Balance">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBal" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balance")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFbalance" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                             <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
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

