<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptFindClient.aspx.cs" Inherits="RealERPWEB.F_21_MKT.RptFindClient" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="container moduleItemWrpper">
        <div class="contentPart">
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
            <div class="row">
                <%--     <fieldset class="scheduler-border fieldset_A">--%>
                <div class="form-horizontal">
                    <asp:Panel ID="Panel2" runat="server">
                        <div class="form-group">
                            <div class="col-md-12  pading5px">
                                <asp:MultiView ID="MultiView1" runat="server">
                                    <asp:View ID="ViewProsClient" runat="server">
                                        <asp:Panel ID="Panel5" runat="server">
                                            <div class="form-group">
                                                <div class="col-md-10  pading5px  asitCol10">
                                                    <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName" Text="Page Size:"></asp:Label>
                                                    <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass=" ddlPage">
                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                        <asp:ListItem Value="15">15</asp:ListItem>
                                                        <asp:ListItem Value="20">20</asp:ListItem>
                                                        <asp:ListItem Value="30">30</asp:ListItem>
                                                        <asp:ListItem Value="50">50</asp:ListItem>
                                                        <asp:ListItem Value="100">100</asp:ListItem>
                                                        <asp:ListItem Value="150">150</asp:ListItem>
                                                        <asp:ListItem Value="200">200</asp:ListItem>
                                                        <asp:ListItem Value="300">300</asp:ListItem>
                                                        <asp:ListItem Value="600">600</asp:ListItem>
                                                        <asp:ListItem Value="900">900</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>





                                            <%--<table style="width: 100%;">
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td class="style20">
                                                <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="color: #FFFFFF; text-align: right;" Text="Page Size:" Width="80px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                                    BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF"
                                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Width="105px">
                                                    <asp:ListItem Value="10">10</asp:ListItem>
                                                    <asp:ListItem Value="15">15</asp:ListItem>
                                                    <asp:ListItem Value="20">20</asp:ListItem>
                                                    <asp:ListItem Value="30">30</asp:ListItem>
                                                    <asp:ListItem Value="50">50</asp:ListItem>
                                                    <asp:ListItem Value="100">100</asp:ListItem>
                                                    <asp:ListItem Value="150">150</asp:ListItem>
                                                    <asp:ListItem Value="200">200</asp:ListItem>
                                                    <asp:ListItem Value="300">300</asp:ListItem>
                                                    <asp:ListItem Value="600">600</asp:ListItem>
                                                    <asp:ListItem Value="900">900</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                           
                                    </table>--%>
                                        </asp:Panel>

                                        <div class="table table-responsive">
                                            <asp:GridView ID="gvProsClient" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" Width="901px" AllowPaging="True"
                                                OnPageIndexChanging="gvProsClient_PageIndexChanging">

                                                <Columns>

                                                    <asp:TemplateField HeaderText="Sl No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvslno" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                                Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Client Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvClientName" runat="server" Font-Bold="True"
                                                                Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name")) %>' Width="160px"
                                                                ForeColor="Black"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Nick Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvClientName" runat="server"
                                                                Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nickname")) %>' Width="120px"
                                                                ForeColor="Black"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Address">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvPerAddress" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preaddress")) %>'
                                                                Width="160px" ForeColor="Black"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Home Phone">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvconNo" runat="server" Width="80px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "homephone")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Mobile">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvMobNo" runat="server" Width="80px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobile")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Office Phone">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvoffPhone" runat="server" Width="80px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "offphone")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Email">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvemail" runat="server" Width="140px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" />
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
                                    <asp:View ID="ViewClntBrthDay" runat="server">
                                        <asp:Panel ID="Panel4" runat="server">
                                            <fieldset class="scheduler-border fieldset_A">
                                                <div class="form-group">
                                                    <div class="col-md-10  pading5px">
                                                        <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName" Text="Date:"></asp:Label>
                                                        <asp:TextBox ID="txtDate" runat="server" Width="80px" CssClass="inputtextbox"></asp:TextBox>
                                                        <asp:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                                            Enabled="True" TargetControlID="txtDate" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                                        <asp:LinkButton ID="lbtnShowBrthDay" CssClass="btn btn-primary primaryBtn" runat="server" OnClick="lbtnShowBrthDay_Click" TabIndex="1">Ok</asp:LinkButton>
                                                        <asp:Label ID="lblmsg" runat="server" CssClass="btn-danger btn  primaryBtn"></asp:Label>
                                                    </div>
                                                </div>
                                            </fieldset>



                                            <%--<tr>
                                                    <td class="style9">&nbsp;</td>
                                                    <td class="style18">
                                                        <asp:Label ID="lblDate" runat="server" CssClass="label2" Text="Date:"
                                                            Width="24px" Height="16px"></asp:Label>
                                                    </td>
                                                    <td class="style11">
                                                        <asp:TextBox ID="txtDate" runat="server" Width="80px" Height="16px"
                                                            BorderStyle="None"></asp:TextBox>

                                                        <asp:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                                            Enabled="True" TargetControlID="txtDate" Format="dd-MMM-yyyy"></asp:CalendarExtender>

                                                    </td>
                                                    <td class="style16">

                                                        <asp:LinkButton ID="lbtnShowBrthDay" runat="server" BackColor="#003366"
                                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                            Font-Size="12px" Height="19px" Style="text-align: center; color: #FFFFFF;"
                                                            Width="23px" OnClick="lbtnShowBrthDay_Click">Ok</asp:LinkButton>

                                                        <td class="style49"></td>
                                                        <td class="style19"></td>
                                                        <td class="style18"></td>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <asp:Label ID="lblmsg" runat="server" BackColor="Red" Font-Bold="True"
                                                                Font-Size="12px" ForeColor="White"></asp:Label>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                </tr>--%>
                                        </asp:Panel>
                                        <div class="table table-responsive">
                                            <asp:GridView ID="gvClientBrthDay" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                Width="186px" ShowFooter="True">

                                                <Columns>

                                                    <asp:TemplateField HeaderText="Sl No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvslno" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                                Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Client Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvClientName" runat="server" Font-Bold="True"
                                                                Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name")) %>' Width="160px"
                                                                ForeColor="Black"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Nick Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvClientnNameb" runat="server"
                                                                Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nickname")) %>' Width="120px"
                                                                ForeColor="Black"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Address">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvpredAddress" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preaddress")) %>'
                                                                Width="160px" ForeColor="Black"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Home Phone">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvhomephoneb" runat="server" Width="80px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "homephone")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" />

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Mobile">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvMobNo" runat="server" Width="100px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobile")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Office Phone">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvoffPhoneb" runat="server" Width="80px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "offphone")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="btnSendMail" runat="server" Font-Bold="True" CssClass="btn btn-warning"
                                                                Style="text-decaration: none;" Font-Size="12px" ForeColor="Black">Send Mail</asp:LinkButton>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Email">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvemailb" runat="server" Width="140px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="btnSendSms" runat="server" Font-Bold="True" CssClass="btn btn-primary "
                                                                Style="text-decaration: none;" Font-Size="12px" ForeColor="Black">Send SMS</asp:LinkButton>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" />
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

                                    <asp:View ID="ViewClientMrrgDay" runat="server">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <fieldset class="scheduler-border fieldset_A">
                                                <div class="form-group">
                                                    <div class="col-md-10  pading5px">
                                                        <asp:Label ID="LblmDate" runat="server" CssClass="lblTxt lblName" Text="Date:"></asp:Label>
                                                        <asp:TextBox ID="TxtmDate" runat="server" Width="80px" CssClass="inputtextbox"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server"
                                                            Enabled="True" TargetControlID="TxtmDate" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                                        <asp:LinkButton ID="lbtnShowMrgDay" CssClass="btn btn-primary primaryBtn" runat="server" OnClick="lbtnShowMrgDay_Click" TabIndex="1">Ok</asp:LinkButton>
                                                        <asp:Label ID="LblmMsg" runat="server" CssClass="btn-danger btn  primaryBtn"></asp:Label>
                                                    </div>
                                                </div>
                                            </fieldset>
                                            
                                        </asp:Panel>

                                        <asp:GridView ID="gvClientMrgDay" runat="server" AutoGenerateColumns="False"
                                            CssClass=" table-striped table-hover table-bordered grvContentarea"
                                            Width="186px" ShowFooter="True">
                                            <Columns>

                                                <asp:TemplateField HeaderText="Sl No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvslno" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                            Width="40px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Client Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvClientName" runat="server" Font-Bold="True"
                                                            Style="text-align: left"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name")) %>' Width="160px"
                                                            ForeColor="Black"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Nick Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvClientnNamem" runat="server"
                                                            Style="text-align: left"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nickname")) %>' Width="120px"
                                                            ForeColor="Black"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Address">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvpredAddress" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preaddress")) %>'
                                                            Width="160px" ForeColor="Black"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Home Phone">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvconNo" runat="server" Width="80px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "homephone")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvMobNo" runat="server" Width="100px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobile")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="btnSendMail" runat="server" Font-Bold="True" CssClass="btn btn-success"
                                                            Style="text-decaration: none;" Font-Size="12px" ForeColor="Black">Send Mail</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Office Phone">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvoffPhonem" runat="server" Width="80px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "offphone")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="btnSendSms" runat="server" Font-Bold="True" CssClass="btn btn-success "
                                                            Style="text-decaration: none;" Font-Size="12px" ForeColor="Black">Send SMS</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvemailm" runat="server" Width="140px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>

                                    </asp:View>
                                </asp:MultiView>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <%--  </fieldset>--%>
            </div>
        </div>
    </div>


    <%--     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>


