<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="LandProspectTransfer.aspx.cs" Inherits="RealERPWEB.F_21_MKT.LandProspectTransfer" %>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid container-data">
                <div class="card-header mt-3 mb-0 pb-0">
                    <h3>Land Transfer from Associate to Associate</h3>
                </div>

                <div class="card-body mb-0 pb-0" style="min-height: 450px;">
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
                                    PageSize="200" AllowPaging="true" OnPageIndexChanging="gvProspectWorking_PageIndexChanging"
                                    ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    HeaderStyle-Font-Size="14px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL." HeaderStyle-Width="30px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: center"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                                    ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Land Address">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblProsName" runat="server" Font-Bold="True"
                                                    Text="Land Address" Width="120px"></asp:Label>
                                                <asp:HyperLink ID="hlnkbtnProsWorking" runat="server"
                                                    CssClass="btn  btn-success  btn-xs" ToolTip="Export Excel"><span class="fa  fa-file-excel "></span></asp:HyperLink>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvLandDesc" runat="server"
                                                    Font-Size="12px" Font-Underline="False"  
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "landaddress")) %>'
                                                    Width="350px"></asp:Label>

                                                <asp:Label ID="lblteamcode" runat="server" Visible="false"
                                                    Font-Size="12px"  
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamcode")) %>'></asp:Label>
                                                 <asp:Label ID="lblproscod" runat="server" Visible="false"
                                                    Font-Size="12px"  
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "proscod")) %>'></asp:Label>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Country">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCountry" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "country")) %>'
                                                    Width="80px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="District">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDistrict" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "district")) %>'
                                                    Width="80px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAllfrm" OnCheckedChanged="chkAllfrm_CheckedChanged" runat="server" AutoPostBack="True" Text=" All" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chckTrnsfer" runat="server"
                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkper"))=="True" %>'
                                                    Width="30px" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" />
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
                                <asp:Label ID="Label1" runat="server" CssClass="col-4 col-form-label">Page</asp:Label>

                                <div class="col-5">

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
                            </div>
                            <div class="row mt-5 pb-0">

                                <asp:Label ID="Label2" runat="server" CssClass="col-4 col-form-label">To Employee </asp:Label>

                                <div class="col-8">

                                    <asp:DropDownList ID="ddlEmpNameTo" data-placeholder="To Employee.." runat="server"
                                        CssClass="select2" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-12 text-center">

                                    <asp:LinkButton ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" CssClass="btn btn-sm   btn-primary">Transfer</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
