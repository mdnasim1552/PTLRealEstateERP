<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="ComplainForm.aspx.cs" Inherits="RealERPWEB.F_30_Facility.ComplainForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        body {
            font-family: Cambria, Cochin, Georgia, Times, Times New Roman, serif;
            font-size: 12px;
        }

        p {
            width: 100%;
            text-align: center;
            border-bottom: 1px solid rgba(34, 34, 48, .1);
            line-height: 0.1em;
            margin: 10px 0 20px;
            color: black;
            font-weight: 300;
            font-size: 14px;
        }

            p span {
                background: #fff;
                padding: 0 10px;
            }
    </style>
    <script type="text/javascript">       
        function loadModalComplain() {
            $('#modalEditComplain').modal('toggle', {
                backdrop: 'static',
                keyboard: false
            });
        }
        function CloseModalComplain() {
            $('#modalEditComplain').modal('hide');
        }
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            $('.chzn-select').chosen({ search_contains: true });
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="card card-fluid container-data">

                <div class="card-body" style="min-height: 600px;">

                    <div class="row mt-2">
                        <div class="col-lg-2">
                            <div class="form-group">
                                <asp:Label runat="server" ID="Label3" class="form-label">Date</asp:Label>
                                <asp:TextBox ID="txtEntryDate" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtEntryDate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-lg-2">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblcomplno" class="form-label" Visible="false"></asp:Label>
                                <asp:Label runat="server" ID="lblProject" class="form-label">Project</asp:Label>
                                <asp:DropDownList ID="ddlProject" CssClass="form-control chzn-select" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged"></asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblCustomer" class="form-label">Customer</asp:Label>
                                <asp:DropDownList ID="ddlCustomer" CssClass="chzn-select form-control" runat="server" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <br />
                            <asp:LinkButton ID="btnOKClick" runat="server" CssClass="btn btn-info align-self-end" OnClick="btnOKClick_Click">
                                <span class="fa fa-check-circle" style="color:white;" aria-hidden="true"></span> OK
                            </asp:LinkButton>
                        </div>

                    </div>


                    <asp:Panel ID="pnlComplain" runat="server" Visible="false">
                        <p><span>Project Information</span></p>
                        <div class="row mt-1">
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblUnitlabel" class="form-label">Unit</asp:Label>
                                    <asp:Label runat="server" ID="lblUnitText" class="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblHandOverDateLabel" class="form-label">HandOver Date</asp:Label>
                                    <asp:Label runat="server" ID="lblHandOverDateText" class="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblWarranty" class="form-label">Warranty</asp:Label>
                                    <asp:DropDownList ID="ddlWarranty" CssClass="chzn-select form-control" runat="server" AutoPostBack="True"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblWarrantyRemain" class="form-label">Warranty Day Remained</asp:Label>
                                    <asp:Label runat="server" ID="lblWarrantyRemainText" class="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblComType" class="form-label">Communication Type</asp:Label>
                                    <asp:DropDownList ID="ddlCommunicationType" CssClass="chzn-select form-control" runat="server" AutoPostBack="True"></asp:DropDownList>
                                </div>
                            </div>



                        </div>
                        <div id="divComplainList">
                            <div class="row">
                                <div class="col-4">
                                    <p><span>Type Problem</span></p>
                                    <div class="row">

                                        <%--<asp:Label runat="server" ID="lblissuetype" class="form-label">Issue Type</asp:Label>--%>
                                        <asp:DropDownList ID="ddlIssueType" CssClass="form-control chzn-select" runat="server" AutoPostBack="True"></asp:DropDownList>

                                    </div>
                                    <div class="row">
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtComplainDesc" placeholder="Write Problem"></asp:TextBox>
                                    </div>
                                    <div class="row mt-1">
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtComplainRemarks" placeholder="Write Remarks" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </div>
                                    <div class="row mt-1 d-flex justify-content-end">
                                        <asp:LinkButton ID="btnAdd" runat="server" CssClass="btn btn-sm btn-success" OnClick="btnAdd_Click" Width="150px">
                                            <span class="fa fa-plus-circle " style="color:white;" aria-hidden="true"  ></span>
                                            Add
                                        </asp:LinkButton>
                                    </div>
                                    <hr />
                                    <div class="row mt-1">
                                        <div class="col-4">
                                            <asp:Label runat="server" ID="Label1" class="form-label">Estimated Date</asp:Label>
                                        </div>
                                        <div class="col-8">
                                            <asp:TextBox ID="txtEstimatedDate" runat="server" CssClass="form-control"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                                Format="dd-MMM-yyyy" TargetControlID="txtEstimatedDate"></cc1:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="row mt-1">
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtNarration" placeholder="Write Additional Notes" TextMode="MultiLine" Rows="5"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="col-8">
                                    <p><span>Problem Table</span></p>
                                    <div class="row mt-1">

                                        <asp:GridView ID="dgv1" runat="server" AutoGenerateColumns="False" CssClass=" table table-striped table-hover table-bordered grvContentarea" ShowFooter="True">
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
                                                            OnClick="LnkbtnDelete_Click" ToolTip="Delete Item" Width="25px">
                                                <span class="fa fa-sm fa-trash " style="color:red;" aria-hidden="true"  ></span>&nbsp;
                                                        </asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="LnkbtnEdit" OnClick="LnkbtnEdit_Click"
                                                            ToolTip="Edit Item">
                                                <span class="fas fa-edit fa-sm" style="color:blue;" aria-hidden="true" Width="25px" ></span>&nbsp;
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Complain Code" Visible="false">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvconcatcode" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "complainId")) %>'
                                                            Width="150px"></asp:Label>
                                                        <asp:Label ID="Label4" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "issueId")) %>'
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>


                                                    <HeaderStyle HorizontalAlign="Left" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Issue Type">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvissueDesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "issueType")) %>'
                                                            Width="120px"></asp:Label>
                                                    </ItemTemplate>


                                                    <HeaderStyle HorizontalAlign="Left" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Problem Details">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvconcatdesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "complainDesc")) %>'
                                                            Width="300px"></asp:Label>
                                                    </ItemTemplate>


                                                    <HeaderStyle HorizontalAlign="Left" />

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvitemdesc" runat="server" BackColor="Transparent"
                                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "remarks").ToString() %>'
                                                            Width="300px" Font-Size="12px" ForeColor="Black"></asp:Label>

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
                            </div>
                        </div>


                        <div class="row d-flex justify-content-center">
                            <asp:LinkButton ID="lnkRefresh" runat="server" CssClass="btn btn-sm btn-warning mx-2 my-2" OnClick="lnkRefresh_Click" Width="100px">
                                <span class="fa fa-redo " style="color:black;" aria-hidden="true"></span> Refresh</asp:LinkButton>
                            <asp:LinkButton ID="lnkSave" runat="server" CssClass="btn btn-sm btn-primary mx-2 my-2" OnClick="lnkSave_Click" Width="100px"
                                OnClientClick="return confirm('Are You Sure?')"><span class="fa fa-save " style="color:white;" aria-hidden="true"  ></span> Save</asp:LinkButton>
                            <asp:LinkButton ID="lnkProceed" runat="server" CssClass="btn btn-sm btn-info mx-2 my-2" OnClick="lnkProceed_Click" Width="150px">
                                <span class="fa fa-arrow-circle-right " style="color:white;" aria-hidden="true"></span>
                                Proceed to Next Step
                            </asp:LinkButton>
                        </div>
                    </asp:Panel>




                </div>
            </div>

            <div id="modalEditComplain" class="modal animated slideInLeft" role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header" style="display: block;">

                            <button type="button" class="close btn btn-xs bg-danger" data-dismiss="modal">
                                <span class="fa fa-close"></span>

                            </button>
                            <h4 class="modal-title">
                                <span class="fa fa-sm fa-table pr-2" runat="server" id="txtheader">Edit Complain</span></h4>
                        </div>
                        <div class="modal-body form-horizontal">
                            <div class="row-fluid">

                                <div class="form-group" runat="server">
                                    <asp:Label ID="lblId" runat="server" Visible="false"></asp:Label>
                                    <asp:Label runat="server" ID="lbltype" class="col-md-4">Problem Details</asp:Label>
                                    <div class="col-md-12">
                                        <asp:TextBox ID="txtProblemDetails" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="form-group" runat="server" id="catdet">
                                    <asp:Label runat="server" ID="Label2" class="col-md-4">Remarks</asp:Label>
                                    <div class="col-md-12">
                                        <asp:TextBox ID="txtmodalRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="8"></asp:TextBox>
                                    </div>
                                </div>

                            </div>



                        </div>
                        <div class="modal-footer ">
                            <asp:LinkButton ID="lnkUpdateComplain" runat="server" CssClass="btn btn-sm btn-success"
                                OnClientClick="CloseModalComplain();" OnClick="lnkUpdateComplain_Click"><span class="glyphicon glyphicon-save"></span>Update</asp:LinkButton>


                            <%--<button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>--%>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
