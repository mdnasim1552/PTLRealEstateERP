<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="QuotationEntry.aspx.cs" Inherits="RealERPWEB.F_70_Services.QuotationEntry" %>

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
    <script>
        function OpenModal() {
            $('#addModal').modal('toggle');
        }
        function CloseModal() {

            $('#addModal').modal('toggle');
        }
        function OpenModalResource() {
            $('#addModalResource').modal('toggle');
        }
        function CloseModalResource() {

            $('#addModalResource').modal('toggle');
        }
        function OpenModalSubContractor() {
            $('#subContractor').modal('toggle');
        }
        function CloseModalSubContractor() {

            $('#subContractor').modal('toggle');
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
                                <asp:Label runat="server" ID="lbldate" class="form-label">Date</asp:Label>
                                <asp:TextBox ID="txtEntryDate" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_txttoDate" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtEntryDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <asp:Label runat="server" ID="isPrevCode" class="form-label" Visible="false"></asp:Label>
                                <asp:Label runat="server" ID="lblQuotation" class="form-label" Visible="false"></asp:Label>
                                <asp:Label runat="server" ID="Label5" class="form-label">Quotation No</asp:Label>
                                <asp:TextBox ID="txtquotno" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <asp:Label runat="server" ID="Label7" class="form-label">Customer</asp:Label>
                                <div class="d-flex">
                                    <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="form-control chzn-select">
                                    </asp:DropDownList>
                                    <asp:LinkButton ID="btnaddcustomer" runat="server" CssClass="btn" OnClick="btnaddcustomer_Click">
                                                <span class="fa fa-plus-circle " aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <br />
                                <asp:LinkButton ID="btnOKClick" runat="server" CssClass="btn btn-info align-self-end" OnClick="btnOKClick_Click">
                                <span class="fa fa-check-circle" style="color:white;" aria-hidden="true"></span> OK
                                </asp:LinkButton>
                            </div>
                        </div>

                    </div>

                    <asp:Panel runat="server" ID="pnlQuotation">


                        <p><span>Quotation Form</span></p>
                        <div class="row mt-2">
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="Label1" class="form-label">Work Type</asp:Label>
                                    <asp:DropDownList ID="ddlWorkType" runat="server" CssClass="form-control chzn-select"></asp:DropDownList>

                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="Label4" class="form-label">Resource</asp:Label>
                                    <div class="d-flex">
                                        <asp:DropDownList ID="ddlResource" runat="server" CssClass="form-control chzn-select">
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="btnAddResource" runat="server" CssClass="btn" OnClick="btnAddResource_Click">
                                                <span class="fa fa-plus-circle " aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </div>
                                    <asp:Label runat="server" ID="lblUnit" class="form-label" Visible="false"></asp:Label>
                                    <asp:Label runat="server" ID="lblsirval" class="form-label" Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <br />
                                    <asp:LinkButton ID="btnAdd" runat="server" CssClass="btn btn-success" OnClick="btnAdd_Click">
                                            <span class="fa fa-plus-circle " style="color:white;" aria-hidden="true"  ></span>
                                            Add
                                    </asp:LinkButton>

                                </div>
                            </div>
                        </div>

                        <div class="row table-responsive">

                            <asp:GridView ID="gvMaterials" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea ml-2"
                                ShowFooter="True" OnRowDataBound="gvMaterials_RowDataBound">
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
                                                OnClick="LnkbtnDelete_Click" ToolTip="Delete Item">
                                                <span class="fa fa-sm fa-trash " style="color:red;" aria-hidden="true"  ></span>&nbsp;
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Material Code" Visible="false">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvconcatcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resourcecode")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>


                                        <HeaderStyle HorizontalAlign="Left" />

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Work Type">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvworktypecode" runat="server" Visible="false"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "worktypecode")) %>'
                                                Width="100px"></asp:Label>
                                            <asp:Label ID="lblgvworktypedesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "worktypedesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>


                                        <HeaderStyle HorizontalAlign="Left" />

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Resource">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvconcatdesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resourcedesc")) %>'
                                                Width="170px"></asp:Label>
                                        </ItemTemplate>


                                        <HeaderStyle HorizontalAlign="Left" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnTotal" runat="server" OnClick="lbtnTotal_Click" CssClass="btn btn-sm btn-primary">Total</asp:LinkButton>

                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvitemdesc" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "unit").ToString() %>'
                                                Width="70px" Font-Size="12px" ForeColor="Black"></asp:Label>
                                        </ItemTemplate>


                                        <HeaderStyle HorizontalAlign="Left" />
                                        <FooterStyle ForeColor="Black" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvPercnt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="50px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <FooterStyle ForeColor="Black" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQuantity" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qqty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="90px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <FooterStyle ForeColor="Black" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvRate" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qrate")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="90px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle ForeColor="Black" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAmount" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="120px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFAmt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Font-Bold="True" Font-Size="12px"
                                                Width="120px" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle ForeColor="Black" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="%" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvChkPercnt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chkpercnt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="50px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <FooterStyle ForeColor="Black" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Check Qty" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvChkQuantity" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chkqty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="90px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <FooterStyle ForeColor="Black" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Check Rate" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvChkRate" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chkrate")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="90px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle ForeColor="Black" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Check Amt" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtChkAmount" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chkamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="120px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvChkFAmt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Font-Bold="True" Font-Size="12px"
                                                Width="120px" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle ForeColor="Black" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="%" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvAprPercnt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprpercnt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="50px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <FooterStyle ForeColor="Black" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Apr. Qty" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvAprQuantity" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprqty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="90px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <FooterStyle ForeColor="Black" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Apr. Rate" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvAprRate" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprrate")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="90px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle ForeColor="Black" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Apr. Amt" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAprAmount" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "apramt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="120px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvAprFAmt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Font-Bold="True" Font-Size="12px"
                                                Width="120px" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle ForeColor="Black" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Process" Visible="false">
                                        <ItemTemplate>

                                            <asp:CheckBox ID="chkProcess" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "isprocess")) %>'></asp:CheckBox>

                                        </ItemTemplate>
                                        <FooterStyle ForeColor="Black" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                </Columns>


                                <FooterStyle CssClass="gvPagination" />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                        </div>


                        <div class="row">
                            <div class="col-md-5 mt-1">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="Label6" class="form-label mt-2">Remarks</asp:Label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtNarration" placeholder="Write Notes" TextMode="MultiLine" Rows="5"></asp:TextBox>

                                </div>

                            </div>
                        </div>

                        <div class="row d-flex justify-content-center">
                            <asp:LinkButton ID="lnkRefresh" runat="server" CssClass="btn btn-sm btn-warning mx-2 my-2" OnClick="lnkRefresh_Click" Width="100px">
                                <span class="fa fa-redo " style="color:black;" aria-hidden="true"></span> Refresh</asp:LinkButton>
                            <asp:LinkButton ID="lnkSave" runat="server" CssClass="btn btn-sm btn-success mx-2 my-2" OnClick="lnkSave_Click" Width="100px"
                                OnClientClick="return confirm('Are You Sure?')"><span class="fa fa-save" style="color:white;" aria-hidden="true"  ></span> Save</asp:LinkButton>

                            <asp:Panel runat="server" ID="pnlAccept" Visible="false">
                                <asp:LinkButton ID="lnkMatReq" runat="server" CssClass="btn btn-sm btn-primary mx-2 my-2"
                                    OnClick="lnkMatReq_Click" Width="100px"
                                    OnClientClick="return confirm('Are You Sure?')">
                                Material Req.</asp:LinkButton>

                                <asp:LinkButton ID="lnkSubContractor" runat="server" CssClass="btn btn-sm btn-primary mx-2 my-2"
                                    OnClick="lnkSubContractor_Click" Width="140px"
                                    OnClientClick="return confirm('Are You Sure?')">
                                Sub Contractor</asp:LinkButton>




                                <asp:LinkButton ID="lnkReceivable" runat="server" CssClass="btn btn-sm btn-primary mx-2 my-2"
                                    OnClick="lnkReceivable_Click" Width="100px"
                                    OnClientClick="return confirm('Are You Sure?')">
                                Receivable </asp:LinkButton>
                            </asp:Panel>


                            <asp:Label runat="server" ID="lblReqno" Visible="false" class="form-label mt-2"></asp:Label>
                            <asp:Label runat="server" ID="lblActcode" Visible="false" class="form-label mt-2"></asp:Label>



                            <asp:LinkButton ID="lnkClose" runat="server" CssClass="btn btn-sm btn-dark mx-2 my-2" Width="150px">
                                <span class="fa fa-window-close" style="color:white;" aria-hidden="true"></span>
                                Close
                            </asp:LinkButton>
                        </div>




                    </asp:Panel>




                </div>
            </div>
            <div class="modal" id="addModal" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-light">
                            <h6 class="modal-title">Add Customer</h6>
                            <asp:LinkButton ID="CloseVehcl" runat="server" CssClass="close close_btn" OnClientClick="CloseModal();" data-dismiss="modal"> &times; </asp:LinkButton>
                        </div>
                        <div class="modal-body mt-3">
                            <div class="form-group">
                                <asp:Label ID="lblLoanId" runat="server">Customer Name</asp:Label>
                                <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            </div>
                        </div>
                        <div class="modal-footer ">
                            <asp:LinkButton ID="lnkUpdateModal" runat="server" CssClass="btn btn-sm btn-success" OnClick="lnkUpdateModal_Click"
                                OnClientClick="CloseModal();"><span class="glyphicon glyphicon-save"></span>Update</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal" id="addModalResource" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-light">
                            <h6 class="modal-title">Add Resources</h6>
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="close close_btn" OnClientClick="CloseModalResource();" data-dismiss="modal"> &times; </asp:LinkButton>
                        </div>
                        <div class="modal-body mt-3">
                            <div class="form-group">
                                <asp:Label ID="Label9" runat="server">Resource Type</asp:Label>
                                <asp:DropDownList ID="ddlResourceType" runat="server" CssClass="form-control chzn-select">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server">Resource</asp:Label>
                                <asp:TextBox ID="txtResource" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server">Unit</asp:Label>
                                <asp:TextBox ID="txtUnit" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="Label8" runat="server">Std. Rate</asp:Label>
                                <asp:TextBox ID="txtRate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            </div>
                        </div>
                        <div class="modal-footer ">
                            <asp:LinkButton ID="lnkUpdateResourceModal" runat="server" CssClass="btn btn-sm btn-success" OnClick="lnkUpdateResourceModal_Click"
                                OnClientClick="CloseModalResource();"><span class="glyphicon glyphicon-save"></span>Update</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal" id="subContractor" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-light">
                            <h6 class="modal-title">Select Sub Contractor</h6>
                            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="close close_btn" OnClientClick="CloseModalSubContractor();" data-dismiss="modal"> &times; </asp:LinkButton>
                        </div>
                        <div class="modal-body mt-3">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <asp:Label ID="Label10" runat="server">Sub Contactor</asp:Label>
                                        <asp:DropDownList ID="ddlSubContractor" runat="server" CssClass="form-control chzn-select">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer ">
                            <asp:LinkButton ID="lnkSubContractorSave" runat="server" CssClass="btn btn-sm btn-success" OnClick="lnkSubContractorSave_Click"
                                OnClientClick="CloseModalSubContractor();"><span class="glyphicon glyphicon-save"></span>Generate Bill</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">              
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

</asp:Content>
