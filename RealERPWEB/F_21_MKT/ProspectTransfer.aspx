<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="ProspectTransfer.aspx.cs" Inherits="RealERPWEB.F_21_MKT.ProspectTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {

            $('.select2').each(function () {
                var select = $(this);
                select.select2({
                    placeholder: 'Select an option',
                    width: '100%',
                    allowClear: !select.prop('required'),
                    language: {
                        noResults: function () {
                            return "{{ __('No results found') }}";
                        }
                    }
                });
            });
        };

    </script>

    <div class="card card-fluid container-data">
        <div class="card-header mt-3 mb-0 pb-0">
            <h3>Prospect Transfer from Associate to Associate</h3>
        </div>

        <div class="card-body mb-0 pb-0">
            <div class="row mb-0 pb-0">
                <div class="col-md-8">
                    <div class="row">
                        <asp:Label ID="lbl" runat="server" CssClass="col-2 col-form-label">From Employee</asp:Label>

                        <div class="col-7">
                            <asp:DropDownList ID="ddlEmpid" ClientIDMode="Static" data-placeholder="Choose Employee.." runat="server"
                                CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpid_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    

                    </div>
                    <div class="row">
                        <asp:GridView ID="gvProspectWorking" runat="server" AutoGenerateColumns="False"
                            PageSize="10" AllowPaging="true" OnPageIndexChanging="gvProspectWorking_PageIndexChanging"
                            ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            HeaderStyle-Font-Size="14px" Width="800px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl" HeaderStyle-Width="30px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: center"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                            ForeColor="Black"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>



                                <asp:TemplateField
                                    HeaderText="Prospect Name">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblProsName" runat="server" Font-Bold="True"
                                            Text="Prospect Name" Width="120px"></asp:Label>
                                        <asp:HyperLink ID="hlnkbtnProsWorking" runat="server"
                                            CssClass="btn  btn-success  btn-xs" ToolTip="Export Excel"><span class="fa  fa-file-excel "></span></asp:HyperLink>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvProsName" runat="server"
                                            Font-Size="12px" Font-Underline="False" Target="_blank"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prospectname")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="left" />
                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Contact No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPhone" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                            Width="90px" ForeColor="Black"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Profession">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvProfession" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "profession")) %>'
                                            Width="80px" ForeColor="Black"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Address">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAddress" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preaddress")) %>'
                                            Width="120px" ForeColor="Black"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Interested Project">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvIntProject" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "interestproj")) %>'
                                            Width="120px" ForeColor="Black"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Source" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSource" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "leadsrc")) %>'
                                            Width="80px" ForeColor="Black"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chckTrnsfer" runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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

                <div class="col-md-4">
                    <div class="row mt-0 pb-0">
                            <asp:Label ID="Label1" runat="server" CssClass="col-3 col-form-label">Page</asp:Label>

                        <div class="col-5 p-0">

                            <asp:DropDownList ID="ddlpage" runat="server" AutoPostBack="True" CssClass="form-control select2" OnSelectedIndexChanged="ddlpage_SelectedIndexChanged">
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>100</asp:ListItem>
                                <asp:ListItem>150</asp:ListItem>
                                <asp:ListItem>200</asp:ListItem>
                                <asp:ListItem>300</asp:ListItem>
                                <asp:ListItem>400</asp:ListItem>
                            </asp:DropDownList>

                        </div>
                        <asp:Label ID="Label2" runat="server" CssClass="col-12 col-form-label">To Employee </asp:Label>

                        <div class="col-12">

                            <asp:DropDownList ID="ddlEmpNameTo" data-placeholder="To Employee.." runat="server"
                                CssClass="select2" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <asp:LinkButton ID="btnUpdate" runat="server" CssClass="btn btn-sm btn-primary">Transfer</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>

    </div>

</asp:Content>
