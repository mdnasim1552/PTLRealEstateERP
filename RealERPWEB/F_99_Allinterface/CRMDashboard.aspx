<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="CRMDashboard.aspx.cs" Inherits="RealERPWEB.F_99_Allinterface.CRMDashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/fontawesome.css" integrity="sha384-eHoocPgXsiuZh+Yy6+7DsKAerLXyJmu2Hadh4QYyt+8v86geixVYwFqUvMU8X90l" crossorigin="anonymous" />

    <style>
        a:hover {
            text-decoration: none;
            cursor: pointer;
        }

        .metric-label {
            font-weight: bold;
            font-size: 25px;
        }

        .textfont16 {
            padding: 5px 10px;
            border-radius: 10px;
        }

        .font-26 {
            font-size: 45px !important;
        }

        .media-body p {
            font-size: 25px !important;
        }

        .counterup {
            padding: 0px 5px;
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
            <div class="card card-fluid container-data mt-5">
                <div class="card-body">
                    <div class="row mb-2">
                        <div class="col-md-2">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend ">
                                    <button class="btn btn-secondary" type="button">From</button>
                                </div>
                                <asp:TextBox ID="txtfodate" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtfodate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-5">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary ml-1" type="button">Team Lead</button>
                                </div>
                                <asp:DropDownList ID="ddlEmpid" ClientIDMode="Static" data-placeholder="Choose Employee.." runat="server" CssClass="custom-select chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpid_SelectedIndexChanged">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-1 float-right">
                            <asp:HyperLink ID="hpylnkBtn" runat="server" ForeColor="White" CssClass="btn btn-sm btn-primary" Text="Go Interface"></asp:HyperLink>
                        </div>
                    </div>

                    <div class="row mb-2">
                        <div class="col-md-6 col-xl-3">
                            <div class="card">
                                <div class="card-body mb-2">
                                    <div class="media">
                                        <div class="avatar-md xbg-dark rounded-circle mr-2">
                                            <i class="fas fa-users avatar-title font-26 text-dark"></i>

                                        </div>
                                        <div class="media-body align-self-center">
                                            <div class="text-right">
                                                <h4 class="font-20 my-0 font-weight-bold"><span class="tile-circle bg-dark text-white counterup" id="lblprospect" runat="server" data-plugin="counterup">0</span></h4>
                                                <p class="mb-0 mt-1  text-truncate">Prospect</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mt-4 d-none">
                                        <h6 class="text-uppercase">Target <span class="float-right">90%</span></h6>
                                        <div class="progress progress-sm m-0">
                                            <div class="progress-bar bg-info" role="progressbar" aria-valuenow="90" aria-valuemin="0" aria-valuemax="100" style="width: 90%">
                                                <span class="sr-only">90% Complete</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- end card-box-->
                            </div>
                        </div>
                        <div class="col-md-6 col-xl-3">
                            <div class="card">
                                <div class="card-body mb-2">
                                    <div class="media">
                                        <div class="avatar-md xbg-info rounded-circle mr-2">
                                            <i class="fas fa-gem avatar-title font-26 text-purple"></i>

                                        </div>
                                        <div class="media-body align-self-center">
                                            <div class="text-right">
                                                <h4 class="font-20 my-0 font-weight-bold"><span class="tile-circle bg-purple text-white counterup" id="lblrating" runat="server" data-plugin="counterup">0</span></h4>
                                                <p class="mb-0 mt-1 text-truncate">Valuable Prospect</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mt-4 d-none">
                                        <h6 class="text-uppercase">Target <span class="float-right">60%</span></h6>
                                        <div class="progress progress-sm m-0">
                                            <div class="progress-bar bg-info" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 60%">
                                                <span class="sr-only">60% Complete</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- end card-box-->
                            </div>
                        </div>
                        <div class="col-md-6 col-xl-3">
                            <div class="card">
                                <div class="card-body mb-2">
                                    <div class="media">
                                        <div class="avatar-md xbg-warning rounded-circle mr-2">
                                            <i class="fas fa-binoculars avatar-title font-26 text-warning"></i>

                                        </div>
                                        <div class="media-body align-self-center">
                                            <div class="text-right">
                                                <h4 class="font-20 my-0 font-weight-bold"><span class="tile-circle bg-warning text-white counterup" id="lbldws" runat="server" data-plugin="counterup">0</span></h4>
                                                <p class="mb-0 mt-1 text-truncate">Todays Follow-up</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mt-4 d-none">
                                        <h6 class="text-uppercase">Target <span class="float-right">60%</span></h6>
                                        <div class="progress progress-sm m-0">
                                            <div class="progress-bar bg-info" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 60%">
                                                <span class="sr-only">60% Complete</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- end card-box-->
                            </div>
                        </div>

                        <div class="col-md-6 col-xl-3">
                            <div class="card">
                                <div class="card-body mb-2">
                                    <div class="media">
                                        <div class="avatar-md bg-success rounded-circle mr-2">
                                            <i class="fas fa-check-circle avatar-title font-26 text-white"></i>

                                        </div>
                                        <div class="media-body align-self-center">
                                            <div class="text-right">
                                                <h4 class="font-20 my-0 font-weight-bold"><span class="tile-circle bg-success text-white counterup" id="lbldwr" runat="server" data-plugin="counterup">0</span></h4>
                                                <p class="mb-0 mt-1 text-truncate">Today's Work Done</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mt-4 d-none">
                                        <h6 class="text-uppercase">Target <span class="float-right">60%</span></h6>
                                        <div class="progress progress-sm m-0">
                                            <div class="progress-bar bg-info" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 60%">
                                                <span class="sr-only">60% Complete</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- end card-box-->
                            </div>
                        </div>
                        <div class="col-md-6 col-xl-3">
                            <div class="card">
                                <div class="card-body mb-2">
                                    <div class="media">
                                        <div class="avatar-md bg-danger rounded-circle mr-2">
                                            <i class="fas fa-times-circle avatar-title font-26 text-white"></i>

                                        </div>
                                        <div class="media-body align-self-center">
                                            <div class="text-right">
                                                <h4 class="font-20 my-0 font-weight-bold"><span class="tile-circle bg-danger text-white counterup" data-plugin="counterup">0</span></h4>
                                                <p class="mb-0 mt-1 text-truncate">Close/Hold</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mt-4 d-none">
                                        <h6 class="text-uppercase">Target <span class="float-right">60%</span></h6>
                                        <div class="progress progress-sm m-0">
                                            <div class="progress-bar bg-info" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 60%">
                                                <span class="sr-only">60% Complete</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- end card-box-->
                            </div>
                        </div>
                        <div class="col-md-6 col-xl-3">
                            <div class="card">
                                <div class="card-body mb-2">
                                    <div class="media">
                                        <div class="avatar-md xbg-info rounded-circle mr-2">
                                            <i class="fas fa-cart-plus avatar-title font-26 text-success"></i>

                                        </div>
                                        <div class="media-body align-self-center">
                                            <div class="text-right">
                                                <h4 class="font-20 my-0 font-weight-bold"><span class="tile-circle bg-success text-white counterup" id="lblcsigned" runat="server" data-plugin="counterup">0</span></h4>
                                                <p class="mb-0 mt-1 text-truncate">Sold</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mt-4 d-none">
                                        <h6 class="text-uppercase"><span class="float-right"></span></h6>
                                        <div class="progress progress-sm m-0">
                                            <div class="progress-bar bg-primary" role="progressbar" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style="width: 100%">
                                                <span class="sr-only"></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- end card-box-->
                            </div>
                        </div>
                        <div class="col-md-6 col-xl-3">
                            <div class="card">
                                <div class="card-body mb-2">
                                    <div class="media">
                                        <div class="avatar-md xbg-info rounded-circle mr-2">
                                            <i class="fas fa-door-closed avatar-title font-26 text-danger"></i>

                                        </div>
                                        <div class="media-body align-self-center">
                                            <div class="text-right">
                                                <h4 class="font-20 my-0 font-weight-bold"><span class="tile-circle bg-danger text-white counterup" data-plugin="counterup">0
                                                                                          </span></h4>
                                                <p class="mb-0 mt-1 text-truncate">Lost</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mt-4 d-none">
                                        <h6 class="text-uppercase"><span class="float-right"></span></h6>
                                        <div class="progress progress-sm m-0">
                                            <div class="progress-bar bg-primary" role="progressbar" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style="width: 100%">
                                                <span class="sr-only"></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- end card-box-->
                            </div>
                        </div>
                        <div class="col-md-6 col-xl-3">
                            <div class="card">
                                <div class="card-body mb-2">
                                    <div class="media">
                                        <div class="avatar-md xbg-info rounded-circle mr-2">
                                            <i class="fas fa-database avatar-title font-26 text-white"></i>

                                        </div>
                                        <div class="media-body align-self-center">
                                            <div class="text-right">
                                                <h4 class="font-20 my-0 font-weight-bold"><span class="tile-circle bg-primary text-white counterup" id="lblDatablank" runat="server" data-plugin="counterup">0</span></h4>
                                                <p class="mb-0 mt-1 text-truncate">Data Bank</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mt-4 d-none">
                                        <h6 class="text-uppercase"><span class="float-right"></span></h6>
                                        <div class="progress progress-sm m-0">
                                            <div class="progress-bar bg-primary" role="progressbar" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style="width: 100%">
                                                <span class="sr-only"></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- end card-box-->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            

                    <div class="row mb-2">
                        <div class="col-md-4 col-sm-4 col-lg-4">
                            <div class="card card-fluid">
                                <div class="card-header drag-handle">Daily Lead Status </div>
                                
                            </div>
                        </div>
                        <div class="col-md-4 col-sm-4 col-lg-4">                            
                              <div class="card card-fluid">
                                <div class="card-header drag-handle">Weekly Lead Status</div>                                
                            </div>
                        </div>
                        <div class="col-md-4 col-sm-4 col-lg-4">
                            <div class="card card-fluid">
                                <div class="card-header drag-handle">Monthly Lead Status</div>                                
                            </div>                           
                        </div>
                    </div>

                


        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
