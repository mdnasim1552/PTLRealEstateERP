<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptMonthSupllBill.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptMonthSupllBill" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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



        }

    </script>




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
                                <asp:GridView ID="grvMSuppBill" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea" AllowPaging="true"
                                    ShowFooter="True" Width="832px" OnPageIndexChanging="grvMSuppBill_PageIndexChanging">
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
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purpose">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMSPurpose" runat="server"
                                                    Text=''
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Bill">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMSTotalBill" runat="server" Style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sbillamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblttlBill" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="100px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Tax">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMSTax" runat="server" Style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttaxamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblttlTax" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="100px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Paid Bill">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpbillamt" runat="server" Style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pbillamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblFpbillamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="100px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        
                                        <asp:TemplateField HeaderText="Net Bill">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgbMSNetbill" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netbill")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblttlNetbill" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="100px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>
                            </div>


                </div>
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

