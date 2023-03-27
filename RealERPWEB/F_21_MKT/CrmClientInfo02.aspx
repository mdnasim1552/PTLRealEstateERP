<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="CrmClientInfo02.aspx.cs" Inherits="RealERPWEB.F_21_MKT.CrmClientInfo02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/crm-new-dashboard.css" rel="stylesheet" />
    <script>
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);           
        });
        function pageLoaded() {
            try {
                let floatingContainer = $(".floating");
                let floatingBtn = $(".floating-btn");
                let floatingHeader = $(".floating-header");

                floatingBtn.click(() => {
                    floatingContainer.addClass("active");
                });
                floatingHeader.click(() => {
                    floatingContainer.removeClass("active");
                });
                //Dashboard Link
                $("#btnDashboard").click(function () {
                    window.location.href = "../F_99_Allinterface/CRMDashboard03?Type=Report";
                });

            }
            catch (e) {

            }
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="wrapper">
                <div class="page mt-4">
                    <div class="page">
                        <div class="row mb-4">
                            <div class="col-md-8">
                                <div class="row align-items-end">
                                    <div class="col-2">
                                        <div class="form-group mb-0">
                                            <label for="form-date-range" class="form-label">
                                                Date</label>
                                            <select class="form-select" id="form-date-range">
                                                <option>Today</option>
                                                <option>Yesterday</option>
                                                <option>Last 7 Days</option>
                                                <option>This Month</option>
                                                <option>Last Month</option>
                                                <option>This Year</option>
                                                <option>last Year</option>
                                                <option>Custom</option>
                                            </select>
                                        </div>
                                    </div>
                                    <!-- END -->
                                    <div class="col-3">
                                        <div class="form-group mb-0">
                                            <select class="form-select">
                                                <option>Choose Employee</option>
                                            </select>
                                        </div>
                                    </div>
                                    <!-- END -->
                                    <div class="col-3">
                                        <div class="form-group mb-0">
                                            <select class="form-select">
                                                <option>Choose Project</option>
                                            </select>
                                        </div>
                                    </div>
                                    <!-- END -->
                                    <div class="col-4">
                                        <div class="d-flex">
                                            <div class="input-group form-search mb-0 mr-3">
                                                <div class="input-group-prepend">
                                                    <div class="input-group-text">
                                                        <i class="fas fa-search"></i>
                                                    </div>
                                                </div>
                                                <input
                                                    type="text"
                                                    class="form-control"
                                                    id="inlineFormInputGroup"
                                                    placeholder="Search Here" />
                                            </div>
                                            <button class="mmbd-btn mmbd-btn-primary">
                                                Apply
                                            </button>
                                        </div>
                                    </div>
                                    <!-- END -->
                                </div>
                            </div>
                            <div class="col-md-4 align-self-end">
                                <div class="d-flex justify-content-end">
                                    <button class="mmbd-btn mmbd-btn-primary mr-2" id="btnaddland" runat="server">
                                        <strong><i class="fas fa-user-plus"></i>&nbsp;Add Lead</strong>
                                    </button>
                                    <button class="mmbd-btn mmbd-btn-primary" id="btnDashboard">
                                        <img
                                            src="../assets/new-ui/images/equalizer.svg"
                                            alt="Dashboard"
                                            class="img-responsive" />
                                        <strong>Dashboard</strong>
                                    </button>
                                </div>
                            </div>
                        </div>
                        <!-- END HEAD -->
                        <div class="mb-4">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card-flash h-100 px-4 pt-3 pb-4">
                                        <div class="card-body pb-0">
                                            <asp:MultiView ID="MultiView1" runat="server">
                                                <asp:View ID="ViewPersonalInfo" runat="server">
                                                    <div class="row">
                                                    </div>
                                                    <div class="row">
                                                    </div>
                                                    <div class="row">
                                                    </div>
                                                    <div class="row mb-2 btnsavefix">
                                                    </div>
                                                </asp:View>
                                                <asp:View ID="ViewSummary" runat="server">
                                                    <div class="table-responsive">
                                                        <table
                                                            class="table table-bordered mmbd-table table-striped table-header-flash">
                                                            <thead>
                                                                <tr>
                                                                    <th scope="col" class="text-left">
                                                                        <div class="d-flex align-items-center">
                                                                            <div class="w-30px">
                                                                                <div
                                                                                    class="form-check form-check-sm form-check-custom form-check-solid">
                                                                                    <input
                                                                                        class="form-check-input"
                                                                                        type="checkbox" />
                                                                                </div>
                                                                            </div>
                                                                            <div
                                                                                class="dropdown mmbd-dropdown-icon mr-2">
                                                                                <button
                                                                                    class="btn btn-secondary dropdown-toggle"
                                                                                    type="button"
                                                                                    data-toggle="dropdown"
                                                                                    aria-expanded="false">
                                                                                    <i class="fa fa-filter"></i>
                                                                                </button>
                                                                                <div class="dropdown-menu">
                                                                                    <span class="dropdown-item">Green
                                                                                    </span>
                                                                                    <span class="dropdown-item">Red
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                            <span class="header-label">P-ID </span>
                                                                        </div>
                                                                    </th>
                                                                    <th scope="col" class="text-center">Generated Date
                                                                    </th>
                                                                    <th scope="col" class="text-center">Associate Name
                                                                    </th>
                                                                    <th scope="col" class="text-center">Project Name
                                                                    </th>
                                                                    <th scope="col" class="text-center">Source Name
                                                                    </th>
                                                                    <th scope="col" class="text-center">Followup Date
                                                                    </th>
                                                                    <th scope="col" class="text-center">
                                                                        <div
                                                                            class="d-flex align-items-center justify-content-center">
                                                                            <div
                                                                                class="dropdown mmbd-dropdown-icon mr-2">
                                                                                <button
                                                                                    class="btn btn-secondary dropdown-toggle"
                                                                                    type="button"
                                                                                    data-toggle="dropdown"
                                                                                    aria-expanded="false">
                                                                                    <i class="fa fa-filter"></i>
                                                                                </button>
                                                                                <div class="dropdown-menu">
                                                                                    <span class="dropdown-item">Query
                                                                                    </span>
                                                                                    <span class="dropdown-item">Lead
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                            <span class="header-label">Lead Status
                                                                            </span>
                                                                        </div>
                                                                    </th>
                                                                    <th scope="col" class="text-center">Prospect Name
                                                                    </th>
                                                                    <th scope="col" class="text-center">Last Discussion
                                                                    </th>
                                                                    <th scope="col" class="text-center w-70px">ACTION
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <div class="d-flex align-items-center">
                                                                            <div class="w-30px">
                                                                                <div
                                                                                    class="form-check form-check-sm form-check-custom form-check-solid">
                                                                                    <input
                                                                                        class="form-check-input"
                                                                                        type="checkbox" />
                                                                                </div>
                                                                            </div>
                                                                            <span class="text-success">#3066 </span>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <div class="d-flex flex-column">
                                                                            Jan 6, 2022
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">Edison Amour</td>
                                                                    <td class="text-center">Facebook</td>
                                                                    <td class="text-center">Jan 6, 2022</td>
                                                                    <td class="text-center">
                                                                        <div class="badge badge-light">
                                                                            <i class="fas fa-check"></i>Query
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">I have talked. I have talked. I have talked.
                                    I have talked. I have talked. I have talked.
                                    I have talked. I have talked. I have talked.
                                    I have talked. I have talked. I have talked.
                                    I have talked. I have talked. I have talked.
                                    I have talked. I have talked. I have talked.
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <div class="d-flex">
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fas fa-eye"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fa fa-edit"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light">
                                                                                <i class="fa fa-handshake"></i>
                                                                            </button>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div class="d-flex align-items-center">
                                                                            <div class="w-30px">
                                                                                <div
                                                                                    class="form-check form-check-sm form-check-custom form-check-solid">
                                                                                    <input
                                                                                        class="form-check-input"
                                                                                        type="checkbox" />
                                                                                </div>
                                                                            </div>
                                                                            <span class="text-success">#3066 </span>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <div class="d-flex flex-column">
                                                                            Jan 6, 2022
                                      <div class="badge badge-light">
                                          RE: Jan 20,2022
                                      </div>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">Edison Amour</td>
                                                                    <td class="text-center">Facebook</td>
                                                                    <td class="text-center">Jan 6, 2022</td>
                                                                    <td class="text-center">
                                                                        <div class="badge badge-light">
                                                                            <i class="fas fa-check"></i>Query
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">I have talked...</td>
                                                                    <td class="text-right">
                                                                        <div class="d-flex">
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fas fa-eye"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fa fa-edit"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light">
                                                                                <i class="fa fa-handshake"></i>
                                                                            </button>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div class="d-flex align-items-center">
                                                                            <div class="w-30px">
                                                                                <div
                                                                                    class="form-check form-check-sm form-check-custom form-check-solid">
                                                                                    <input
                                                                                        class="form-check-input"
                                                                                        type="checkbox" />
                                                                                </div>
                                                                            </div>
                                                                            <span class="text-success">#3066 </span>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <div class="d-flex flex-column">
                                                                            Jan 6, 2022
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">Edison Amour</td>
                                                                    <td class="text-center">Facebook</td>
                                                                    <td class="text-center">Jan 6, 2022</td>
                                                                    <td class="text-center">
                                                                        <div class="badge badge-light">
                                                                            <i class="fas fa-check"></i>Query
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">I have talked...</td>
                                                                    <td class="text-right">
                                                                        <div class="d-flex">
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fas fa-eye"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fa fa-edit"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light">
                                                                                <i class="fa fa-handshake"></i>
                                                                            </button>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div class="d-flex align-items-center">
                                                                            <div class="w-30px">
                                                                                <div
                                                                                    class="form-check form-check-sm form-check-custom form-check-solid">
                                                                                    <input
                                                                                        class="form-check-input"
                                                                                        type="checkbox" />
                                                                                </div>
                                                                            </div>
                                                                            <span class="text-success">#3066 </span>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <div class="d-flex flex-column">
                                                                            Jan 6, 2022
                                      <div class="badge badge-light">
                                          RE: Jan 20,2022
                                      </div>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">Edison Amour</td>
                                                                    <td class="text-center">Facebook</td>
                                                                    <td class="text-center">Jan 6, 2022</td>
                                                                    <td class="text-center">
                                                                        <div class="badge badge-light">
                                                                            <i class="fas fa-check"></i>Query
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">I have talked...</td>
                                                                    <td class="text-right">
                                                                        <div class="d-flex">
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fas fa-eye"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fa fa-edit"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light">
                                                                                <i class="fa fa-handshake"></i>
                                                                            </button>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div class="d-flex align-items-center">
                                                                            <div class="w-30px">
                                                                                <div
                                                                                    class="form-check form-check-sm form-check-custom form-check-solid">
                                                                                    <input
                                                                                        class="form-check-input"
                                                                                        type="checkbox" />
                                                                                </div>
                                                                            </div>
                                                                            <span class="text-success">#3066 </span>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <div class="d-flex flex-column">
                                                                            Jan 6, 2022
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">Edison Amour</td>
                                                                    <td class="text-center">Facebook</td>
                                                                    <td class="text-center">Jan 6, 2022</td>
                                                                    <td class="text-center">
                                                                        <div class="badge badge-light">
                                                                            <i class="fas fa-check"></i>Query
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">I have talked...</td>
                                                                    <td class="text-right">
                                                                        <div class="d-flex">
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fas fa-eye"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fa fa-edit"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light">
                                                                                <i class="fa fa-handshake"></i>
                                                                            </button>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div class="d-flex align-items-center">
                                                                            <div class="w-30px">
                                                                                <div
                                                                                    class="form-check form-check-sm form-check-custom form-check-solid">
                                                                                    <input
                                                                                        class="form-check-input"
                                                                                        type="checkbox" />
                                                                                </div>
                                                                            </div>
                                                                            <span class="text-success">#3066 </span>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <div class="d-flex flex-column">
                                                                            Jan 6, 2022
                                      <div class="badge badge-light">
                                          RE: Jan 20,2022
                                      </div>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">Edison Amour</td>
                                                                    <td class="text-center">Facebook</td>
                                                                    <td class="text-center">Jan 6, 2022</td>
                                                                    <td class="text-center">
                                                                        <div class="badge badge-light">
                                                                            <i class="fas fa-check"></i>Query
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">I have talked...</td>
                                                                    <td class="text-right">
                                                                        <div class="d-flex">
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fas fa-eye"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fa fa-edit"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light">
                                                                                <i class="fa fa-handshake"></i>
                                                                            </button>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div class="d-flex align-items-center">
                                                                            <div class="w-30px">
                                                                                <div
                                                                                    class="form-check form-check-sm form-check-custom form-check-solid">
                                                                                    <input
                                                                                        class="form-check-input"
                                                                                        type="checkbox" />
                                                                                </div>
                                                                            </div>
                                                                            <span class="text-success">#3066 </span>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <div class="d-flex flex-column">
                                                                            Jan 6, 2022
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">Edison Amour</td>
                                                                    <td class="text-center">Facebook</td>
                                                                    <td class="text-center">Jan 6, 2022</td>
                                                                    <td class="text-center">
                                                                        <div class="badge badge-light">
                                                                            <i class="fas fa-check"></i>Query
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">I have talked...</td>
                                                                    <td class="text-right">
                                                                        <div class="d-flex">
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fas fa-eye"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fa fa-edit"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light">
                                                                                <i class="fa fa-handshake"></i>
                                                                            </button>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div class="d-flex align-items-center">
                                                                            <div class="w-30px">
                                                                                <div
                                                                                    class="form-check form-check-sm form-check-custom form-check-solid">
                                                                                    <input
                                                                                        class="form-check-input"
                                                                                        type="checkbox" />
                                                                                </div>
                                                                            </div>
                                                                            <span class="text-success">#3066 </span>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <div class="d-flex flex-column">
                                                                            Jan 6, 2022
                                      <div class="badge badge-light">
                                          RE: Jan 20,2022
                                      </div>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">Edison Amour</td>
                                                                    <td class="text-center">Facebook</td>
                                                                    <td class="text-center">Jan 6, 2022</td>
                                                                    <td class="text-center">
                                                                        <div class="badge badge-light">
                                                                            <i class="fas fa-check"></i>Query
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">I have talked...</td>
                                                                    <td class="text-right">
                                                                        <div class="d-flex">
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fas fa-eye"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fa fa-edit"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light">
                                                                                <i class="fa fa-handshake"></i>
                                                                            </button>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-4">
                                                            <div class="d-flex align-items-center">
                                                                <div class="length">
                                                                    <select
                                                                        class="form-control form-control-sm mb-0">
                                                                        <option value="25">25</option>
                                                                        <option value="50">50</option>
                                                                        <option value="100">100</option>
                                                                    </select>
                                                                </div>
                                                                <div class="info ml-2">
                                                                    Showing 1 to 19 of 19 records
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-8">
                                                            <nav class="table-pagination">
                                                                <ul class="pagination">
                                                                    <li class="page-item mr-3">
                                                                        <a class="page-link previews" href="#"><i class="fas fa-arrow-left"></i>
                                                                            Previous</a>
                                                                    </li>
                                                                    <li class="page-item">
                                                                        <a class="page-link active" href="#">1</a>
                                                                    </li>
                                                                    <li class="page-item">
                                                                        <a class="page-link" href="#">2</a>
                                                                    </li>
                                                                    <li class="page-item">
                                                                        <a class="page-link" href="#">3</a>
                                                                    </li>
                                                                    <li class="page-item">
                                                                        <a class="page-link">...</a>
                                                                    </li>
                                                                    <li class="page-item">
                                                                        <a class="page-link" href="#">8</a>
                                                                    </li>
                                                                    <li class="page-item">
                                                                        <a class="page-link" href="#">9</a>
                                                                    </li>
                                                                    <li class="page-item">
                                                                        <a class="page-link" href="#">10</a>
                                                                    </li>
                                                                    <li class="page-item ml-3">
                                                                        <a class="page-link next" href="#">Next<i class="fas fa-arrow-right"></i></a>
                                                                    </li>
                                                                </ul>
                                                            </nav>
                                                        </div>
                                                    </div>
                                                </asp:View>
                                            </asp:MultiView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- END -->
                        <div class="floating">
                            <div class="floating-btn">
                                <i class="fas fa-angle-up"></i>
                                <span>To</span>
                                <span>Do</span>
                            </div>
                            <div class="floating-panel">
                                <div class="floating-header">
                                    <div class="floating-title">TO DO</div>
                                    <div class="floating-action">
                                        <i class="fas fa-angle-down down"></i>
                                        <i class="fas fa-angle-up up"></i>
                                    </div>
                                </div>
                                <div class="floating-body">
                                    <ul>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-primary">SW</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Schedules Work</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lbldws" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-purple">TD</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">To Day Task</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lbltdt" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-info">DWR</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Daily Work Report</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lbldwr" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-dark">KPI</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Key Performance Indicator</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-blue">Call</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Call</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblCall" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-success">Visit</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Visit</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblvisit" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-red">DP</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Day Passed</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblDayPass" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-warning">PME</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Project Meeting External</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblpme" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-info">PMI</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Project Meeting Internal</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblpmi" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-indigo">COM</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Comments</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblComments" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-orange">FRE</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Freezing</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblFreez" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-cyan">DP</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Dead Pros</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblDeadProspect" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-success">Si</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Signed</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblcsigned" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-danger">DB</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <a class="fs-3" href="#">Data Bank</a>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblDatablank" runat="server">0</div>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Modal -->
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
