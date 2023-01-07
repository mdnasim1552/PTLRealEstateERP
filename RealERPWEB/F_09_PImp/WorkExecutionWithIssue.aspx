<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="WorkExecutionWithIssue.aspx.cs" Inherits="RealERPWEB.F_09_PImp.WorkExecutionWithIssue" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@400;500;700&display=swap" rel="stylesheet">
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />

    <style>
        body {
            font-family: 'Poppins', sans-serif;
        }

        hr {
            margin: 0;
        }

        .backgroundColorContainer {
            background: #F9F9F9;
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
                        <div class="col-lg-2">
                            <div class="form-group">
                                <asp:Label runat="server" ID="Label3" class="form-label">Date</asp:Label>
                                <asp:TextBox ID="txtEntryDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtEntryDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <asp:Label runat="server" ID="lblProject" class="form-label">Project</asp:Label>
                            <asp:DropDownList ID="ddlProject" CssClass="form-control select2" runat="server"></asp:DropDownList>
                        </div>
                        <div class="col-lg-1">
                            <br />
                            <asp:LinkButton ID="btnOK" runat="server" CssClass="btn btn-info w-100" OnClick="btnOK_Click">
                                <span class="fa fa-check-circle" style="color:white;" aria-hidden="true"></span> OK
                            </asp:LinkButton>
                        </div>
                    </div>
                    <hr />
                    <asp:Panel runat="server" ID="pnl1" Visible="false">
                        <div class="row">
                            <div class="col-lg-2">
                                <asp:Label runat="server" ID="Label1" class="form-label">Category</asp:Label>
                                <asp:DropDownList ID="ddlCategory" CssClass="form-control select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="col-lg-2">
                                <asp:Label runat="server" ID="Label2" class="form-label">Item List</asp:Label>
                                <asp:DropDownList ID="ddlItem" CssClass="form-control select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="col">
                                <asp:Label runat="server" ID="Label4" class="form-label">Division</asp:Label>
                                <asp:ListBox ID="ddlDivision" runat="server" CssClass="form-control select2" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                            <div class="col-lg-1">
                                <br />
                                <asp:LinkButton ID="btnSelectOne" runat="server" CssClass="btn btn-info w-100" OnClick="btnSelectOne_Click">
                                Select
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="row mt-1">
                            <div class="col-lg-2 d-flex align-items-center">
                                <h5 class="mb-0">WORK EXECUTION</h5>
                            </div>
                            <div class="col-lg-3">
                                <div class="row">
                                    <div class="col-4 d-flex align-items-center">
                                        <asp:Label runat="server" ID="Label5" class="form-label">W.Issue No</asp:Label>
                                    </div>
                                    <div class="col-4 px-1">
                                        <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txtWINoPartOne" disabled></asp:TextBox>
                                    </div>
                                    <div class="col-4 p-0">
                                        <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txtWINoPartTwo" disabled></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="backgroundColorContainer mt-1">
                            <div class="row">
                                <div class="col-lg-2">
                                    <asp:Label runat="server" ID="Label7" class="form-label">Page</asp:Label>
                                    <asp:DropDownList ID="ddlPage" CssClass="form-control select2" runat="server">
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
                                <div class="col-lg-2">
                                    <asp:Label runat="server" ID="Label8" class="form-label">Ref No.</asp:Label>
                                    <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txtWRefNo"></asp:TextBox>
                                </div>
                                <div class="col-lg-2 offset-lg-6">
                                    <br />
                                    <asp:LinkButton ID="btnGenerateIssue" runat="server" CssClass="btn btn-warning w-100" OnClick="btnGenerateIssue_Click">
                                        Generate Issue
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="row">
                                <asp:GridView ID="DataGridOne" runat="server" AutoGenerateColumns="False" CssClass=" table table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMSRSlNo" runat="server" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" />
                                        <asp:TemplateField HeaderText="Item Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblitemcode" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcode")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fl" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvflrCode" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrcod")) %>'
                                                    Width="20px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Floor Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvflrDes" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Work Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblwrkdesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "workitem")) %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="10pt" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="Label14" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wrkunit")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkTotal" runat="server" CssClass="btn btn-primary btn-sm primarygrdBtn">Total</asp:LinkButton>
                                            </FooterTemplate>

                                            <ItemStyle Font-Size="10pt" HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bal.Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbalqty" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="120px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Wrk.Qty">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtwrkqty" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wrkqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="120px" BackColor="Transparent" BorderColor="#D3D3D3"
                                                    BorderStyle="Solid" BorderWidth="1px" Style="text-align: right"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>


                                    <FooterStyle CssClass="gvPagination" />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>
                        </div>
                        <asp:Panel runat="server" ID="pnl2" Visible="false">
                            <div class="row mt-1">
                                <div class="col-lg-2 d-flex align-items-center">
                                    <h5 class="mb-0">MATERIAL ISSUE</h5>
                                </div>
                                <div class="col-lg-3">
                                    <div class="row">
                                        <div class="col-4 d-flex align-items-center">
                                            <asp:Label runat="server" ID="Label6" class="form-label">M.Issue No</asp:Label>
                                        </div>
                                        <div class="col-4 px-1">
                                            <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txtMINoPartOne" disabled></asp:TextBox>
                                        </div>
                                        <div class="col-4 p-0">
                                            <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txtMINoPartTwo" disabled></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="backgroundColorContainer mt-1">
                                <%-- <div class="row">
                                    <div class="col-lg-2">
                                        <asp:Label runat="server" ID="Label11" class="form-label">Materials</asp:Label>
                                        <asp:DropDownList ID="ddlMaterials" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="col-lg-2">
                                        <asp:Label runat="server" ID="Label12" class="form-label">Specification</asp:Label>
                                        <asp:DropDownList ID="ddlSpecifications" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="col-lg-1">
                                        <br />
                                        <asp:LinkButton ID="btnSelectTwo" runat="server" CssClass="btn btn-info w-100">
                                Select
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-lg-1">
                                        <br />
                                        <asp:LinkButton ID="btnSelectTwoAll" runat="server" CssClass="btn btn-info w-100">
                                Select All
                                        </asp:LinkButton>
                                    </div>
                                </div>--%>
                                <div class="row">
                                    <div class="col-lg-2">
                                        <asp:Label runat="server" ID="Label9" class="form-label">Page</asp:Label>
                                        <asp:DropDownList ID="DropDownList2" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="col-lg-2">
                                        <asp:Label runat="server" ID="Label10" class="form-label">Ref No.</asp:Label>
                                        <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="TextBox1"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row">
                                    <asp:GridView ID="DataGridTwo" runat="server" AutoGenerateColumns="False" CssClass=" table table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.">
                                                <ItemTemplate>
                                                    <asp:Label ID="serialnoid" runat="server"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="LnkbtnDelete"
                                                        ToolTip="Delete Item" Width="25px">
                                                <span class="fa fa-sm fa-trash " style="color:red;" aria-hidden="true"  ></span>&nbsp;
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Code" Visible="false">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvconcatcode" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isircode")) %>'
                                                        Width="150px"></asp:Label>
                                                    <asp:Label ID="Label13" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                        Width="150px"></asp:Label>
                                                    <asp:Label ID="Label15" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrcod")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>


                                                <HeaderStyle HorizontalAlign="Left" />

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Work">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvissueDesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc")) %>'
                                                        Width="300px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Floor">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvitemdesc" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "flrdesc").ToString() %>'
                                                        Width="100px" Font-Size="12px" ForeColor="Black"></asp:Label>

                                                </ItemTemplate>


                                                <HeaderStyle HorizontalAlign="Left" />
                                                <FooterStyle ForeColor="Black" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Material">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvconcatdesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Specification">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlSpecification" CssClass="form-control select2" runat="server"
                                                        ></asp:DropDownList>

                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bal. Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvitemdesc" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rstdqty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="180px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>

                                                </ItemTemplate>


                                                <HeaderStyle HorizontalAlign="Left" />
                                                <FooterStyle ForeColor="Black" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvQuantity" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rstdqty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="180px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <FooterStyle ForeColor="Black" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                        </Columns>


                                        <FooterStyle CssClass="gvPagination" />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>

                                </div>
                            </div>
                        </asp:Panel>

                    </asp:Panel>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>




</asp:Content>
