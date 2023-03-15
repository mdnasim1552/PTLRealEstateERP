<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptMissFollowup.aspx.cs" Inherits="RealERPWEB.F_21_MKT.RptMissFollowup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {

            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

        }

    </script>

    <asp:UpdatePanel runat="server">
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

            <div class="card">
                <div class="card-body mt-2">
                    <div class="row">

                        <div class="col-md-3 mr-3 ">
                            <div cssclass="form-group" style="display: flex">
                                <asp:Label runat="server" CssClass="form-label mr-2 mt-2">Employee:</asp:Label>
                                <asp:DropDownList runat="server" ID="ddlEmployeeName" CssClass="form-control chzn-select" Style="width: 90%">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-3 mr-5">
                            <div cssclass="form-group" style="display: flex">
                                <asp:Label ID="Label1" runat="server" CssClass="smLbl_to mr-2 mt-2"
                                    Text="From:"></asp:Label>
                                <asp:TextBox ID="txtFDate" runat="server" CssClass="inputDateBox rounded-0" TabIndex="5"></asp:TextBox>
                                <cc1:CalendarExtender runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>
                                <asp:Label ID="Label3" runat="server" CssClass="smLbl_to ml-2 mr-2 mt-2"
                                    Text="To:"></asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="inputDateBox rounded-0" TabIndex="5"></asp:TextBox>
                                <cc1:CalendarExtender runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-1 mt-1 mr-4">
                            <div class="form-group" style="display: flex">
                                <asp:Label ID="lblPage" runat="server" CssClass="control-label mr-2 mt-1" Text="Size:"></asp:Label>
                                <asp:DropDownList ID="ddlpagesize" CssClass="form-control form-control-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Width="71px">
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="150">150</asp:ListItem>
                                    <asp:ListItem Value="200">200</asp:ListItem>
                                    <asp:ListItem Value="300">300</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1 mt-1">
                            <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm primaryBtn">Ok</asp:LinkButton>
                        </div>

                    </div>
                </div>
            </div>

            <div class="card" style="min-height: 500px">
                <div class="card-body">
                    <div class="row p-2">
                        <div class="table-responsive">
                            <asp:GridView runat="server" ID="grvMissFollowUp" AllowPaging="True" CssClass="table-striped  table-bordered grvContentarea"
                                AutoGenerateColumns="False" ShowFooter="True" OnPageIndexChanging="grvMissFollowUp_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ID">
                                        <ItemTemplate>
                                            <asp:Label runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prosid")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Team">
                                        <ItemTemplate>
                                            <asp:Label runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teampname")) %>'
                                                Width="160px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Prospect">
                                        <ItemTemplate>
                                            <asp:Label runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Discussion Date">
                                        <ItemTemplate>
                                            <asp:Label runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cdate")) %>'
                                                Width="170px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Follow Up">
                                        <ItemTemplate>
                                            <asp:Label runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "followupdesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Follow Up Date">
                                        <ItemTemplate>
                                            <asp:Label runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nfupdate")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actual">
                                        <ItemTemplate>
                                            <asp:Label runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "repdate")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                                <FooterStyle CssClass="grvFooterNew" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeaderNew" />
                                <RowStyle CssClass="grvRowsNew" />


                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
