<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptClientDOBMrrDay.aspx.cs" Inherits="RealERPWEB.F_24_CC.RptClientDOBMrrDay" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
            <div class="card mt-5 moduleItemWrpper">
                <div class="contentPart">
                    <div class="card-header">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="ViewProsClient" runat="server">
                        </asp:View>
                        <asp:View ID="ViewClntBrthDay" runat="server">
                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">
                                        <asp:Panel ID="Panel4" runat="server">
                                            
                                                <div class="col-lg-10 col-md-10 col-sm-10">
                                                    <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName" Text="Date:"></asp:Label>

                                                    <asp:TextBox ID="txtDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                                    <asp:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Enabled="True"
                                                        TargetControlID="txtDate" Format="dd-MMM-yyyy"></asp:CalendarExtender>

                                                   
                                                   <%-- <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>--%>

                                                </div>
                                            <div class="col-lg-2 col-md-2 col-sm-10">
                                                 <asp:LinkButton ID="lbtnShowBrthDay" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnShowBrthDay_Click">Ok</asp:LinkButton>

                                                </div>

                                            
                                        </asp:Panel>
                                   
                                </fieldset>
                            </div>
                            </div>

                            <div class="table table-responsive">
                                <asp:GridView ID="gvClientBrthDay" runat="server" AutoGenerateColumns="False" Width="186px" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True">
                                   
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvslno" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Client Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvClientName" runat="server" Font-Bold="True" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name")) %>' Width="160px"
                                                    ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Nick Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvClientnNameb" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nickname")) %>'
                                                    Width="120px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Address">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpredAddress" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preaddress")) %>'
                                                    Width="160px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Home Phone">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvhomephoneb" runat="server" Width="80px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "homephone")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mobile">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMobNo" runat="server" Width="100px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobile")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="Office Phone">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvoffPhoneb" runat="server" Width="80px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "offphone")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Email">
                                            <ItemTemplate>
                                                <a href="Mailto:<%# Eval("email") %>">
                                                    <asp:Label ID="lgvemail" runat="server" Width="140px" Text='<%# Eval("email") %>'></asp:Label></a>
                                                <%-- <asp:Label ID="lgvemailb" runat="server" Width="140px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'></asp:Label>--%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                    </Columns>
                                   <FooterStyle CssClass="grvFooter"/>
<EditRowStyle />
<AlternatingRowStyle />
<PagerStyle CssClass="gvPagination" />
<HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>

                            </div>
                        </asp:View>
                        <asp:View ID="ViewClientMrrgDay" runat="server">
                            <asp:Panel ID="Panel1" runat="server">
                                <div class="form-group">
                                    <div class="col-md-10 pading5px asitCol10">
                                        <asp:Label ID="LblmDate" runat="server" CssClass="lblTxt lblName" Text="Date:"></asp:Label>

                                        <asp:TextBox ID="TxtmDate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="TxtmDate"
                                            Format="dd-MMM-yyyy"></asp:CalendarExtender>

                                        <asp:LinkButton ID="lbtnShowMrgDay" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnShowMrgDay_Click">Ok</asp:LinkButton>

                                        <asp:Label ID="LblmMsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>

                                    </div>

                                </div>
                            </asp:Panel>



                            <asp:GridView ID="gvClientMrgDay" runat="server" AutoGenerateColumns="False" Width="186px" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True">
                               
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvslno" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Client Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvClientName" runat="server" Font-Bold="True" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name")) %>' Width="160px"
                                                ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Nick Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvClientnNamem" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nickname")) %>'
                                                    Width="120px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Address">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpredAddress" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preaddress")) %>'
                                                Width="160px" ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Home Phone">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvconNo" runat="server" Width="80px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "homephone")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mobile">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvMobNo" runat="server" Width="100px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobile")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Office Phone">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvoffPhonem" runat="server" Width="80px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "offphone")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Email">
                                        <ItemTemplate>
                                            <a href="Mailto:<%# Eval("email") %>">
                                                <asp:Label ID="lgvemail" runat="server" Width="140px" Text='<%# Eval("email") %>'></asp:Label></a>
                                            <%--  <asp:Label ID="lgvemailm" runat="server" Width="140px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'></asp:Label>--%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                               <FooterStyle CssClass="grvFooter"/>
<EditRowStyle />
<AlternatingRowStyle />
<PagerStyle CssClass="gvPagination" />
<HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </asp:View>


                    </asp:MultiView>
                </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
