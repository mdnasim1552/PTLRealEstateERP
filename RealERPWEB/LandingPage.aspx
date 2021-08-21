<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LandingPage.aspx.cs" Inherits="RealERPWEB.LandingPage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="jquery.min.js"></script>
    <style>
        .title {
            font-size: 17px;
            font-weight: bold;
            color: #ffffff;
            background-color: #337AB7;
        }

        /*-------------marquee-----------------*/




        .boxShadow {
            -webkit-border-bottom-right-radius: 5px;
            -webkit-border-bottom-left-radius: 5px;
            -moz-border-radius-bottomright: 5px;
            -moz-border-radius-bottomleft: 5px;
            border-bottom-right-radius: 5px;
            border-bottom-left-radius: 5px;
            box-shadow: 1px 0 10px 4px rgba(101, 125, 142, 0.75);
            border-bottom: 1px solid rgba(101, 125, 142, 0.75);
        }

        .box-title {
            /*position: absolute;*/
            bottom: 5px;
            left: 15px;
            font-size: 16px;
            font-weight: normal;
            text-shadow: 1px 0 1px rgba(0, 0, 0, 0.2);
            font-family: 'Agency FB' !important;
            color: #fff;
        }

        .topbtnWidthHig {
            width: 115px;
            height: 40px;
            background-color: #F6E2DB;
            color: #449D44;
            padding-top: 15px;
            border: none;
            font-family: AR ESSENCE;
        }

        /*#F8F9FA;*/

        .DashboardWidth {
            width: 68px;
        }

        .listStyle1 {
            padding-top: 4px !important;
            background-color: Transparent !important;
        }

        .listStyle2 {
            padding-top: 4px !important;
            background-color: #f9f9f9 !important;
        }

        .ddlRptMenufont {
            float: left;
            font-size: 16px !important;
            font-family: 'Agency FB';
            font-weight: normal !important;
        }

        .topfontsize {
            font-size: 30px !important;
            font-family: AR ESSENCE;
            /*color: #000000;*/
        }

        .btninterfacetop {
            border-radius: 17px;
            height: 100px;
        }

        .headerstyle {
            font-size: 12px;
        }

        .center {
            display: block;
            margin-left: auto;
            margin-right: auto;
            width: 50%;
        }
    </style>

    <script>

        function ShowModal() {
            $('#exampleModalLong').modal('show');
        }

        function ShowModalA() {
            $('#exampleModalLongA').modal('show');
        }
        function GetData(userdata, dateA) {
            //alert(dateA);
            // alert(userdata);
            // var userdataA = '"' + userdata + '"';
            // var dateAA = '"' + dateA + '"';
            //var dataget = 'userdata='+ encodeURIComponent(userdata)  & '&dateA='+ encodeURIComponent(dateA); 
            $.ajax({
                type: "post",
                url: "LandingPage.aspx/GetData",
                contentType: "application/json; charset=utf-8",
                data: '{userdata: "' + userdata + '",dateA:"' + dateA + '" }',
                dataType: "json",
                success: function (msg) {
                    var json = JSON.parse(msg.d);
                    console.log(json);
                    $('#tableid').show();
                    $('#replacepart').replaceWith($('#tableid'));              
                   

                    for (var i = 0; i < json.length; i++) {                        
                        $('#dataTable').append(
                            '<tr><th scope="row">' + (i + 1).toString() + '</th><td>' + json[i].entryuser + '</td><td>' + json[i].grpdesc + '</td><td>' + json[i].tcount + '</td></tr>'
                        );

                    }


                }
            });




        }

    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="modal fade" id="exampleModalLong" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <!-- .modal-header -->
                <div class="modal-header modal-body-scrolled">
                    <div class="row">
                        <div class="col-md-5">
                            <h5 id="exampleModalDrawerRightLabel" class="modal-title">Offline User List </h5>
                        </div>

                        <div class="col-md-4 ">
                            <asp:Label ID="Label1" runat="server" CssClass=" smLbl_to">Date: </asp:Label>
                            <asp:TextBox ID="txtdate" runat="server" autocomplete="off" CssClass="inputTxt inputName inPixedWidth120 "></asp:TextBox>
                            <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="True"
                                Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>

                        </div>
                        <div class="col-md-3 pull-right">
                            <button id="lbtnOk" type="button" runat="server" onserverclick="lbtnOk_Click" class="btn btn-primary okBtn">Ok</button>
                        </div>
                    </div>
                </div>
                <!-- /.modal-header -->
                <!-- .modal-body -->
                <div class="modal-body">
                    <div class="row" id="OfflineUsers" runat="server">
                        <h4>No Data!!!</h4>
                    </div>

                </div>
                <!-- /.modal-body -->
                <!-- .modal-footer -->
                <div class="modal-footer modal-body-scrolled">
                    <button type="button" class="btn btn-light" data-dismiss="modal">Close</button>
                </div>
                <!-- /.modal-footer -->
            </div>


        </div>
    </div>

    <div class="modal fade" id="exampleModalLongA" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <!-- .modal-header -->
                <div class="modal-header modal-body-scrolled">
                    <div class="row">
                        <div class="col-md-5">
                            <h5 class="modal-title">Top 5 Active User </h5>
                        </div>

                        <div class="col-md-4 ">
                            <asp:Label ID="Label2" runat="server" CssClass=" smLbl_to">Date: </asp:Label>
                            <asp:TextBox ID="txtdatetopfive" runat="server" autocomplete="off" CssClass="inputTxt inputName inPixedWidth120 "></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                Format="dd-MMM-yyyy" TargetControlID="txtdatetopfive"></cc1:CalendarExtender>

                        </div>
                        <div class="col-md-3 pull-right">
                            <button id="Button1" type="button" runat="server" onserverclick="lbtnOkA_Click" class="btn btn-primary okBtn">Ok</button>
                        </div>
                    </div>
                </div>
                <!-- /.modal-header -->
                <!-- .modal-body -->
                <div class="modal-body">
                    <div class="row" id="TopActivity" runat="server">
                        <h4>No Data!!!</h4>
                    </div>
                    
                    <div id="tableid" style="display: none;">
                        <button id="Button2" type="button" runat="server" onserverclick="lbtnOkA_Click" class="btn btn-warning pull-right">Go Back</button>
                        <table class="table" >
                            
                            <h5>Work List</h5>
                            <thead>
                                <tr>
                                    <th scope="col">#</th>
                                    <th scope="col">User</th>
                                    <th scope="col">Name</th>
                                    <th scope="col">Number</th>
                                </tr>
                            </thead>
                            <tbody id="dataTable">
                            </tbody>
                        </table>
                    </div>
                </div>
                <!-- /.modal-body -->
                <!-- .modal-footer -->
                <div class="modal-footer modal-body-scrolled">
                    <button type="button" class="btn btn-light" data-dismiss="modal">Close</button>
                </div>
                <!-- /.modal-footer -->
            </div>


        </div>
    </div>


    <div class="asit_container boxShadow ">


        <asp:Panel ID="pnlHousing" runat="server" Visible="false">
            <div class="row animated slideInRight" style="background-color: #ffffff !important; margin: 0px !important;">
                <div class="box-title ">


                    <div class="col-sm-4">

                        <a href="F_34_Mgt/RptUserLogDetails.aspx" target="_blank" class="btn btn-success  btn-sm btninterfacetop  topbtnWidthHig" role="button">
                            <h5 class="headerstyle">Today's Activities</h5>

                            <span class="topfontsize" runat="server" id="todaywrkcount">8</span>

                        </a>



                        <a href="#" class="btn btn-success  btn-sm btninterfacetop topbtnWidthHig" data-toggle="modal" data-target="#exampleModalLong" role="button">

                            <h5 class="headerstyle">Users Offline</h5>
                            <span class="topfontsize" runat="server" id="offlineUserCount">8</span>
                        </a>

                        <!-- Modal -->







                        <a href="#" class="btn btn-success btn-sm btninterfacetop topbtnWidthHig" data-toggle="modal" data-target="#exampleModalLongA" role="button">


                            <h5 class="headerstyle">Top Active Users</h5>
                            <span class="topfontsize">5</span>

                        </a>


                        <br />
                        <br />
                        <br />
                        <img src="Image/business_logo/housing.png" style="width: 300px; height: 200px;" class="img img-responsive" />
                        <br />
                        <br />
                        <br />

                        <br />




                        <div class="col-md-4">
                            <a href="DeafultMenu.aspx?Type=8010" target="_blank" class="btn btn-success btn-xs topbtnWidthHig" role="button">Flow</a>

                        </div>
                        <div class="col-md-4">
                            <a href="StepofOperation.aspx?moduleid=35" target="_blank" class="btn btn-success btn-xs topbtnWidthHig" role="button">Control Panel</a>

                        </div>

                        <div class="col-md-4">
                            <a href="#" target="_blank" class="btn btn-success btn-xs topbtnWidthHig" role="button">Development Tools</a>

                        </div>

                        <div class="clear-fix"></div>
                        <div class="col-md-12" style="margin-top: 5px !important; padding: 0px !important;">



                            <a href="F_22_Sal/SalesInformation.aspx?Type=Report&comcod=" target="_blank" class="btn btn-primary btn-xs DashboardWidth" role="button"><span class="glyphicon glyphicon-list-alt"></span>
                                <br />
                                Sales</a>
                            <a href="F_14_Pro/PurInformation.aspx?Type=Report&comcod=" class="btn btn-warning btn-xs DashboardWidth" target="_blank" role="button"><span class="glyphicon glyphicon-file"></span>
                                <br />
                                Purchase</a>
                            <a href="F_18_MAcc/AccDashBoard.aspx?Type=Report&comcod=3336" class="btn btn-danger btn-xs DashboardWidth" target="_blank" role="button"><span class="glyphicon glyphicon-signal"></span>
                                <br />
                                Accounts</a>
                            <a href="F_08_PPlan/ConstructionInfo.aspx?Type=Report&comcod=" class="btn btn-info btn-xs DashboardWidth" target="_blank" role="button"><span class="glyphicon glyphicon-usd"></span>
                                <br />
                                Construction</a>

                            <a href="F_32_Mis/RptMisMasterBgd.aspx?Type=InvPlan&comcod=" class="btn btn-success btn-xs DashboardWidth" target="_blank" role="button"><span class="glyphicon glyphicon-dashboard"></span>
                                <br />
                                Project</a>

                        </div>

                        <div class="clear-fix"></div>

                    </div>



                    <aside class="col-sm-4" style="clear: right;">
                        <%--   <p>Filter 2</p>--%>


                        <div class="card">
                            <article class="card-group-item panel-success">
                                <div class="panel-heading" style="font-family: AR ESSENCE;">
                                    <h3 class="panel-title">
                                        <span class="glyphicon glyphicon-bookmark"></span>
                                        <a class="collapsed card-link text-center" data-toggle="collapse" href="#collapseFIRST">Modules
                                        </a>
                                    </h3>
                                </div>



                                <div id="collapseFIRST" class="collapse filter-content " style="height: 480px; line-height: 9px; font-family: AR ESSENCE;">
                                    <div class="list-group list-group-flush" id="modlist" runat="server">
                                        <%--<a href="StepofOperation.aspx?moduleid=05" class="list-group-item">Business Plan<span class="float-right badge badge-light round"></span></a>
                                        <a href="StepofOperation.aspx?moduleid=01" class="list-group-item">Land Feasibility  <span class="float-right badge badge-light round"></span></a>
                                        <a href="StepofOperation.aspx?moduleid=04" class="list-group-item">Budgetary Control <span class="float-right badge badge-light round"></span></a>
                                        <a href="StepofOperation.aspx?moduleid=08" class="list-group-item">Project Planning <span class="float-right badge badge-light round"></span></a>
                                        <a href="StepofOperation.aspx?moduleid=09" class="list-group-item">Project Implementation <span class="float-right badge badge-light round"></span></a>
                                        <a href="StepofOperation.aspx?moduleid=12" class="list-group-item">Inventory <span class="float-right badge badge-light round"></span></a>
                                        <a href="StepofOperation.aspx?moduleid=14" class="list-group-item">Purchase <span class="float-right badge badge-light round"></span></a>

                                        <a href="StepofOperation.aspx?moduleid=21" class="list-group-item">Customer Relation Management(CRM) <span class="float-right badge badge-light round"></span></a>
                                        <a href="StepofOperation.aspx?moduleid=22" class="list-group-item">Sales <span class="float-right badge badge-light round"></span></a>
                                        <a href="StepofOperation.aspx?moduleid=23" class="list-group-item">Credit Realization/Recovery <span class="float-right badge badge-light round"></span></a>
                                        <a href="StepofOperation.aspx?moduleid=24" class="list-group-item">Customer Care <span class="float-right badge badge-light round"></span></a>
                                        <a href="StepofOperation.aspx?moduleid=29" class="list-group-item">Fixed Assets <span class="float-right badge badge-light round"></span></a>

                                        <a href="StepofOperation.aspx?moduleid=17" class="list-group-item">Accounts <span class="float-right badge badge-light round"></span></a>
                                        <a href="StepofOperation.aspx?moduleid=35" class="list-group-item">Control Panel <span class="float-right badge badge-light round"></span></a>
                                        <a href="StepofOperation.aspx?moduleid=32" class="list-group-item">MIS <span class="float-right badge badge-light round"></span></a>
                                        --%>
                                    </div>
                                    <!-- list-group .// -->
                                </div>




                                <!-- .metric -->

                                <!-- /.metric -->




                            </article>

                            <!-- card-group-item.// -->




                            <!-- card-group-item.// -->
                        </div>

                        <!-- card.// -->



                    </aside>









                    <aside class="col-sm-4">
                        <%--   <p>Filter 2</p>--%>


                        <div class="card">
                            <article class="card-group-item  panel-success">
                                <div class="panel-heading" style="font-family: AR ESSENCE;">
                                    <h3 class="panel-title">
                                        <span class="glyphicon glyphicon-bookmark"></span>Process Interface</h3>
                                </div>


                                <div class="filter-content " style="height: 480px; line-height: 9px; font-family: AR ESSENCE;">
                                    <div class="list-group list-group-flush ">
                                        <a href="F_99_Allinterface/BusinessDashboard.aspx?Type=Report&comcod=" target="_blank" class="list-group-item">Business Development<span class="float-right badge badge-light round"></span></a>
                                        <a href="F_99_Allinterface/FeasibilityInterface.aspx" target="_blank" class="list-group-item">Land Feasibility  <span class="float-right badge badge-light round"></span></a>
                                        <a href="F_99_Allinterface/InventoryInterface.aspx" target="_blank" class="list-group-item">Inventory <span class="float-right badge badge-light round"></span></a>
                                        <a href="F_99_Allinterface/BudgetInterface.aspx" target="_blank" class="list-group-item">Budget <span class="float-right badge badge-light round"></span></a>

                                        <a href="F_99_Allinterface/RptPurInterface.aspx?Type=Report&comcod=" target="_blank" class="list-group-item">Purchase <span class="float-right badge badge-light round"></span></a>
                                        <a href="F_99_Allinterface/RptEngInterface.aspx" target="_blank" class="list-group-item">General Bill <span class="float-right badge badge-light round"></span></a>
                                        <a href="F_99_Allinterface/SubContractorBillInterface.aspx?Type=Report&comcod=" target="_blank" class="list-group-item">Sub-Contract <span class="float-right badge badge-light round"></span></a>
                                        <a href="F_15_DPayReg/BillRegInterface.aspx?Type=Report&comcod=" target="_blank" class="list-group-item">Bill Register <span class="float-right badge badge-light round"></span></a>
                                        <a href="F_99_Allinterface/SalesInterface.aspx?Type=Report&comcod=" target="_blank" class="list-group-item">Recovery <span class="float-right badge badge-light round"></span></a>
                                        <a href="F_99_Allinterface/AccountInterface.aspx?Type=Report&comcod=" target="_blank" class="list-group-item">Accounts <span class="float-right badge badge-light round"></span></a>
                                        <a href="F_17_Acc/AllVoucherTopSheet.aspx" target="_blank" class="list-group-item">Voucher 360 <sup>0 <span class="float-right badge badge-light round"></span></a>
                                        <%-- <a href="F_99_Allinterface/HRInterfaceTopSheet.aspx" class="list-group-item">Salary 360 <sup>0 <span class="float-right badge badge-light round"></span></a>--%>
                                        <a href="F_99_Allinterface/KPIDashboard.aspx?Type=Report&comcod=" target="_blank" class="list-group-item">Sales/CRM <span class="float-right badge badge-light round"></span></a>

                                        <a href="F_99_Allinterface/AddWorkInterface.aspx" target="_blank" class="list-group-item">Customer Care <span class="float-right badge badge-light round"></span></a>

                                        <a href="F_99_Allinterface/RptAuditInterface.aspx" target="_blank" class="list-group-item">Audit <span class="float-right badge badge-light round"></span></a>
                                        <a href="F_99_Allinterface/SMSInterface.aspx" target="_blank" class="list-group-item">SMS/Mail <span class="float-right badge badge-light round"></span></a>
                                    </div>
                                    <!-- list-group .// -->
                                </div>
                            </article>
                            <!-- card-group-item.// -->

                            <!-- card-group-item.// -->



                        </div>
                        <!-- card.// -->






                    </aside>
                    <!-- col.// -->

                    <!-- col.// -->


                    <!-- row.// -->



                </div>



            </div>


            </sup>


        </asp:Panel>



        <asp:Panel ID="pnlHousingStd" runat="server" Visible="false">
            <div class="row animated slideInRight" style="background-color: #ffffff !important; margin: 0px !important;">
                <div class="box-title ">

                    <aside class="col-sm-4" style="clear: right;">
                        <%--   <p>Filter 2</p>--%>


                        <div class="card">
                            <article class="card-group-item panel-success">
                                <div class="panel-heading" style="font-family: AR ESSENCE;">
                                    <h3 class="panel-title">
                                        <span class="glyphicon glyphicon-bookmark"></span>Modules</h3>
                                </div>

                                <div class="filter-content " style="height: 480px; line-height: 18px; font-family: AR ESSENCE;">
                                    <div class="list-group list-group-flush ">


                                        <a href="StepofOperation.aspx?moduleid=04" class="list-group-item">Budgetary Control <span class="float-right badge badge-light round"></span></a>

                                        <a href="StepofOperation.aspx?moduleid=09" class="list-group-item">Project Implementation <span class="float-right badge badge-light round"></span></a>
                                        <a href="StepofOperation.aspx?moduleid=12" class="list-group-item">Inventory <span class="float-right badge badge-light round"></span></a>
                                        <a href="StepofOperation.aspx?moduleid=14" class="list-group-item">Purchase <span class="float-right badge badge-light round"></span></a>
                                        <a href="StepofOperation.aspx?moduleid=22" class="list-group-item">Sales <span class="float-right badge badge-light round"></span></a>
                                        <a href="StepofOperation.aspx?moduleid=23" class="list-group-item">Credit Realization/Recovery <span class="float-right badge badge-light round"></span></a>
                                        <a href="StepofOperation.aspx?moduleid=17" class="list-group-item">Accounts <span class="float-right badge badge-light round"></span></a>
                                        <a href="StepofOperation.aspx?moduleid=35" class="list-group-item">Control Panel <span class="float-right badge badge-light round"></span></a>
                                        <a href="StepofOperation.aspx?moduleid=32" class="list-group-item">MIS <span class="float-right badge badge-light round"></span></a>



                                    </div>
                                    <!-- list-group .// -->
                                </div>




                                <!-- .metric -->

                                <!-- /.metric -->




                            </article>

                            <!-- card-group-item.// -->




                            <!-- card-group-item.// -->
                        </div>

                        <!-- card.// -->



                    </aside>


                    <div class="col-sm-4">

                        <a href="F_34_Mgt/RptUserLogDetails.aspx" target="_blank" class="btn btn-success btn-sm topbtnWidthHig" role="button"><span class="" id="todaywrkcount">A-</span>


                            Today's Activities</a>



                        <a href="#" class="btn btn-success btn-sm topbtnWidthHig" role="button"><span class="">B-</span>


                            Users Offline</a>




                        <a href="#" class="btn btn-success btn-sm topbtnWidthHig" role="button"><span class="">C-</span>


                            Top Active Users</a>

                        <br />
                        <br />
                        <br />

                        <img src="Image/business_logo/housing.png" style="width: 300px; height: 200px;" class="img img-responsive" />
                        <br />
                        <br />
                        <br />




                        <div class="col-md-4">
                            <a href="DeafultMenu.aspx?Type=8010" target="_blank" class="btn btn-success btn-xs topbtnWidthHig" role="button">Flow</a>

                        </div>
                        <div class="col-md-4">
                            <a href="StepofOperation.aspx?moduleid=35" target="_blank" class="btn btn-success btn-xs topbtnWidthHig" role="button">Control Panel</a>

                        </div>

                        <div class="col-md-4">
                            <a href="#" target="_blank" class="btn btn-success btn-xs topbtnWidthHig" role="button">Development Tools</a>

                        </div>

                        <div class="clear-fix"></div>
                        <div class="col-md-12" style="margin-top: 5px !important; padding: 0px !important;">



                            <a href="F_22_Sal/SalesInformation.aspx?Type=Report&comcod=" target="_blank" class="btn btn-primary btn-xs DashboardWidth" role="button"><span class="glyphicon glyphicon-list-alt"></span>
                                <br />
                                Sales</a>
                            <a href="F_14_Pro/PurInformation.aspx?Type=Report&comcod=" class="btn btn-warning btn-xs DashboardWidth" target="_blank" role="button"><span class="glyphicon glyphicon-file"></span>
                                <br />
                                Purchase</a>
                            <a href="F_18_MAcc/AccDashBoard.aspx?Type=Report&comcod=3336" class="btn btn-danger btn-xs DashboardWidth" target="_blank" role="button"><span class="glyphicon glyphicon-signal"></span>
                                <br />
                                Accounts</a>
                            <%--<a href="F_08_PPlan/ConstructionInfo.aspx?Type=Report&comcod=" class="btn btn-info btn-xs DashboardWidth" target="_blank" role="button"><span class="glyphicon glyphicon-usd"></span>
                            <br />
                            Construction</a>--%>

                            <a href="F_32_Mis/RptMisMasterBgd.aspx?Type=InvPlan&comcod=" class="btn btn-success btn-xs DashboardWidth" target="_blank" role="button"><span class="glyphicon glyphicon-dashboard"></span>
                                <br />
                                Project</a>

                        </div>



                    </div>








                    <aside class="col-sm-4">
                        <%--   <p>Filter 2</p>--%>


                        <div class="card">
                            <article class="card-group-item  panel-success">
                                <div class="panel-heading" style="font-family: AR ESSENCE;">
                                    <h3 class="panel-title">
                                        <span class="glyphicon glyphicon-bookmark"></span>Process Interface</h3>
                                </div>


                                <div class="filter-content " style="height: 480px; line-height: 18px; font-family: AR ESSENCE;">
                                    <div class="list-group list-group-flush ">


                                        <a href="F_99_Allinterface/BudgetInterface.aspx" target="_blank" class="list-group-item">Budget <span class="float-right badge badge-light round"></span></a>
                                        <a href="F_99_Allinterface/InventoryInterface.aspx" target="_blank" class="list-group-item">Inventory <span class="float-right badge badge-light round"></span></a>
                                        <a href="F_99_Allinterface/RptPurInterface.aspx?Type=Report&comcod=" target="_blank" class="list-group-item">Purchase <span class="float-right badge badge-light round"></span></a>
                                        <a href="F_99_Allinterface/RptEngInterface.aspx" target="_blank" class="list-group-item">General Bill <span class="float-right badge badge-light round"></span></a>
                                        <a href="F_99_Allinterface/SubContractorBillInterface.aspx?Type=Report&comcod=" target="_blank" class="list-group-item">Sub-Contract <span class="float-right badge badge-light round"></span></a>
                                        <a href="F_15_DPayReg/BillRegInterface.aspx?Type=Report&comcod=" target="_blank" class="list-group-item">Bill Register <span class="float-right badge badge-light round"></span></a>
                                        <a href="F_99_Allinterface/SalesInterface.aspx?Type=Report&comcod=" target="_blank" class="list-group-item">Recovery <span class="float-right badge badge-light round"></span></a>
                                        <a href="F_99_Allinterface/AccountInterface.aspx?Type=Report&comcod=" target="_blank" class="list-group-item">Accounts <span class="float-right badge badge-light round"></span></a>
                                        <a href="F_17_Acc/AllVoucherTopSheet.aspx" target="_blank" class="list-group-item">Voucher 360 <sup>0 <span class="float-right badge badge-light round"></span></a>

                                    </div>
                                    <!-- list-group .// -->
                                </div>
                            </article>
                            <!-- card-group-item.// -->

                            <!-- card-group-item.// -->



                        </div>
                        <!-- card.// -->






                    </aside>
                    <!-- col.// -->

                    <!-- col.// -->


                    <!-- row.// -->



                </div>



            </div>


            </sup>


        </asp:Panel>




    </div>






</asp:Content>

