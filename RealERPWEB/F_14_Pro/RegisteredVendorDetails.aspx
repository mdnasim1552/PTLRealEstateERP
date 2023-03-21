﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RegisteredVendorDetails.aspx.cs" Inherits="RealERPWEB.F_14_Pro.RegisteredVendorDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $('.chzn-select').chosen({ search_contains: true });

        }
        function OpenSuppModal() {
            $("#suppModal").modal("show");
        }
        function CloseSuppModal() {
            $("#suppModal").modal('hide');
        }
    </script>

    <style>
        label {
            display: inline-block;
            margin-bottom: 0rem;
        }

        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
                    
        }
    </style>

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

            <div class="row">
                <div class="col-8">
                    <div class="card">
                        <div class="card-header">
                            <div class="row" style="margin-top: 16px;">
                                <div class="col-8">

                                    <h4><i class="fa fa-id-card" style="font-size: x-large; margin: 3px 10px 0px 0px"></i>Vendor Profile</h4>
                                </div>
                                <div class="col-4">
                                    <div class="d-flex flex-row-reverse">
                                        <asp:LinkButton ID="lbtnExistingEnlist" runat="server" Visible="false" CssClass="btn btn-sm btn-warning"
                                            OnClick="lbtnExistingEnlist_Click">Enlist with existing supplier</asp:LinkButton>


                                        <asp:LinkButton ID="lbtngvRVLvarify" runat="server" Visible="false"
                                            CssClass="btn btn-sm btn-warning mr-2"
                                            OnClientClick="return confirm('Once enlisted a vendor. You cannot unenlist later.');"
                                            OnClick="lbtngvRVLvarify_Click">Enlist</asp:LinkButton>

                                        <asp:Label ID="lblEnlisted" runat="server"
                                            Font-Size="Large" Font-Bold="true" ForeColor="White" Visible="false"
                                            Style="padding-top: 0px" CssClass="btn btn-sm btn-success">Enlisted</asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card-body" style="min-height: 500px">
                            <div class="row">
                                <div class="row" style="width: 100%">
                                    <h5>Generals</h5>
                                    <hr width="89%" align="right" />
                                </div>
                                <div class="row" style="width: 100%">
                                    <div class="col-7">
                                        <div class="form-group">
                                            <label class="form-label">Company Name</label>
                                            <asp:Label runat="server" ID="lblcompanyname" CssClass="form-control  form-control-sm"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-5">
                                        <div class="form-group">
                                            <label class="form-label">License No</label>
                                            <asp:Label runat="server" ID="lblLicenseno" CssClass="form-control  form-control-sm"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="width: 100%">
                                    <div class="col-7">
                                        <div class="form-group">
                                            <label class="form-label">Concern Name</label>
                                            <asp:Label runat="server" ID="lblConcernName" CssClass="form-control form-control-sm"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-5">
                                        <div class="form-group">
                                            <label class="form-label">Designation</label>
                                            <asp:Label runat="server" ID="lbldesignation" CssClass="form-control form-control-sm"></asp:Label>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="row" style="width: 100%">
                                    <h5>Contact</h5>
                                    <hr width="90%" align="right" />
                                </div>
                                <div class="row" style="width: 100%">
                                    <div class="col-4">
                                        <div class="form-group">
                                            <label class="form-label">Contact No 1</label>
                                            <asp:Label runat="server" ID="lblmobile" CssClass="form-control form-control-sm"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-4">
                                        <div class="form-group">
                                            <label class="form-label">Contact No 2</label>
                                            <asp:Label runat="server" ID="lblmobile2" CssClass="form-control form-control-sm"></asp:Label>

                                        </div>
                                    </div>
                                    <div class="col-4">
                                        <div class="form-group">
                                            <label class="form-label">Email</label>
                                            <asp:Label runat="server" ID="lblEmail" CssClass="form-control form-control-sm"></asp:Label>

                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="width: 100%">
                                    <div class="col-12">
                                        <div class="form-group">
                                            <label class="form-label">Address</label>
                                            <asp:Label runat="server" ID="lblAddress" CssClass="form-control form-control-sm">13154131314</asp:Label>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="row" style="width: 100%">
                                    <h5>Owner Details</h5>
                                    <hr width="84.5%" align="right" />
                                </div>
                                <div class="row" style="width: 100%">
                                    <div class="col-4">
                                        <div class="form-group">
                                            <label class="form-label">Owner Name</label>
                                            <asp:Label runat="server" ID="lblownername" CssClass="form-control form-control-sm"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-4">
                                        <div class="form-group">
                                            <label>Owner NID</label>
                                            <asp:Label runat="server" ID="lblownernid" CssClass="form-control form-control-sm"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-4">
                                        <div class="form-group">
                                            <label class="form-label">Owner TIN/BIN</label>
                                            <asp:Label runat="server" ID="lblownertin" CssClass="form-control form-control-sm"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="row" style="width: 100%">
                                    <h5>Bank Details</h5>
                                    <hr width="86.5%" align="right" />
                                </div>
                                <div class="row" style="width: 100%">
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label class="form-label">Bank Name</label>
                                            <asp:Label runat="server" ID="lblbankname" CssClass="form-control form-control-sm"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label class="form-label">Brance Name</label>
                                            <asp:Label runat="server" ID="lblbranch" CssClass="form-control form-control-sm"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label class="form-label">Account Name</label>
                                            <asp:Label runat="server" ID="lblaccname" CssClass="form-control form-control-sm"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label class="form-label">Account Number</label>
                                            <asp:Label runat="server" ID="lblaccnumber" CssClass="form-control form-control-sm"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="row" style="width: 100%">
                                    <h5>Overview</h5>
                                    <hr width="88.5%" align="right" />
                                </div>
                                <div class="row" style="width: 100%">
                                    <asp:TextBox runat="server" ID="lblcomoverview" Wrap="true" Width="100%" Rows="5" ReadOnly="true" TextMode="MultiLine" BorderStyle="None" BorderWidth="0"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-4">
                    <div class="card card-fluid">
                        <div class="card-header" style="margin-top: 16px;">
                            <div class="d-flex justify-content-md-center">
                                <asp:Label runat="server" ID="lblProName" Font-Bold="true" Font-Size="Large"></asp:Label>
                            </div>

                            <div class="d-flex justify-content-md-center">
                                <asp:Label runat="server" ID="lblProCompName" CssClass="text-muted"></asp:Label>

                            </div>
                            <div class="row" style="margin-top: 20px;">
                                <div class="col-6">
                                    <div class="card">
                                        <div class="card-body" style="padding-bottom: 16px;">
                                            <div class="d-flex justify-content-md-center">
                                                <asp:Label runat="server" ID="lblVendorId" Font-Bold="true" Font-Size="Large">10</asp:Label>

                                            </div>
                                            <div class="d-flex justify-content-md-center">
                                                <asp:Label runat="server" Font-Bold="true" Font-Size="Large">Vendor ID#</asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="card">
                                        <div class="card-body">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card-body" style="min-height: 500px">
                            <div class="row">
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div id="suppModal" class="modal" tabindex="-1" role="dialog">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Select Supplier</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div>
                                <asp:Label ID="lblSuplList2" runat="server" CssClass="lblTxt lblName" Text="Supplier"></asp:Label>
                                <asp:DropDownList ID="ddlSupl2" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="6">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton runat="server" ID="lbtnEnlistExisting" OnClick="lbtnEnlistExisting_Click"
                                OnClientClick="CloseSuppModal()" class="btn btn-sm btn-primary">Save changes</asp:LinkButton>
                            <button type="button" class="btn btn-sm btn-secondary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>