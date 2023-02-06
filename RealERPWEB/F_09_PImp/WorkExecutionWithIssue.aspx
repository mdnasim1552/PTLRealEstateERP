<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="WorkExecutionWithIssue.aspx.cs" Inherits="RealERPWEB.F_09_PImp.WorkExecutionWithIssue" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@400;500;700&display=swap" rel="stylesheet">
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />

    <style>
        /*body {
            font-family: 'Poppins', sans-serif;
        }*/

        hr {
            margin: 0;
        }

        .backgroundColorContainer {
            background: #F9F9F9;
        }

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

        function onGenerateValidate() {
            if ($("#ContentPlaceHolder1_txtWRefNo").val().length == 0) {
                showContentFail("Enter Reference No.");
                $("#ContentPlaceHolder1_txtWRefNo").focus();
                return false;
            }
            else {
                return true;
            }
        }
        function onSaveValidate() {



            if ($("#ContentPlaceHolder1_txtSMCR").val().length == 0) {
                showContentFail("Enter SMCR No.");
                $("#ContentPlaceHolder1_txtSMCR").focus();
                return false;
            }
            else {
                if ($("#ContentPlaceHolder1_txtDMIRF").val().length == 0) {
                    showContentFail("Enter DMIRF No.");
                    $("#ContentPlaceHolder1_txtDMIRF").focus();
                    return false;
                }
                else {
                    if ($("#ContentPlaceHolder1_txtRefno").val().length == 0) {
                        showContentFail("Enter R/A No.");
                        $("#ContentPlaceHolder1_txtRefno").focus();
                    }
                    else {
                        if (confirm("Are you sure to Save?") == true) {
                            return true;
                        }
                        else {
                            return false;
                        }
                    }





                }
            }

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
                                <h6 class="mb-0">WORK EXECUTION</h6>
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
                            <div class="col-lg-3">
                                <div class="row">
                                    <div class="col-4 d-flex align-items-center">
                                        <asp:Label runat="server" ID="Label7" class="form-label">Ref No.</asp:Label>
                                    </div>
                                    <div class="col-8 d-flex align-items-center">
                                        <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txtWRefNo"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-2 offset-lg-2">
                                <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-sm btn-warning w-100" OnClientClick="return onGenerateValidate()"
                                    OnClick="btnGenerateIssue_Click">
                                        Generate Issue
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="backgroundColorContainer mt-1">
                            <div class="row">
                                <asp:GridView ID="DataGridOne" runat="server" AutoGenerateColumns="False"
                                    CssClass=" table table-striped table-hover table-bordered grvContentarea"
                                    OnRowDeleting="DataGridOne_RowDeleting"
                                    ShowFooter="True">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
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
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnl2" Visible="false">

                        <asp:Panel runat="server" ID="pnlMat" Visible="false">

                            <div class="row mt-1">
                                <div class="col-lg-2 d-flex align-items-center">
                                    <h6 class="mb-0">MATERIAL ISSUE</h6>
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
                                <div class="col-lg-3">
                                    <div class="row">
                                        <div class="col-4 d-flex align-items-center">
                                            <asp:Label runat="server" ID="Label11" class="form-label">SMCR No.</asp:Label>
                                        </div>
                                        <div class="col-8 p-0">
                                            <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txtSMCR"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="row">
                                        <div class="col-4 d-flex align-items-center">
                                            <asp:Label runat="server" ID="Label12" class="form-label">DMIRF No.</asp:Label>
                                        </div>
                                        <div class="col-8 p-0">
                                            <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txtDMIRF"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-1">
                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-sm btn-info w-100" OnClick="btnBack_Click">
                                        Back
                                    </asp:LinkButton>
                                </div>
                            </div>

                            <div class="backgroundColorContainer mt-1">
                                <div class="row">
                                    <asp:GridView ID="DataGridTwo" runat="server" AutoGenerateColumns="False"
                                        OnRowDataBound="DataGridTwo_RowDataBound"
                                        CssClass="table-striped table-hover table-bordered grvContentarea"
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


                                            <asp:TemplateField HeaderText="Code" Visible="false">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblisircode" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isircode")) %>'
                                                        Width="150px"></asp:Label>
                                                    <asp:Label ID="lblrsircode" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                        Width="150px"></asp:Label>
                                                    <asp:Label ID="lblflrcod" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrcod")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>


                                                <HeaderStyle HorizontalAlign="Left" />

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Floor">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblflrdesc" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "flrdesc").ToString() %>'
                                                        Width="100px" Font-Size="12px" ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <FooterStyle ForeColor="Black" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Work">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblisirdesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblwrkunit" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "WrkUnit")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Font-Size="10pt" HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Work Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblwrkqty" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "WrkQty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="70px" Font-Size="12px" ForeColor="Black" Style="text-align: right; padding-right: 2px;"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <FooterStyle ForeColor="Black" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Material">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrsirdesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                        Width="200px" Style="padding-left: 2px;"></asp:Label>

                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Specification">
                                                <ItemTemplate>
                                                    <div class="d-flex align-items-center justify-content-center">
                                                        <asp:DropDownList ID="ddlSpecification" CssClass="form-control select2" runat="server" AutoPostBack="true" Width="70px"
                                                            OnSelectedIndexChanged="ddlSpecification_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:LinkButton runat="server" ID="LnkbtnSpec" OnClick="LnkbtnSpec_Click" CssClass="d-flex align-items-center"
                                                            ToolTip="Add Specification">
                                                            <span class="fas fa-plus fa-sm px-1 ml-1" style="color:blue;" aria-hidden="true"></span>&nbsp;
                                                        </asp:LinkButton>
                                                    </div>

                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrsirunit" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Font-Size="10pt" HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bal. Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrstdqty" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="80px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <FooterStyle ForeColor="Black" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Actual Quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAnaQty" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuqty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="80px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <FooterStyle ForeColor="Black" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtAnaQty" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuqty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="80px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <FooterStyle ForeColor="Black" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Use Of Location">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtuol" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "useoflocation") %>'
                                                        Width="130px" Font-Size="12px" ForeColor="Black"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <FooterStyle ForeColor="Black" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtremarks" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "remarks") %>'
                                                        Width="120px" Font-Size="12px" ForeColor="Black"></asp:TextBox>
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
                        <asp:Panel runat="server" ID="pnlLab" Visible="false">
                            <div class="row mt-1">
                                <div class="col-lg-2 d-flex align-items-center">
                                    <h6 class="my-2">SUB-CONTRACTOR EXECUTION</h6>
                                </div>
                            </div>
                            <div class="backgroundColorContainer mt-1">
                                <div class="row">
                                    <asp:GridView ID="DataGridThree" runat="server" AutoGenerateColumns="False" 
                                        CssClass="table-striped table-hover table-bordered grvContentarea"
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


                                            <asp:TemplateField HeaderText="Code" Visible="false">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblisircode" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isircode")) %>'
                                                        Width="150px"></asp:Label>
                                                    <asp:Label ID="lblrsircode" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                        Width="150px"></asp:Label>
                                                    <asp:Label ID="lblflrcod" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrcod")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>


                                                <HeaderStyle HorizontalAlign="Left" />

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Floor">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblflrdesc" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "flrdesc").ToString() %>'
                                                        Width="100px" Font-Size="12px" ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <FooterStyle ForeColor="Black" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Work">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblisirdesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblwrkunit" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "WrkUnit")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Font-Size="10pt" HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Work Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblwrkqty" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "WrkQty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="70px" Font-Size="12px" ForeColor="Black" Style="text-align: right; padding-right: 2px;"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <FooterStyle ForeColor="Black" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Labour">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lnkTotal" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lnkTotal_Click">Calculate</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrsirdesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                        Width="200px" Style="padding-left: 2px;"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrsirunit" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Font-Size="10pt" HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bal. Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrstdqty" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="80px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <FooterStyle ForeColor="Black" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Work Quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtWorkAnaQty" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lwrkqty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="80px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <FooterStyle ForeColor="Black" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtAnaQty" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuqty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="80px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <FooterStyle ForeColor="Black" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtAnaRate" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isurat")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="80px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <FooterStyle ForeColor="Black" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFamount" runat="server" Style="text-align: right"
                                                        Width="80px" Font-Size="12px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtAnaAmount" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="80px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:TextBox>
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










                        <div class="row">
                            <div class="col-lg-2">
                                <asp:Label runat="server" ID="Label9" class="form-label">Narration</asp:Label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtNarration" TextMode="MultiLine" Rows="3"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row d-flex justify-content-center">
                            <asp:LinkButton ID="lnkSave" runat="server" CssClass="btn btn-sm btn-primary mx-2 my-2" OnClick="lnkSave_Click" Width="100px"
                                OnClientClick="return onSaveValidate()"><span class="fa fa-save " style="color:white;" aria-hidden="true"  ></span> Save</asp:LinkButton>

                        </div>
                    </asp:Panel>
                    <asp:Panel runat="server">
                    </asp:Panel>



                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>




</asp:Content>
