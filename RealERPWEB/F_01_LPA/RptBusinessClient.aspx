<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptBusinessClient.aspx.cs" Inherits="RealERPWEB.F_01_LPA.RptBusinessClient" %>

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
    <div class="card mt-4">
        <div class="card-body" >
            <div class="row mb-4">
                <div class="col-md-1">


                    <asp:DropDownList ID="ddlyearland" runat="server" CssClass="custom-select chzn-select" AutoPostBack="true" TabIndex="2" OnSelectedIndexChanged="ddlyearland_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
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
            </div>
        </div>
    <div class="card mt-4">
        <div class="card-body" >
            <div class="row mb-4">

            <asp:GridView ID="gvSummary" runat="server" AutoGenerateColumns="False"
                ShowFooter="True" CssClass="table-condensed table-hover table-bordered grvContentarea">
                <RowStyle />
                <Columns>

                    <asp:TemplateField HeaderText="Sl">
                        <ItemTemplate>
                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                Style="text-align: right"
                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Code" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblDealid" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dealcode")) %>'></asp:Label>
                            <asp:Label ID="lsircode" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode")) %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="LID">
                        <ItemTemplate>
                            <asp:Label ID="lsircode1" runat="server" Width="60px" Font-Size="10px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode1")) %>'></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Generated">
                        <ItemTemplate>
                            <asp:Label ID="lblgenerated" runat="server" Width="60px" Font-Size="10px"
                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "generated")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Followup">
                        <ItemTemplate>
                            <asp:Label ID="lbllfollowup" runat="server" Width="60px" Font-Size="10px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lfollowup")) %>'></asp:Label>

                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>

                            <asp:HyperLink ID="hlnkbtnEntry" runat="server" ToolTip="New Followup" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>

                            </asp:HyperLink>


                        </ItemTemplate>
                        <ItemStyle Width="2px" />
                        <HeaderStyle HorizontalAlign="Center" Width="10px" VerticalAlign="Top" />
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="Owner Details">
                        <ItemTemplate>
                            <asp:Label ID="lowner" runat="server" Width="170px" Font-Size="10px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ownname")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Property Details">
                        <ItemTemplate>
                            <asp:Label ID="ldesc" runat="server" Width="200px" Font-Size="10px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="Associate">
                        <ItemTemplate>
                            <asp:Label ID="lassoc" runat="server" Width="90px" Font-Size="10px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assoc")) %>'></asp:Label>
                        </ItemTemplate>


                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Dealing">
                        <ItemTemplate>
                            
                            <asp:Label ID="lblbusername" runat="server" Width="90px" Font-Size="10px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "delname")) %>'></asp:Label>
                        </ItemTemplate>


                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lbllstatus" runat="server" Width="60px" Font-Size="10px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstatus")) %>'></asp:Label>
                        </ItemTemplate>


                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Prio">
                        <ItemTemplate>
                            <asp:Label ID="lprio" runat="server" Width="90px" Font-Size="10px"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prio")) %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>




                    <asp:TemplateField HeaderText="Last </br>Follow. </br>Dur.">
                        <ItemTemplate>
                            <asp:Label ID="lblfollowdday" runat="server" Width="20px" Font-Size="10px" Style="text-align: center;"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "followdday")) %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Phone">
                        <ItemTemplate>
                            <asp:Label ID="lblPhone" runat="server" Width="90px" Font-Size="10px" Style="text-align: center;"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cphone")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <asp:Label ID="lblMail" runat="server" Width="90px" Font-Size="10px" Style="text-align: center;"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cmail")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                           
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
       </div>
             </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

