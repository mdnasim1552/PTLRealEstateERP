<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="interfaceVchl.aspx.cs" Inherits="RealERPWEB.F_36_Vehcl.interfaceVchl" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .nav-tabs {
            border: none !important;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .chzn-container {
            width: 100% !important;
        }

        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }

        .mt20 {
            margin-top: 20px;
        }

        input#ContentPlaceHolder1_txtSearch {
            height: 29px;
        }

        .bw-100 {
            width: 100px !important;
        }

        ul.tbMenuWrp {
            margin: 0;
            padding: 0;
            border: 0;
            background: none !important;
        }

            ul.tbMenuWrp li {
                width: 155px;
                padding: 0px 0;
                float: left;
                list-style: none;
                margin: 0 2px;
                color: #fff;
                background: #5F5F5F;
                -webkit-border-radius: 4px;
                -moz-border-radius: 4px;
                border-radius: 4px;
            }

                ul.tbMenuWrp li a {
                    padding: 0 0;
                    background: #5F5F5F;
                    -webkit-border-radius: 4px;
                    -moz-border-radius: 4px;
                    border-radius: 4px;
                    display: block;
                    color: #fff;
                    padding: 0px 0 0 0;
                    vertical-align: middle;
                    border: none !important;
                }

                    ul.tbMenuWrp li a:hover {
                        background: #12A5A6;
                    }

                    ul.tbMenuWrp li a:focus {
                        outline: none;
                        outline-offset: 0;
                    }

                    ul.tbMenuWrp li a label {
                        color: #fff;
                        background: none;
                        border: none;
                        text-align: center;
                        font-weight: bold;
                        font-size: 16px;
                        display: block;
                        cursor: pointer;
                        width: 100%;
                    }

        .tbMenuWrp > li.active > a, .tbMenuWrp > li.active > a:focus, .tbMenuWrp > li.active > a:hover {
            background: #472AC6 !important;
            color: #fff;
        }


            .tbMenuWrp > li.active > a, .tbMenuWrp > li.active > a:focus, .tbMenuWrp > li.active > {
                background: #472AC6 !important;
                color: #fff;
            }




        table.grvContentarea tr td span.glyphicon {
            margin: 0 4px;
        }

        .tbMenuWrp table tr td label {
            color: #000;
            cursor: pointer;
            font-weight: bold;
            height: 35px;
            margin: 1px 0;
            /*padding: 2px;*/
            width: 100%;
        }

            .tbMenuWrp table tr td label.active > a, .tbMenuWrp table tr td label.active > .tbMenuWrp table tr td label:focus, .tbMenuWrp table tr td label.active {
                background: #12A5A6;
                color: #fff;
            }

                .tbMenuWrp table tr td label.active > a, .tbMenuWrp table tr td label.active > .tbMenuWrp table tr td label:focus, .tbMenuWrp table tr td label.active > a:hover {
                    background: #12A5A6;
                    color: #fff;
                }


        .tbMenuWrp table tr td input[type="checkbox"], input[type="radio"] {
            display: none;
        }

        .tabMenu a {
            display: block;
            line-height: 15px;
            font-size: 14px;
            color: #000;
            text-align: center;
            background: #fff;
        }

        .tbMenuWrp table tr td label span.lbldata {
            border: 2px solid #fff;
            border-radius: 50%;
            color: #fff;
            display: inline-block;
            float: left;
            font-size: 17px;
            font-weight: bold;
            padding: 2px;
            position: absolute;
            right: 4px;
            top: 7px;
        }

        .rptPurInt span.lbldata2 {
            background: #e5dcdd none repeat scroll 0 0;
            border: 1px solid #3ba8e0;
            display: block;
            font-size: 12px;
            line-height: 22px;
            margin: 14px 0 0;
            padding: 0;
            text-align: center;
        }

        .tbMenuWrp table tr td label .lblactive {
            background: #667DE8;
            color: #000000;
        }

        .lblactive label tr td {
            background: #667DE8 !important;
            color: #000 !important;
        }

        .blink_me {
            animation: blinker 5s linear infinite;
        }

        @keyframes blinker {
            50% {
                opacity: 0;
            }
        }

        .grvContentarea tr td:last-child {
            /*width: 120px;*/
        }


        .fan:nth-child(1) {
            background-color: #e6b0e1;
            color: #fff;
            height: 100%;
            line-height: 32px;
        }


        .fan {
            border-radius: 0;
            px display: inline-block;
            float: left;
            font-size: 18px;
            padding: 8px;
        }

            .fan:nth-child(1) {
                background-color: #817E24;
                border-bottom: 2px solid red;
                /* border-top: 2px solid red; */
                /* border-left: 3px solid #4800ff; */
                color: #fff;
                height: 35px;
                line-height: 14px;
            }

            .fan:nth-child(2) {
            }

            .fan:nth-child(3) {
            }

            .fan:nth-child(4) {
            }

            .fan:nth-child(5) {
            }

            .fan:nth-child(6) {
            }

            .fan:nth-child(7) {
            }
        /* for interface*/

        .circle-tile {
            margin-bottom: 15px;
            text-align: center;
            width: 87px;
        }

        .tbMenuWrp table tr td {
            /*height: 50px;*/
            width: 90px;
            padding: 0 0;
            float: left;
            list-style: none;
            margin: 0 3px;
            color: #fff;
            text-align: center;
            /*border: 2px solid #D1D735;*/
            /*-webkit-border-radius: 30px;
            -moz-border-radius: 30px;
            border-radius: 30px;*/
            cursor: pointer;
            background: #fff;
            position: relative;
        }


        .circle-tile-heading {
            border: 3px solid rgba(255, 255, 255, 0.3);
            border-radius: 100%;
            color: #FFFFFF;
            font-size: 12px;
            font-family: Calibri,Arial !important;
            height: 38px;
            margin: -2px auto -22px;
            padding: 8px 4px;
            position: relative;
            text-align: center;
            transition: all 0.3s ease-in-out 0s;
            width: 42px;
        }

            .circle-tile-heading .fa {
                line-height: 80px;
            }

        .circle-tile-content {
            padding-top: 18px;
            border-radius: 0px 15px;
            font-family: Calibri;
            font-size: 12px;
        }

        .circle-tile-number {
            font-size: 26px;
            font-weight: 700;
            line-height: 1;
            padding: 5px 0 15px;
        }

        .circle-tile-description {
            text-transform: capitalize;
        }

        .circle-tile-footer {
            background-color: rgba(0, 0, 0, 0.1);
            color: rgba(255, 255, 255, 0.5);
            display: block;
            padding: 5px;
            transition: all 0.3s ease-in-out 0s;
        }

            .circle-tile-footer:hover {
                background-color: rgba(0, 0, 0, 0.2);
                color: rgba(255, 255, 255, 0.5);
                text-decoration: none;
            }

        .circle-tile-heading.dark-blue:hover {
            background-color: #8E44AD;
        }

        .circle-tile-heading.green:hover {
            background-color: #05F37C;
        }

        .circle-tile-heading.orange:hover {
            background-color: #34495E;
        }

        .circle-tile-heading.blue:hover {
            background-color: #2473A6;
        }

        .circle-tile-heading.red:hover {
            background-color: #16A085;
        }

        .circle-tile-heading.purple:hover {
            background-color: #E74C3C;
        }

        .circle-tile-heading.deep-sky-blue:hover {
            background-color: #0179A8;
        }

        .circle-tile-heading.deep-pink:hover {
            background-color: #B76BA3
        }

        .circle-tile-heading.lime:hover {
            background-color: #00BFFF;
        }

        .circle-tile-heading.chocolate:hover {
            background-color: #32CD32;
        }

        .circle-tile-heading.blue-violet:hover {
            background-color: #FF1493;
        }

        .tile-img {
            text-shadow: 2px 2px 3px rgba(0, 0, 0, 0.9);
        }


        .green {
            background-color: #16A085;
        }


        .orange {
            background-color: #F39C12;
        }

        .red {
            background-color: #E74C3C;
        }

        .purple {
            background-color: #8E44AD;
        }


        .yellow {
            background-color: #F1C40F;
        }

        .purple {
            background-color: #8E44AD;
        }

        .deep-sky-blue {
            background-color: #0179A8;
        }

        .deep-pink {
            background-color: #B76BA3;
        }

        .danger {
            background: #DC3545;
        }

        .text-lime {
            color: #32CD32;
        }

        .deep-green {
            background: #00A28A;
        }

        .txt-white {
            color: white;
        }
    </style>
    <script>
        function OpenReqVehcl() {

            $('#ReqVchlModal').modal('toggle');
        }
        function CloseVehcl() {

            $('#ReqVchlModal').modal('toggle');
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
            <div class="card mt-5">
                <div class="card-header d-flex ">

                    <div class="p-2 mr-auto">
                        <h6 class="card-title" style="margin: 0;">Vehicles Interface</h6>
                    </div>
                    <div class="ml-auto p-2">
                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary btn-sm ml-auto" OnClick="ReqvchlModal_click"><i class="fa fa-plus"></i> Add Vehicles</asp:LinkButton>

                    </div>
                </div>
                <div class="card-body">
                    <div class="panel with-nav-tabs panel-primary">
                        <fieldset class="tabMenu">
                            <div class="form-horizontal">
                                <div class="tbMenuWrp nav nav-tabs rptPurInt">
                                    <asp:RadioButtonList ID="radiobtnv" runat="server" AutoPostBack="true" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="0"><div class="circle-tile"><a><div class="circle-tile-heading deep-sky-blue counter">0</div></a><div class="circle-tile-content deep-sky-blue"><div class="circle-tile-description txt-white"> Queue</div></div></div></asp:ListItem>
                                        <asp:ListItem Value="1"><div class="circle-tile"><a><div class="circle-tile-heading purple counter">0</div></a><div class="circle-tile-content purple"><div class="circle-tile-description txt-white"> Process</div></div></div></asp:ListItem>
                                        <asp:ListItem Value="2"><div class="circle-tile"><a><div class="circle-tile-heading  deep-pink counter">0</div></a><div class="circle-tile-content  deep-pink"><div class="circle-tile-description txt-white"> Approval</div></div></div></asp:ListItem>
                                        <asp:ListItem Value="3"><div class="circle-tile"><a><div class="circle-tile-heading  orange counter">0</div></a><div class="circle-tile-content  orange"><div class="circle-tile-description txt-white"> Generate</div></div></div></asp:ListItem>
                                        <asp:ListItem Value="4"><div class="circle-tile"><a><div class="circle-tile-heading  deep-green counter">0</div></a><div class="circle-tile-content  deep-green"><div class="circle-tile-description txt-white"> Generate</div></div></div></asp:ListItem>
                                        <asp:ListItem Value="5"><div class="circle-tile"><a><div class="circle-tile-heading  bg-danger text-white counter">0</div></a><div class="circle-tile-content bg-danger"><div class="circle-tile-description txt-white text-white"> Cancelled</div></div></div></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                        </fieldset>
                        <div>
                        </div>
                    </div>



                    <div class="modal" id="ReqVchlModal" data-backdrop="static" data-keyboard="false">
                        <div class="modal-dialog modal-xl">
                            <div class="modal-content">
                                <div class="modal-header bg-light">
                                    <h6 class="modal-title">Add Vehicles</h6>
                                    <asp:LinkButton ID="CloseVehcl" runat="server" CssClass="close close_btn" OnClientClick="CloseVehcl();" data-dismiss="modal"> &times; </asp:LinkButton>
                                </div>
                                <div class="modal-body mt-3">
                                    <div class="row">
                                        <div class="col-lg-7">
                                            <div class="row">
                                                <div class="col-lg-4">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblempid" runat="server">Emp Id</asp:Label>
                                                        <asp:TextBox ID="txtVid" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-lg-4">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label1" runat="server">Name</asp:Label>
                                                        <asp:TextBox ID="txtRegNo" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-lg-4">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label2" runat="server">Designation</asp:Label>
                                                        <asp:TextBox ID="txtModel" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                    </div>
                                                </div>

                                     
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-4">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label4" runat="server">Department</asp:Label>
                                                        <asp:TextBox ID="txtop" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                    </div>
                                                </div>

                                         

                                                <div class="col-lg-4">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label6" runat="server">Start Time</asp:Label>
                                                        <asp:TextBox ID="txtstartdat" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-lg-4">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label7" runat="server">End Time</asp:Label>
                                                        <asp:TextBox ID="txtenddat" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                             <div class="row">
                                   
                                                <div class="col-lg-6">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label5" runat="server">Destination</asp:Label>
                                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control form-control-sm" TextMode="MultiLine" Rows="4" Height="80"></asp:TextBox>
                                                    </div>
                                                </div>
                                                 
                                                <div class="col-lg-6">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label3" runat="server">Purpose</asp:Label>
                                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control form-control-sm" TextMode="MultiLine" Rows="4" Height="80"></asp:TextBox>
                                                    </div>
                                                </div>

                                       
                                            </div>
                                                <div class="row d-flex justify-content-center mb-3">
                                                    
                                                    <asp:HyperLink runat="server" CssClass="btn btn-success btn-sm text-white">Send Request</asp:HyperLink>
                                          
                                                </div>
                                        </div>
                                        <div class="col-lg-5">
                                                    <h5 class="text-center font-weight-bold">Available Vehicles</h5>
                                            <hr />
                                <table class="table table-bordered table-striped table-hover table-bordered grvContentarea">
                                    <thead class="bg-light">
                                        <tr>

                                            <th scope="col"> Vehicle</th>
                                            <th scope="col">From</th>
                                            <th scope="col">To</th>
                                             <th scope="col">Model No</th>
                                            <th scope="col">Status</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>

                                            <td>Application for vehicle</td>
                                            <td>2022-10-10</td>
                                            <td>@URL</td>
                                                         <td>2022-10-10</td>
                                            <td>@URL</td>
                                        </tr>
                                        <tr>

                                            <td>Application for vehicle</td>
                                            <td>2022-10-10</td>
                                            <td>@URL</td>
                                                         <td>2022-10-10</td>
                                            <td>@URL</td>
                                        </tr>

                                    </tbody>
                                </table>
                 
                                        </div>
                                    </div>
                                









                                </div>
                            </div>
                        </div>
                    </div>
        </ContentTemplate>

    </asp:UpdatePanel>



</asp:Content>
