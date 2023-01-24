<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptWorkWiResVariance.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.RptWorkWiResVariance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <style>
        .table th, .table td {
            padding: 4px;
        }
    </style>
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".select2").select2();
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            //$('.chzn-select').chosen({ search_contains: true });
            //$(".chosen-select").chosen({
            //    search_contains: true,
            //    no_results_text: "Sorry, no match!",
            //    allow_single_deselect: true
            //});
            $('.select2').each(function () {
                var select = $(this);
                select.select2({
                    placeholder: 'Select an option',
                    width: '100%',
                    allowClear: !select.prop('required'),
                    language: {
                        noResults: function () {
                            return "'No results found'";
                        }
                    }
                });
            });
        }
    </script>
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
            <div class="card card-fluid container-data">
                <div class="card-body" style="min-height: 600px;">
                    <div class="row mt-2">
                        <div class="col-md-4">
                            <asp:Label runat="server" ID="lblProject" class="form-label">Project</asp:Label>
                            <asp:DropDownList ID="ddlProject" CssClass="form-control select2" runat="server"></asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <br />
                            <asp:LinkButton ID="btnOK" runat="server" CssClass="btn btn-info w-100" OnClick="btnOK_Click">
                                <span class="fa fa-check-circle" style="color:white;" aria-hidden="true"></span> OK
                            </asp:LinkButton>
                        </div>
                        <div class="col-md-2">
                            <asp:Label runat="server" ID="Label1" class="form-label">Floor</asp:Label>
                            <asp:DropDownList ID="ddlFloor" CssClass="form-control select2" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlFloor_SelectedIndexChanged"></asp:DropDownList>
                        </div>

                        <div class=" col-md-1">
                            <asp:Label ID="lblPage" CssClass="smLbl_to" runat="server" Text="Page"></asp:Label>
                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control select2" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                <asp:ListItem Value="10">10</asp:ListItem>
                                <asp:ListItem Value="15">15</asp:ListItem>
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

                    <div class="table table-responsive">
                        <asp:GridView ID="gvWrkVsRes" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            OnPageIndexChanging="gvWrkVsRes_PageIndexChanging" ShowFooter="True" Width="640px"
                            OnRowDataBound="gvWrkVsRes_RowDataBound" OnRowCreated="gvWrkVsRes_RowCreated" >
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Floor">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvFloor" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Description" FooterText="Total">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvItemDes" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc"))     %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvItemUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirunit")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvItemQty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itemqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Resource">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvResDes" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))     %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvResUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvresqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvresrate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFAmt " runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="90px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvresamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvresqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvresrate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isurat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="90px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvresamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvresrate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "vqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="% of Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvresrate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qtypcnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvresrate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "vrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="% of Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvresrate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ratpcnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="V Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvresrate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "vcost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="% of Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvresrate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amtpcnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
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
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>
