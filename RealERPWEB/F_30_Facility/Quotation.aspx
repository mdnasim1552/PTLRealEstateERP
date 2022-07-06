<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="Quotation.aspx.cs" Inherits="RealERPWEB.F_30_Facility.Quotation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            font-family: Cambria, Cochin, Georgia, Times, Times New Roman, serif;
            font-size: 12px;
        }

        p {
            width: 100%;
            text-align: center;
            border-bottom: 1px solid rgba(34, 34, 48, .3);
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

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
                                <asp:Label runat="server" ID="Label5" class="form-label">Select</asp:Label>
                                <asp:DropDownList ID="ddlDgNo" CssClass="chzn-select form-control" runat="server" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <br />
                                <asp:LinkButton ID="btnOKClick" runat="server" CssClass="btn btn-info align-self-end" OnClick="btnOKClick_Click">
                                <span class="fa fa-check-circle" style="color:white;" aria-hidden="true"></span>OK
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>

                    <asp:Panel runat="server" ID="pnlComplain">
                        <div class="row mt-1">
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblProject" class="form-label">Project</asp:Label>
                                    <asp:Label runat="server" ID="lblProjectText" class="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblCustomer" class="form-label">Customer</asp:Label>
                                    <asp:Label runat="server" ID="lblCustomerText" class="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblUnitlabel" class="form-label">Unit</asp:Label>
                                    <asp:Label runat="server" ID="lblUnitText" class="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="Label3" class="form-label">Warranty</asp:Label>
                                    <asp:Label runat="server" ID="lblWarranty" class="form-control"></asp:Label>
                                    <asp:Label runat="server" ID="lblWarrantyCode" class="form-control" Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <p><span>Complain Form</span></p>


                            <div class="col-lg-4">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-4">
                                            <asp:Label runat="server" ID="Label1" CssClass="font-weight-bold">Complain Date:</asp:Label>
                                        </div>
                                        <div class="col-8">
                                            <asp:Label runat="server" ID="lblComplainDate"></asp:Label>
                                        </div>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-4">
                                            <asp:Label runat="server" ID="Label2" CssClass="font-weight-bold">Remarks:</asp:Label>
                                        </div>
                                        <div class="col-8">
                                            <asp:Label runat="server" ID="lblRemarksCmp">No Comments</asp:Label>
                                        </div>
                                    </div>

                                </div>

                            </div>
                            <div class="col-lg-8">
                                <div class="row  table-responsive m-1">
                                    <asp:GridView ID="gvComplainForm" runat="server" ShowFooter="true" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea">
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
                                            <asp:TemplateField HeaderText="Complain Code" Visible="false">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvconcatcode" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "complainId")) %>'
                                                        Width="150px"></asp:Label>
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
                        <div class="row">

                            <p><span>Engineer Diagnosis</span></p>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-4">
                                            <asp:Label runat="server" ID="Label4" CssClass="font-weight-bold">Engr. Check Date:</asp:Label>
                                        </div>
                                        <div class="col-8">
                                            <asp:Label runat="server" ID="lblEngrDate"></asp:Label>
                                        </div>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-4">
                                            <asp:Label runat="server" ID="Label8" CssClass="font-weight-bold">Remarks:</asp:Label>
                                        </div>
                                        <div class="col-8">
                                            <asp:Label runat="server" ID="lblRemarksDg">No Comments</asp:Label>
                                        </div>
                                    </div>

                                </div>

                            </div>
                            <div class="col-lg-8">
                                <div class="row  table-responsive m-1">
                                    <asp:GridView ID="dgv1" runat="server" ShowFooter="true" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea">
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
                                            <asp:TemplateField HeaderText="Complain Code" Visible="false">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvconcatcode" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "complainId")) %>'
                                                        Width="150px"></asp:Label>
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

                        <div class="row">
                            <p><span>Budget Form</span></p>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-4">
                                            <asp:Label runat="server" ID="Label6" CssClass="font-weight-bold">Budget Date:</asp:Label>
                                        </div>
                                        <div class="col-8">
                                            <asp:Label runat="server" ID="lblBgdDate"></asp:Label>
                                        </div>
                                    </div>

                                </div>
                                 <div class="form-group">
                                    <div class="row">
                                        <div class="col-4">
                                            <asp:Label runat="server" ID="Label11" CssClass="font-weight-bold">Approved Date:</asp:Label>
                                        </div>
                                        <div class="col-8">
                                            <asp:Label runat="server" ID="lblApprDate"></asp:Label>
                                        </div>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-4">
                                            <asp:Label runat="server" ID="Label9" CssClass="font-weight-bold">Remarks:</asp:Label>
                                        </div>
                                        <div class="col-8">
                                            <asp:Label runat="server" ID="lblRemarksAppr">No Comments</asp:Label>
                                        </div>
                                    </div>

                                </div>

                            </div>
                            <div class="col-lg-8">
                                <div class="row table-responsive">

                                    <asp:GridView ID="gvMaterials" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea ml-2"
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
                                            <asp:TemplateField HeaderText="Material Code" Visible="false">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvconcatcode" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "materialId")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>


                                                <HeaderStyle HorizontalAlign="Left" />

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Material">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvconcatdesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "materialdesc")) %>'
                                                        Width="300px"></asp:Label>
                                                </ItemTemplate>


                                                <HeaderStyle HorizontalAlign="Left" />

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Unit">
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
                                            <asp:TemplateField HeaderText="Quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvQuantity" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "quantity")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="140px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <FooterStyle ForeColor="Black" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvRate" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="140px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle ForeColor="Black" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="140px" Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFAmt" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Font-Bold="True" Font-Size="12px"
                                                        Width="140px" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
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
                            </div>
                        </div>





                        <div class="row d-flex justify-content-center">
                            <asp:LinkButton ID="lnkQuotAcc" runat="server" CssClass="btn btn-sm btn-warning mx-2 my-2" OnClick="lnkQuotAcc_Click" Width="150px" 
                                OnClientClick="return confirm('Are You Sure?')">
                                <span class="fa fa-check " style="color:black;" aria-hidden="true"></span> Quotation Accept</asp:LinkButton>
                            <asp:LinkButton ID="lnkCollection" runat="server" CssClass="btn btn-sm btn-primary mx-2 my-2" OnClick="lnkCollection_Click" Width="150px"
                                OnClientClick="return confirm('Are You Sure?')"><span class="fa fa-receipt " style="color:white;" aria-hidden="true"></span> Collection</asp:LinkButton>
                            <asp:LinkButton ID="lnkReq" runat="server" CssClass="btn btn-sm btn-info mx-2 my-2" OnClick="lnkReq_Click" Width="150px">
                                <span class="fa fa-arrow-circle-right" style="color:white;" aria-hidden="true"></span>
                                Mat. Requisition
                            </asp:LinkButton>
                        </div>




                    </asp:Panel>




                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
