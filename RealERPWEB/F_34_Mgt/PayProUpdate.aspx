<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="PayProUpdate.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.PayProUpdate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat="server">


</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-4  pading5px">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass=" smLbl_to"
                                            Text="Req.Date"></asp:Label>

                                        <asp:TextBox ID="txtfrmdate" runat="server" AutoCompleteType="Disabled"
                                            CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurReqDate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>

                                        <asp:Label ID="lbltodate" runat="server" CssClass=" smLbl_to"
                                            Text="To"></asp:Label>

                                        <asp:TextBox ID="txttodate" runat="server" AutoCompleteType="Disabled"
                                            CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>



                                        <asp:LinkButton ID="lnkOk" runat="server" CssClass="btn btn-primary primaryBtn"
                                            OnClick="lnkOk_Click">Ok</asp:LinkButton>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>

                    <asp:GridView ID="gvAPPr" runat="server" AutoGenerateColumns="False" BackColor="#F0F0F0"
                        PageSize="15" ShowFooter="True" Width="540px" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        OnRowDeleting="gvAPPr_RowDeleting" OnRowDataBound="gvAPPr_RowDataBound">
                        <PagerSettings Position="TopAndBottom" />

                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" />

                            <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="gvdate" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                        BorderStyle="None" BorderWidth="1px" CssClass="GridLebelL" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bildat"))%>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pay. Proposal No," ItemStyle-HorizontalAlign="Center"
                                Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="gvppno" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                        BorderStyle="None" BorderWidth="1px" CssClass="GridLebelL" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ppno"))%>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Proposal No" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="gvppno1" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                        BorderStyle="None" BorderWidth="1px" CssClass="GridLebelL" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ppno1"))%>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Software Bill No" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="gvbillno" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                        BorderStyle="None" BorderWidth="1px" CssClass="GridLebelL" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno"))%>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Manual Bill No" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="gvrefno" runat="server" BackColor="Transparent" BorderColor="Transparent" ReadOnly="true"
                                        BorderStyle="None" BorderWidth="1px" CssClass="GridLebelL" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))%>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Party/Head" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="gvparname" runat="server" BackColor="Transparent" BorderColor="Transparent" ReadOnly="true"
                                        BorderStyle="None" BorderWidth="1px" CssClass="GridLebelL" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "parname"))%>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="14px" FooterStyle-HorizontalAlign="Right"
                                HeaderText="Project" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="gvproname" runat="server" BackColor="Transparent" BorderColor="Transparent" ReadOnly="true"
                                        BorderStyle="None" BorderWidth="1px" CssClass="GridLebelL" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "proname"))%>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" Font-Size="14px" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="14px" FooterStyle-HorizontalAlign="Right"
                                HeaderText="Amount" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="gvamt" runat="server" BackColor="Transparent" BorderColor="Transparent" ReadOnly="true"
                                        BorderStyle="None" BorderWidth="1px" CssClass="GridTextbox" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Purpose">
                                <ItemTemplate>
                                    <asp:Label ID="gvpurpes" runat="server" BackColor="Transparent" BorderColor="Transparent" ReadOnly="true"
                                        BorderStyle="None" BorderWidth="1px" CssClass="GridLebelL" Height="21px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "purpes")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle  ForeColor="#000" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Signatory" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="gvsign" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                        BorderStyle="None" BorderWidth="1px" CssClass="GridLebelL" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrappname"))%>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="gvchk" runat="server" Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "apprv"))=="1" %>'
                                        Width="20px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbok" runat="server" Width="30px" CommandArgument="lbok" Font-Size="12px"
                                        OnClick="lbok_Click">OK</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="User">
                                <ItemTemplate>
                                    <asp:Label ID="gvuser" runat="server" BackColor="Transparent" BorderColor="Transparent" ReadOnly="true"
                                        BorderStyle="None" BorderWidth="1px" CssClass="GridLebelL" Height="21px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "userid")) %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle  ForeColor="#000" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                            </asp:TemplateField>
                        </Columns>

                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>

                    <hr />
                    <asp:GridView ID="gvAPPRPT" runat="server" AutoGenerateColumns="False"
                        PageSize="15" ShowFooter="True" Width="540px"
                        CssClass=" table-striped table-hover table-bordered grvContentarea"
                        OnRowDataBound="gvAPPRPT_RowDataBound">
                        <PagerSettings Position="TopAndBottom" />

                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblrserialnoid" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="gvrdate" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                        CssClass="GridLebelL"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bildat"))%>'
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Proposal No,"
                                ItemStyle-HorizontalAlign="Center" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="gvrppno" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                        CssClass="GridLebelL"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ppno"))%>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pay. Proposal No"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="gvrppno1" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                        CssClass="GridLebelL"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ppno1"))%>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Software Bill No" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="gvrbillno" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                        CssClass="GridLebelL"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno"))%>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Manual Bill No" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="gvrrefno" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                        CssClass="GridLebelL" ReadOnly="true"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))%>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Party/Head" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="gvrparname" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                        CssClass="GridLebelL" ReadOnly="true"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "parname"))%>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="14px"
                                FooterStyle-HorizontalAlign="Right" HeaderText="Project"
                                ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="gvrproname" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                        CssClass="GridLebelL" ReadOnly="true"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "proname"))%>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" Font-Size="14px" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="14px"
                                FooterStyle-HorizontalAlign="Right" HeaderText="Approved Amount"
                                ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="gvramt" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                        CssClass="GridTextbox" ReadOnly="true"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>

                                <FooterStyle Font-Bold="True" Font-Size="14px" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Purpose">
                                <ItemTemplate>
                                    <asp:Label ID="gvrpurpes" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                        CssClass="GridLebelL" Height="21px" ReadOnly="true"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "purpes")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle  ForeColor="#000" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Signatory" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="gvsign" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                        BorderStyle="None" BorderWidth="1px" CssClass="GridLebelL" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrappname"))%>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="App. User">
                                <ItemTemplate>
                                    <asp:Label ID="gvruser" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                        CssClass="GridLebelL" Height="21px" ReadOnly="true"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "userid")) %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle  ForeColor="#000" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="200px" />
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

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

