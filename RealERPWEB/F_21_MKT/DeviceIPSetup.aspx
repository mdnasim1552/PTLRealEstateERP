<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="DeviceIPSetup.aspx.cs" Inherits="RealERPWEB.F_21_MKT.DeviceIPSetup" UnobtrusiveValidationMode="None" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function loadModalAddCode() {
            $('#AddIpAdd').modal('toggle', {
                backdrop: 'static',
                keyboard: false
            });
        };

        function CloseModalAddCode() {
            $('#AddIpAdd').modal('hide');
        };

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

            <div class="card mt-4" style="min-height: 500px">
                <div class="card-body">
                    <div class="row">
                        <div class="mb-2" style="margin-left: 360px">
                            <asp:LinkButton ID="BtnAdd" runat="server" OnClick="BtnAdd_Click" CssClass="btn btn-primary btn-sm" AutoPostBack="True"><i class="fa fa-plus" aria-hidden="true"></i> Add New IP</asp:LinkButton>
                        </div>
                    </div>
                    <div class="row">
                        <div class="table-responsive">
                            <asp:GridView runat="server" ID="grvIpSetup" AllowPaging="True" CssClass="table-striped  table-bordered grvContentarea"
                                AutoGenerateColumns="False" OnPageIndexChanging="grvacc_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="Machine No">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblMachNo" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "machno")) %>'>></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ip Address">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtIpAddress" runat="server" CssClass="border-0 text-center" Wrap="true"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ipaddress")) %>'>
                                            </asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Alias">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAlias" runat="server" CssClass="border-0 text-center" Wrap="true"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "machinealias")) %>'>>
                                            </asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Port">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPort" runat="server" CssClass="border-0 text-center"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "port")) %>'>
                                            </asp:TextBox>
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

            <div id="AddIpAdd" class="modal animated slideInLeft " role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content  ">
                        <div class="modal-header">
                            <h5 class="modal-title"><i class="fas fa-info-circle"></i>&nbsp;Add New IP Address</h5>
                            <asp:Label ID="lblmobile" runat="server"></asp:Label>
                            <button type="button" class="btn btn-xs btn-danger float-right" data-dismiss="modal" title="Close"><i class="fas fa-times-circle"></i></button>
                        </div>
                        <div class="modal-body form-horizontal">
                            <div class="row mb-1">
                                <label class="col-md-3">Machine No</label>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtMachineNo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-1">
                                <label class="col-md-3">IP Address</label>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtIpAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-1">
                                <label class="col-md-3">Alias</label>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtAlias" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-1">
                                <label class="col-md-3">Port</label>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtPort" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer ">
                            <asp:LinkButton ID="lbtnAddCode" runat="server" CssClass="btn btn-sm btn-success" ToolTip="Update Ip Info." OnClientClick="CloseModalAddCode();" OnClick="lbtnAddCode_Click">
                                <i class="fas fa-plus"></i>&nbsp;Add</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
