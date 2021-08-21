<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="MoneyRecptApprov.aspx.cs" Inherits="RealERPWEB.F_17_Acc.MoneyRecptApprov" %>

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
                        <asp:Panel ID="pnlMoneyRcptApp" runat="server" Style="margin-left: 20px;">

                            <asp:GridView ID="gvMoneyRcptApp" runat="server" AllowPaging="False"
                                AutoGenerateColumns="False"
                                ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea" Width="593px">
                                <RowStyle />

                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMSlNo" runat="server" Font-Bold="True"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AC.Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMAcCod" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                Width="49px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UsirCode" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcccMUcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="Acc. Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcMPactdesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="130px"></asp:Label>
                                        </ItemTemplate>
                                        <%-- <FooterTemplate>
                                                <asp:LinkButton ID="lbtnUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpdate_Click">Update</asp:LinkButton>
                                            </FooterTemplate>--%>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Description">

                                        <ItemTemplate>
                                            <asp:Label ID="lgcMUdesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ref.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvMrmrks" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MR. No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvMmrno" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>' Width="70"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bank Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvMBaName" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cheque No">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvMtotal" runat="server" Text="Total :" CssClass="btn btn-primary primaryBtn" Width="70"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvMCheNo" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pay Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvMCheDate" runat="server"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "paydate")).ToString("dd-MMM-yyyy") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>


                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvMFdramt" runat="server" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvMdrvamt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                    </asp:TemplateField>





                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <%--<asp:HyperLink ID="lnkbtnPrintcdep" runat="server" Style="display:inline-block;"><span class="glyphicon glyphicon-print"></span></asp:HyperLink>--%>
                                            <asp:LinkButton ID="lnkbtnMAppcdep" runat="server" ForeColor="Black" Font-Underline="false" Style="display: inline-block;" OnClick="lnkbtnMAppcdep_OnClick" OnClientClick="return confirm('Are you want to approve?')"><span class="glyphicon glyphicon-ok"></span>
                                            </asp:LinkButton>
                                            <%--  <asp:LinkButton ID="btnDelOrdercdep" runat="server" Style="display:inline-block;"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>--%>
                                        </ItemTemplate>
                                        <ItemStyle Width="80px" />
                                        <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                        <ControlStyle Width="80px" />

                                    </asp:TemplateField>



                                </Columns>

                                <FooterStyle BackColor="#F5F5F5" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                            </asp:GridView>


                        </asp:Panel>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

