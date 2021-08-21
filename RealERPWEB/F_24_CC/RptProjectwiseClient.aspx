<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptProjectwiseClient.aspx.cs" Inherits="RealERPWEB.F_24_CC.RptProjectwiseClient" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card card-fluid">
        <div class="card-body" style="min-height: 600px;">

            <div class="row">
                     <div class="col-md-5 pading5px ">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control inputTxt chzn-select" TabIndex="3">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click" TabIndex="4">Ok</asp:LinkButton>

                                    </div>

            </div>
            <div class="row" style="padding-top: 15px">

                <div class="col-md-1">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlSmsMail" runat="server" OnSelectedIndexChanged="ddlSmsMail_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control">
                            <asp:ListItem Value="01">SMS</asp:ListItem>
                            <asp:ListItem Value="02">Mail</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlSMSMAILTEMP" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSMSMAILTEMP_SelectedIndexChanged" CssClass="form-control">
                    </asp:DropDownList>

                </div>

                <div class="col-md-6">
                    <asp:Label ID="lblTemMSg" runat="server" Text="This is Test for Template "></asp:Label>
                </div>

                <div class="col-md-1">
                    <asp:LinkButton ID="lnkSend" runat="server" CssClass="btn btn-success btn-sm" OnClick="lnkSend_Click" Text="Send"></asp:LinkButton>

                </div>

            </div>

              <asp:GridView ID="gvLetter" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="258px">
                                <PagerSettings Position="Top" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvletcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "letcode")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkletter" runat="server"
                                                Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chk"))=="True" %>' />
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Letter Information">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvteamdesc0" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "letdesc")) %>'
                                                Width="150px"></asp:Label>
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
             <asp:GridView ID="gvClientLetter" runat="server" AllowPaging="True"
                                AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True"
                                Width="614px">
                                <PagerSettings Position="Top" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo5" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAllfrm" runat="server" AutoPostBack="True"
                                                OnCheckedChanged="chkAllfrm_CheckedChanged" Text="ALL" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkletterc" runat="server"
                                                Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chk"))=="True" %>' />
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvletcodec" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Client Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvclinetname" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Address">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvclientAddress" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paddress")) %>'
                                                Width="350px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mobile">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvclientMob" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Email">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvclientemail" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>' Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <%-- <asp:CheckBox ID="chkassign" runat="server"
                                        Width="20px" />--%>
                    <asp:CheckBox ID="chkstatus" runat="server"
                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "smstatus"))=="1" %>'
                        Width="20px" />
                </ItemTemplate>
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
</asp:Content>

