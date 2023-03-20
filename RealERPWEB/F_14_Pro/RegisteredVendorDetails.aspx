<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RegisteredVendorDetails.aspx.cs" Inherits="RealERPWEB.F_14_Pro.RegisteredVendorDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                                <div class="col-10">

                                    <h4><i class="fa fa-id-card" style="font-size: x-large; margin: 3px 10px 0px 0px"></i>Vendor Profile</h4>
                                </div>
                                <div class="col-2">
                                    <div class="d-flex flex-row-reverse">
                                        <asp:LinkButton ID="lbtngvRVLenlist" runat="server" Font-Size="X-Small" CssClass="btn btn-sm btn-primary"
                                            OnClientClick="return confirm('Are you sure you want to change.');"
                                            OnClick="lbtngvRVLvarify_Click">Enlist</asp:LinkButton>
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
                                    <div class="col-6">
                                        <div class="form-group">
                                            <label class="form-label">Owner Name</label>
                                            <asp:Label runat="server" ID="lblownername" CssClass="form-control form-control-sm"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-6">
                                        <div class="form-group">
                                            <label class="form-label">Owner NID</label>
                                            <asp:Label runat="server" ID="lblownernid" CssClass="form-control form-control-sm"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="width: 100%">
                                    <div class="col-6">
                                        <div class="form-group">
                                            <label class="form-label">Owner TIN/BIN</label>
                                            <asp:Label runat="server" ID="lblownertin" CssClass="form-control form-control-sm"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-6">
                                        <div class="form-group">
                                            <label class="form-label">Upload TIN/BIN</label><br />
                                            <input type="file" cssclass="form-control form-control-sm" />
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
                                    <div class="col-6">
                                        <div class="form-group">
                                            <label class="form-label">Bank Name</label>
                                            <asp:Label runat="server" ID="lblbankname" CssClass="form-control form-control-sm"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-6">
                                        <div class="form-group">
                                            <label class="form-label">Brance Name</label>
                                            <asp:Label runat="server" ID="lblbranch" CssClass="form-control form-control-sm"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="row" style="width: 100%">
                                    <div class="col-6">
                                        <div class="form-group">
                                            <label class="form-label">Account Name</label>
                                            <asp:Label runat="server" ID="lblaccname" CssClass="form-control form-control-sm"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-6">
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
                                    <div class="form-group">
                                        <asp:Label runat="server">Company Portfolio and Overview</asp:Label>
                                        <asp:Label runat="server" ID="lblcomoverview"></asp:Label>
                                    </div>

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


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
