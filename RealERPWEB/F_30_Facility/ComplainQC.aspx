<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="ComplainQC.aspx.cs" Inherits="RealERPWEB.F_30_Facility.ComplainQC" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
                                <asp:Label runat="server" ID="Label5" class="form-label">Select</asp:Label>
                                <asp:DropDownList ID="ddlDgNo" CssClass="chzn-select form-control" runat="server" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <br />
                                <asp:LinkButton ID="btnOKClick" runat="server" CssClass="btn btn-info align-self-end" OnClick="btnOKClick_Click">
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <asp:Panel runat="server" ID="pnlMatReq" Visible="false">
                        <div class="row mt-1">
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblProject" class="form-label">Project</asp:Label>
                                    <asp:Label runat="server" ID="lblProjectText" class="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="Label2" class="form-label">Site Visited</asp:Label>
                                    <asp:Label ID="lblSiteVisisted" runat="server" CssClass="form-control"></asp:Label>
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
                                    <asp:Label runat="server" ID="Label1" class="form-label">Warranty</asp:Label>
                                    <asp:Label runat="server" ID="lblWarranty" class="form-control"></asp:Label>
                                    <asp:Label runat="server" ID="lblWarrantyCode" class="form-control" Visible="false"></asp:Label>
                                </div>
                            </div>

                        </div>


                        <div class="row">
                            <p><span>Complaint Form</span></p>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-4">
                                            <asp:Label runat="server" ID="Label4" CssClass="font-weight-bold">Complain Date:</asp:Label>
                                        </div>
                                        <div class="col-8">
                                            <asp:Label runat="server" ID="lblComplainDate"></asp:Label>
                                        </div>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-4">
                                            <asp:Label runat="server" ID="Label6" CssClass="font-weight-bold">Remarks:</asp:Label>
                                        </div>
                                        <div class="col-8">
                                            <asp:Label runat="server" ID="lblRemarksCmp">No Comments</asp:Label>
                                        </div>
                                    </div>

                                </div>

                            </div>
                            <div class="col-lg-8">
                                <div class="row table-responsive">

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
                                            <asp:TemplateField HeaderText="QC">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkQC" runat="server" />
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
                         <div class="row d-flex justify-content-center">
                            <asp:LinkButton ID="lnkSave" runat="server" CssClass="btn btn-sm btn-primary mx-2 my-2" Width="100px"
                                OnClientClick="return confirm('Are You Sure?')"><span class="fa fa-save " style="color:white;" aria-hidden="true"  ></span> Save</asp:LinkButton>
                            <asp:LinkButton ID="lnkProceed" runat="server" CssClass="btn btn-sm btn-info mx-2 my-2"  Width="150px">
                                <span class="fa fa-arrow-circle-right " style="color:white;" aria-hidden="true"></span>
                                Proceed to Next Step
                            </asp:LinkButton>
                        </div>
                    </asp:Panel>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
