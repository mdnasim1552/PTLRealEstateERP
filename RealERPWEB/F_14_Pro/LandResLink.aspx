<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LandResLink.aspx.cs" Inherits="RealERPWEB.F_14_Pro.LandResLink" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });

            $('.chzn-select').chosen({ search_contains: true });


        });

        function pageLoaded(parameters) {

        }

    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">

                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="col-md-2">
                                <asp:Label ID="lblConTrolCode" runat="server" CssClass="lblTxt lblName">Payable Code</asp:Label>
                            </div>
                            <div class="col-md-4 pading5px asitCol4" style="margin-left: -50px;">
                                <asp:DropDownList ID="ddlPPayableCode" runat="server" CssClass="form-control inputTxt" >
                                </asp:DropDownList>

                            </div>

                            <div class="col-md-1 pading5px asitCol3">
                                <div class="colMdbtn pading5px">
                                    <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" Style="margin-left: 10px;" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                </div>

                            </div>
                            <div class="col-md-3 pading5px asitCol3">
                                <div class="colMdbtn pading5px">

                                    <asp:Label ID="lblmsg1" CssClass="btn-danger btn disabled primaryBtn" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>
                        </fieldset>
                        <asp:Panel ID="Panel2" runat="server" Visible="False">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-2">

                                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Main Head</asp:Label>
                                        </div>
                                        <div class="col-md-4 pading5px asitCol4" style="margin-left: -50px;">

                                            <asp:DropDownList ID="ddlmaincode" runat="server" CssClass="chzn-select form-control inputTxt" OnSelectedIndexChanged="ddlmaincode_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-2">

                                            <asp:Label ID="lblUser1" runat="server" CssClass="lblTxt lblName">Land Code</asp:Label>
                                        </div>
                                        <div class="col-md-4 pading5px asitCol4" style="margin-left: -50px;">

                                            <asp:DropDownList ID="ddlLandCode" runat="server" CssClass="chzn-select form-control inputTxt">
                                            </asp:DropDownList>

                                        </div>
                                        <div class="col-md-1 pading5px">

                                            <div class="colMdbtn pading5px">
                                                <asp:LinkButton ID="lbtnSelectSupl1" runat="server" Style="margin-left: 10px;" CssClass="btn btn-primary primaryBtn checkbox" OnClick="lbtnSelectSupl1_Click">Select</asp:LinkButton>
                                            </div>

                                        </div>
                                        <div class="col-md-1 pading5px">
                                            <div class="colMdbtn pading5px">
                                                <asp:LinkButton ID="lbtnSelectAll" runat="server" Style="margin: 0;" CssClass="btn btn-primary  primaryBtn checkbox" OnClick="lbtnSelectAll_Click">Select All</asp:LinkButton>

                                            </div>

                                        </div>
                                    </div>

                                </div>

                            </fieldset>

                            <asp:GridView ID="gvLandCodeLink" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" ShowFooter="True" Width="16px"
                                OnRowDeleting="gvLandCodeLink_RowDeleting">
                                <PagerSettings Visible="False" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="user Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResCod0" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:CommandField ShowDeleteButton="True" />

                                    <asp:TemplateField HeaderText="pactcode Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprocode" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Project Name">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnSuplUpdate" runat="server" OnClick="lbtnSuplUpdate_Click"
                                                CssClass="btn  btn-danger primarygrdBtn">Final Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvproDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                Width="300px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>


                        </asp:Panel>
                    </div>



                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


