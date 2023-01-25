<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AIinterface.aspx.cs" Inherits="RealERPWEB.F_99_Allinterface.AIinterface" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
            width: 81px;
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
            background-color: #2E4154;
        }

        .circle-tile-heading.green:hover {
            background-color: #138F77;
        }

        .circle-tile-heading.orange:hover {
            background-color: #DA8C10;
        }

        .circle-tile-heading.blue:hover {
            background-color: #2473A6;
        }

        .circle-tile-heading.red:hover {
            background-color: #CF4435;
        }

        .circle-tile-heading.purple:hover {
            background-color: #7F3D9B;
        }

        .tile-img {
            text-shadow: 2px 2px 3px rgba(0, 0, 0, 0.9);
        }

        .dark-blue {
            background-color: #34495E;
        }

        .green {
            background-color: #16A085;
        }

        .blue {
            background-color: #2980B9;
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

        .dark-gray {
            background-color: #7F8C8D;
        }

        .gray {
            background-color: #95A5A6;
        }

        .light-gray {
            background-color: #BDC3C7;
        }

        .yellow {
            background-color: #F1C40F;
        }

        .text-dark-blue {
            color: #34495E;
        }

        .text-green {
            color: #16A085;
        }

        .text-blue {
            color: #2980B9;
        }

        .text-orange {
            color: #F39C12;
        }

        .text-red {
            color: #E74C3C;
        }

        .text-purple {
            color: #8E44AD;
        }

        .text-faded {
            color: rgba(255, 255, 255, 0.7);
        }
        /*Pnael Side Bar*/
        .pnlSidebarCl {
            width: 50%;
            height: 80vh;
            position: absolute;
            right: 0;
        }

            .pnlSidebarCl .form-control {
                height: 25px;
                line-height: 25px;
                padding: 2px;
            }

        .divPnl table tr td, .divPnl table tr th {
            padding: 5px 5px;
        }
    </style>
    <script type="text/javascript" language="javascript">
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
        function SetTarget(type) {
            window.open('../RDLCViewerWin.aspx?PrintOpt=' + type, '_blank');
        }

        function checkEmptyNote() {
            OpenAddCustomer();
            OpenAddProject();
            OpenAddTask();
            OpenAddBatch();
            closecustomeradd();
            CustomerCreate();
        }
        function CustomerCreate() {

            $('#btnAdd').modal('toggle');
        }
        function OpenAddProject() {

            $('#AddProjectModal').modal('toggle');
        }
        function OpenAddTask() {

            $('#TaskCreateModal').modal('toggle');
        }



        function closecustomeradd() {
            $('#btnAdd').modal('hide');
        }
<%--        function checkEmptyNote() {
            OpenApplyLoan();
        }

        function sum() {
            var txtFirstNumberValue = document.getElementById("<%=txtLoanAmt.ClientID%>").value;


            var txtSecondNumberValue = parseInt(document.getElementById("<%=txtInstNum.ClientID%>").value);
            var result = txtFirstNumberValue.replace(/,(?=\d{3})/g, '') / txtSecondNumberValue;

            if (result < 1) {
                alert("Amount Per  Installment can not be 0")
                return;
            }
            if (!isNaN(result)) {

                document.getElementById("<%=txtAmtPerIns.ClientID%>").value = result.toFixed(2);
            }
        }--%>


        function isNumberKey(txt, evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode == 46) {
                //Check if the text already contains the . character
                if (txt.value.indexOf('.') === -1) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                if (charCode > 31 &&
                    (charCode < 48 || charCode > 57))
                    return false;
            }
            return true;
        }

        //function OpenApplyLoan() {

        //    $('#ApplyLoan').modal('toggle');
        //}

        //function ViewLoan() {
        //    $('#ApplyLoan').modal('toggle');
        //}

        //function EditLoan() {
        //    $('#ApplyLoan').modal('toggle');
        //}

        //function ModalLoanClose() {
        //    $('#ApplyLoan').appendTo("body").modal('hide');

        //    $('.modal').remove();


        //    $('.modal-backdrop show').remove();
        //    $('body').removeClass("modal-open");
        //    $('.modal-backdrop').remove()
        //    $(document.body).removeClass("modal-open");

        //}
        //function OpenDeleteModal() {
        //    $('#deleteModal').modal('toggle');
        //}

       <%-- function PrintRpt() {
            window.open('<%= ResolveUrl("../../RDLCViewerWin.aspx?PrintOpt=PDF") %>', '_blank');
      }--%>
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


            <div class="section">
                <div class="card mt-4">
                    <div class="card-header pt-2 pb-2">
                        <div class="row">
                            <div class="col-lg-1">
                                <asp:Label ID="Label1" runat="server">From</asp:Label>
                                <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                            </div>
                            <div class="col-lg-1">
                                <asp:Label ID="Label2" runat="server">To</asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true" OnTextChanged="txttodate_TextChanged"></asp:TextBox>
                                <cc1:CalendarExtender runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>
                            <div class="col-lg-2">
                                <asp:Label ID="Label3" runat="server">Project Type</asp:Label>
                                <asp:DropDownList ID="ddlLoanTypeSearch" runat="server" CssClass="form-control chzn-select">
                                    <asp:ListItem Value="0">Pilot</asp:ListItem>
                                    <asp:ListItem Value="1">sow</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-2">
                                <asp:Label ID="Label4" runat="server">Work Type</asp:Label>
                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control chzn-select">
                                    <asp:ListItem Value="0">Image Annotation</asp:ListItem>
                                    <asp:ListItem Value="1">Software Development</asp:ListItem>

                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-2">
                                <asp:Label ID="Label5" runat="server">Customer Type</asp:Label>
                                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control chzn-select">
                                    <asp:ListItem Value="0">Annotation Support</asp:ListItem>
                                    <asp:ListItem Value="1">Software Developer</asp:ListItem>

                                </asp:DropDownList>
                            </div>

                            <div class="col-md-2">
                                <div class="form-group-">
                                    <asp:LinkButton ID="lnkbtnok" runat="server" OnClick="lnkbtnok_Click" CssClass=" btn btn-primary btn-sm mt20">Ok</asp:LinkButton></li>
                                </div>
                            </div>
                            <div class="col-md-2">

                                <div class=" btn-group" role="group" aria-label="Button group with nested dropdown">
                                    <div class="btn-group" role="group">
                                        <button id="btnGroupDrop4" type="button" class="btn btn-success ml-auto bw-100 btn-sm mt20 mr-2 dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Operation</button>
                                        <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                            <div class="dropdown-arrow"></div>

                                            <asp:LinkButton ID="btnaddPrj" OnClick="btnaddPrj_Click" CssClass="dropdown-item" Style="padding: 0 10px" runat="server">Add Project</asp:LinkButton>
                                            <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl="~/F_38_AI/CustomerList" CssClass="dropdown-item" Style="padding: 0 10px">Customer List</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" NavigateUrl="~/F_38_AI/AddProject" CssClass="dropdown-item" Style="padding: 0 10px">All Project's</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" NavigateUrl="~/F_38_AI/RptOngoingProjects.aspx?Type=Report" CssClass="dropdown-item" Style="padding: 0 10px">Ongoing Projects</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink4" runat="server" Target="_blank" NavigateUrl="~/F_38_AI/CreateTask" CssClass="dropdown-item" Style="padding: 0 10px">Create Task</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink5" runat="server" Target="_blank" NavigateUrl="~/StepofOperationNew?moduleid=14" CssClass="dropdown-item" Style="padding: 0 10px">Assign Team Leader</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink6" runat="server" Target="_blank" NavigateUrl="~/StepofOperationNew?moduleid=14" CssClass="dropdown-item" Style="padding: 0 10px">Creating Invoice</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink7" runat="server" Target="_blank" NavigateUrl="~/StepofOperationNew?moduleid=14" CssClass="dropdown-item" Style="padding: 0 10px">Day Wise Work Status</asp:HyperLink>

                                        </div>
                                    </div>
                                </div>
                            </div>


                        </div>
                        <div class="row">
                            <div class="col-lg-5 col-md-6 col-sm-6">
                                <%--<asp:LinkButton ID="tblAddProjectModal" runat="server" CssClass="btn btn-primary ml-auto bw-100 btn-sm mt20 mr-2" OnClick="tblAddProjectModal_Click"><i class="fa fa-plus"></i>Add Project</asp:LinkButton>
                                <asp:LinkButton ID="tblTaskCreateModal" runat="server" CssClass="btn btn-primary ml-auto bw-100 btn-sm mt20 mr-2" OnClick="tblTaskCreateModal_Click"><i class="fa fa-plus"></i>Add Tasks</asp:LinkButton>--%>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row col-md-12">
                            <%-- <div class="panel with-nav-tabs panel-primary">
                        </div>--%>
                            <fieldset class="tabMenu">
                                <div class=" row form-horizontal">
                                    <div class="tbMenuWrp nav nav-tabs rptPurInt">
                                        <asp:RadioButtonList ID="TasktState" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="TasktState_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Selected="True"><div class="circle-tile"><a><div class="circle-tile-heading bg-green counter">0</div></a><div class="circle-tile-content green"><div class="circle-tile-description txt-white">All Projects</div></div></div></asp:ListItem>
                                            <asp:ListItem Value="1"><div class="circle-tile"><a><div class="circle-tile-heading deep-sky-blue counter">0</div></a><div class="circle-tile-content deep-sky-blue"><div class="circle-tile-description txt-white">Batch Status</div></div></div></asp:ListItem>
                                            <asp:ListItem Value="2"><div class="circle-tile"><a><div class="circle-tile-heading purple counter">0</div></a><div class="circle-tile-content purple"><div class="circle-tile-description txt-white">Assigned</div></div></div></asp:ListItem>
                                            <asp:ListItem Value="3"><div class="circle-tile"><a><div class="circle-tile-heading  bg-success counter">0</div></a><div class="circle-tile-content bg-success"><div class="circle-tile-description txt-white">Production</div></div></div></asp:ListItem>
                                            <asp:ListItem Value="4"><div class="circle-tile"><a><div class="circle-tile-heading  deep-pink counter">0</div></a><div class="circle-tile-content  deep-pink"><div class="circle-tile-description txt-white">QC</div></div></div></asp:ListItem>
                                            <asp:ListItem Value="5"><div class="circle-tile"><a><div class="circle-tile-heading  orange counter">0</div></a><div class="circle-tile-content  orange"><div class="circle-tile-description txt-white">Accept/Reject</div></div></div></asp:ListItem>
                                            <asp:ListItem Value="6"><div class="circle-tile"><a><div class="circle-tile-heading  bg-success counter">0</div></a><div class="circle-tile-content  bg-info"><div class="circle-tile-description txt-white">QA</div></div></div></asp:ListItem>
                                            <asp:ListItem Value="7"><div class="circle-tile"><a><div class="circle-tile-heading  bg-pink counter">0</div></a><div class="circle-tile-content  bg-pink"><div class="circle-tile-description txt-white">Delivery</div></div></div></asp:ListItem>
                                            <asp:ListItem Value="8"><div class="circle-tile"><a><div class="circle-tile-heading  orange counter">0</div></a><div class="circle-tile-content  orange"><div class="circle-tile-description txt-white">FeedBack</div></div></div></asp:ListItem>
                                            <asp:ListItem Value="9"><div class="circle-tile"><a><div class="circle-tile-heading bg-dark bg-gradient counter">0</div></a><div class="circle-tile-content bg-dark bg-gradient"><div class="circle-tile-description txt-white">Invoice</small></div></div></div></asp:ListItem>
                                            <asp:ListItem Value="10"><div class="circle-tile"><a><div class="circle-tile-heading  bg-info text-white counter">0</div></a><div class="circle-tile-content bg-info"><div class="circle-tile-description txt-white text-white">Collection</div></div></div></asp:ListItem>
                                            <%-- <asp:ListItem Value="7"><div class="circle-tile"><a><div class="circle-tile-heading  bg-danger text-white counter">0</div></a><div class="circle-tile-content bg-danger"><div class="circle-tile-description txt-white text-white">Loan Cancelled</div></div></div></asp:ListItem>--%>
                                        </asp:RadioButtonList>


                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <hr />
                        <div class="divPnl">
                            <asp:Panel ID="pnlAllProject" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblPage" runat="server">Page Size</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass="form-control form-control-sm">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label runat="server" ID="lblprjserchname">Project </asp:Label>
                                        <asp:DropDownList ID="ddlprjsearch" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 mt-4">
                                        <asp:LinkButton runat="server" ID="prjSearch" OnClick="prjSearch_Click" CssClass="btn btn-primary btn-sm">Search</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="table-responsive mt-2">
                                    <asp:GridView ID="gvInterface" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" Visible="True" AllowPaging="true" PageSize="15" OnPageIndexChanging="gvInterface_PageIndexChanging" OnRowDataBound="gvInterface_RowDataBound">

                                        <Columns>
                                            <asp:TemplateField HeaderText="SL # ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right; font-size: 12px;"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ProjectName" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcurrncy" runat="server" Text='<%#Eval("currncy").ToString()%>' Width="10px"></asp:Label>
                                                    <asp:Label ID="lblpactcode" runat="server" Text='<%#Eval("pactcode").ToString()%>' Width="10px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Projectcode" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblinfcod" runat="server" Text='<%#Eval("infcod").ToString()%>' Width="10px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Customer Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcustname" runat="server" Height="16px"
                                                        Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))%>'
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblordertype" runat="server" CssClass='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "ordertype")) =="Pilot" ? "btn btn-info text-white":" " %>'
                                                        Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "ordertype"))%>'
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ProjectName">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblprojectName" runat="server" Width="200px"
                                                        Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "projectName"))%>'
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ProjectType">
                                                <ItemTemplate>
                                                    <asp:Label ID="tblproj" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "typedesc")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DataSet">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldataset" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dataset")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="WorkType">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblwrktype" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "worktype")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Create date">
                                                <ItemTemplate>
                                                    <asp:Label ID="tblcreatedate" runat="server" Width="80px"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "createdate")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"": Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "createdate")).ToString("dd-MMM-yyyy")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delivery date" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldeliverydate" runat="server" Width="80px"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "deliverydate")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"": Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "createdate")).ToString("dd-MMM-yyyy")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="tbladdress" runat="server" Width="80px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "quantity")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Hour">
                                                <ItemTemplate>
                                                    <asp:Label ID="tblcountry" runat="server" Width="80px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalhour")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Batch">
                                                <ItemTemplate>
                                                    <%--//<asp:LinkButton ID="lnkVieww" runat="server" OnClick="tblAddBatch_Click" CssClass="text-primary pr-2 pl-2" ToolTip="view"><i class="fa fa-eye"></i></asp:LinkButton>--%>

                                                    <asp:LinkButton runat="server" ID="tblAddBatch" OnClick="tblAddBatch_Click" ToolTip="batchadd" CssClass="btn btn-warning btn-sm pr-2 pl-2">
                                                        <i class="fas fa-plus-circle"></i>
                                                        (<asp:Label ID="tblcount" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ttlbatch")) %>'></asp:Label>)
                                                    </asp:LinkButton>


                                                </ItemTemplate>
                                                <ItemStyle Width="80px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lnkprjDAshboard" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="text-primary pr-2 pl-2"
                                                        ToolTip="Project Dashboard"><i class="fa fa-pie-chart"></i></asp:HyperLink>

                                                    <asp:HyperLink ID="lnkprjView" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="text-primary pr-2 pl-2"
                                                        ToolTip="Project View"><i class="fa fa-eye"></i></asp:HyperLink>


                                                    <asp:LinkButton ID="btnRemove" runat="server" CssClass="text-danger d-none pr-2" ToolTip="delete"><i class="fa fa-trash"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="btnEdit" runat="server" CssClass="text-primary" ToolTip="edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>


                                        </Columns>
                                        <%--<FooterStyle CssClass="grvFooter" />--%>

                                        <PagerStyle CssClass="gvPagination" />

                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlStatus" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="Label31" runat="server">Page Size</asp:Label>
                                        <asp:DropDownList ID="ddlBatchPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBatchPage_SelectedIndexChanged" CssClass="form-control form-control-sm">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label34" runat="server">Batch List</asp:Label>
                                        <asp:DropDownList ID="ddlsearchBatchlist" runat="server" AutoPostBack="True" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 mt-3">
                                        <asp:LinkButton runat="server" ID="btnbatchSearch" OnClick="btnbatchSearch_Click" CssClass="btn btn-primary btn-sm">Search</asp:LinkButton>

                                    </div>

                                </div>
                                <div class="row mt-2">
                                    <asp:GridView ID="gv_BatchList" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" Visible="True" AllowPaging="true" PageSize="15" Width="100%" OnPageIndexChanging="gv_BatchList_PageIndexChanging" OnRowDataBound="gv_BatchList_RowDataBound">
                                        <Columns>

                                            <asp:TemplateField HeaderText="SL # ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right; font-size: 12px;"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                                        ForeColor="Black"></asp:Label>

                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BatchID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbatchid" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "id")) %>'></asp:Label>

                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Projectcode" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstatusprjid" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prjid")) %>'></asp:Label>

                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ProjectName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbatchprojname" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "projname")) %>'
                                                        Width="150px"></asp:Label>

                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Batch Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbatchuniqid" runat="server" Height="16px" CssClass="badge badge-pill badge-primary"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "id")) %>'></asp:Label>

                                                    <asp:Label ID="lblbatchbatchid" runat="server" Height="16px" Width="200px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchid")) %>'></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Data <br> Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbatchdatasettype" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "datasettype")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Work <br> Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbatchworktype" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "worktype")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Data Set <br>  QTY">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbatchdatasetqty" runat="server" Height="16px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "datasetqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbatchrate" runat="server" Height="16px" Visible="false"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.0;(#,##0.0); ") %>'
                                                        Width="80px"></asp:Label>
                                                    <asp:Label ID="lblbatchamount" runat="server" Height="16px" Visible="false"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                    <asp:Label ID="lblbatchpwrkperhour" runat="server" Height="16px" Visible="false"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pwrkperhour")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                    <asp:Label ID="lblbatchempcapacity" runat="server" Height="16px" Visible="false"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "empcapacity")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                    <asp:Label ID="lblbatchestimatemanpower" runat="server" Height="16px" Visible="false"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "estimatemanpower")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total <br> Hour">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbatchtotalhour" runat="server" Height="16px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalhour")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Start <br> Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbatchstartdate" runat="server" Height="16px"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "startdate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delivery <br> Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbatchdeliverydate" runat="server" Height="16px"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "deliverydate")).ToString("dd-MMM-yyyy") %>' Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Man <br>Power" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblestimatemanpower" runat="server" Height="16px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "estimatemanpower")).ToString("#,##0;(#,##0); ") %>' Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delivery">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbatchdoneqty" runat="server" Height="18px" CssClass="text-blue " Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roletype"))=="95003"  ? true:false %>'
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "doneqty")).ToString("#,##0;(#,##0); ") %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="work Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbatchworkstatus" runat="server" CssClass="badge badge-pill badge-info"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "workstatus")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="btnview" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="text-primary pr-2 pl-2" ToolTip="view"><i class="fa fa-eye"></i></asp:HyperLink>

                                                    <asp:LinkButton ID="btnbatchupdate" OnClick="btnbatchupdate_Click" runat="server" CssClass="text-primary" ToolTip="edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                        </Columns>

                                        <PagerStyle CssClass="gvPagination" />

                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlAssign" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="Label32" runat="server">Page Size</asp:Label>
                                        <asp:DropDownList ID="ddlassignpagesize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlassignpagesize_SelectedIndexChanged" CssClass="form-control form-control-sm">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label ID="Label35" runat="server">Title List</asp:Label>
                                        <asp:DropDownList ID="ddlsearchtitle" runat="server" AutoPostBack="True" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 mt-3">
                                        <asp:LinkButton runat="server" ID="btnsearchtitle" OnClick="btnsearchtitle_Click" CssClass="btn btn-primary btn-sm">Search</asp:LinkButton>

                                    </div>
                                </div>
                                <div class="table-responsive mt-2">
                                    <asp:GridView ID="gvAssingJob" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" Visible="True" AllowPaging="true" PageSize="15" OnRowDataBound="gvAssingJob_RowDataBound" OnPageIndexChanging="gvAssingJob_PageIndexChanging">

                                        <Columns>
                                            <asp:TemplateField HeaderText="SL # ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                        Style="text-align: right; font-size: 12px;"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                            </asp:TemplateField>
                                            <%--  <asp:TemplateField HeaderText="ProjectName" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpactcode" runat="server" Text='<%#Eval("pactcode").ToString()%>' Width="10px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="EmpID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblempid" runat="server" Text='<%#Eval("empid").ToString()%>' Width="10px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Project Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblprjname" runat="server" Width="100px"
                                                        Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "projname"))%>'
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order Type" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblordertype" runat="server"
                                                        Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "ordertype"))%>'
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Task Title">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblprojectName" runat="server" Width="150px"
                                                        Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "tasktitle"))%>'
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Batch Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbljobiduniq" runat="server" class="badge badge-pill badge-info"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobid")) %>'></asp:Label>
                                                    <asp:Label ID="tblproj" runat="server" Width="200px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchname")) %>'></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="DataSet Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="tbldataset" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "datasettype")) %>'></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="WorkType">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvworktype" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "worktype")) %>'></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvempname" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Create date">
                                                <ItemTemplate>
                                                    <asp:Label ID="tblcreatedate" runat="server" Width="80px"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "startdate")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"": Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "startdate")).ToString("dd-MMM-yyyy")%>'></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Assigned QTY">
                                                <ItemTemplate>
                                                    <asp:Label ID="tbladdress" runat="server" Width="80px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "assignqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Assign Per Hour">
                                                <ItemTemplate>
                                                    <asp:Label ID="tblcountry" runat="server" Width="80px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "empworkhour")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <%-- NavigateUrl="~/F_38_AI/MyTasks?Empid=930100101005" --%>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hylnkView" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="text-primary pr-2 pl-2" ToolTip="view"><i class="fa fa-eye"></i></asp:HyperLink>
                                                    <%-- <asp:LinkButton ID="btnRemove" runat="server" CssClass="text-danger pr-2" ToolTip="delete"><i class="fa fa-trash"></i></asp:LinkButton>
                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="text-primary" ToolTip="edit"><i class="fa fa-edit"></i></asp:LinkButton>--%>
                                                </ItemTemplate>
                                                <ItemStyle Width="80px" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <%--<FooterStyle CssClass="grvFooter" />--%>

                                        <PagerStyle CssClass="gvPagination" />

                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlProduction" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="Label33" runat="server">Page Size</asp:Label>
                                        <asp:DropDownList ID="ddlProduction_page_Size" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProduction_page_Size_SelectedIndexChanged" CssClass="form-control form-control-sm">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label ID="Label36" runat="server">Task Title</asp:Label>
                                        <asp:DropDownList ID="ddlsearchtasktitle" runat="server" AutoPostBack="True" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 mt-3">
                                        <asp:LinkButton runat="server" ID="btntasktitle" OnClick="btntasktitle_Click" CssClass="btn btn-primary btn-sm">Search</asp:LinkButton>

                                    </div>
                                </div>
                                <div class="table-responsive mt-2">
                                    <asp:GridView ID="gv_Production" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" Visible="True" AllowPaging="true" PageSize="15" OnRowDataBound="gv_Production_RowDataBound" OnPageIndexChanging="gv_Production_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL # ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                        Style="text-align: right; font-size: 12px;"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="jobid" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvpjobid" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobid"))%>'
                                                        Width="80px"></asp:Label>

                                                    <asp:Label ID="lblProdtaskid" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "taskid"))%>'
                                                        Width="80px"></asp:Label>


                                                    <asp:Label ID="lblgvpprjid" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prjid"))%>'
                                                        Width="80px"></asp:Label>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EmpID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvpassignuser" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assignuser")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BatchID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvbatchid" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchid"))%>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Create <br> Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcreatedate" runat="server"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "createdate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Role Type " Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvvelocitytype" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roletype")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Assign Type " Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvassigntype" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assigntype")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Project Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvprojectName" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "projectName")) %>'
                                                        Width="150px"></asp:Label>
                                                    <asp:Label ID="lblgvprodu" runat="server"
                                                        class='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "ordertype"))=="Pilot") ? " badge badge-pill badge-danger":" badge badge-pill badge-success" %>'
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ordertype")) %>'></asp:Label>
                                                </ItemTemplate>

                                                <%--//<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />--%>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Batch <br> Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvbatchname" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchname")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Name">
                                                <ItemTemplate>
                                                    <asp:HyperLink runat="server" ID="lnkbtnprodlink" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="text-primary pr-2 pl-2">
                                                        <asp:Label ID="lblgvpempname" runat="server" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                            Width="150px"></asp:Label>
                                                    </asp:HyperLink>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Task <br> Title">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvtasktitle" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tasktitle")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Annator<br> ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvannoid" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "annoid")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Assigned<br> Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvvelocityqty" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "assignqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Job Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvjobstatus" runat="server" CssClass="badge badge-pill badge-info"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobstatus")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Done<br>QTY">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvdoneqty" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "doneqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                    <asp:LinkButton runat="server" ID="btnproductionlink" OnClick="btnproductionlink_Click" Visible='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "doneqty")) > 0 ? true:false %>' CssClass="text-primary pr-2 pl-2" ToolTip="Assign for QC"><i class="fa fa-user-plus"></i></asp:LinkButton>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Skip<br>QTY">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvskipqty" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "skipqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pending<br>QTY">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvpendingqty" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pendingqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Spent <br> Hour" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvtimeinminute" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "timeinminute")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Pediction <br> Time">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:HyperLink runat="server" ID="hybtnprodlink" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="text-primary pr-2 pl-2" ToolTip="view"><i class="fa fa-eye"></i></asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <PagerStyle CssClass="gvPagination" />

                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>

                            </asp:Panel>
                            <asp:Panel ID="pnelQC" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="Label37" runat="server">Page Size</asp:Label>
                                        <asp:DropDownList ID="dllQA1search" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dllQA1search_SelectedIndexChanged" CssClass="form-control form-control-sm">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label ID="Label38" runat="server">Task Title</asp:Label>
                                        <asp:DropDownList ID="ddltastileqa1" runat="server" AutoPostBack="True" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 mt-3">
                                        <asp:LinkButton runat="server" ID="btnqa1search" OnClick="btnqa1search_Click" CssClass="btn btn-primary btn-sm">Search</asp:LinkButton>

                                    </div>
                                </div>


                                <div class="table-responsive mt-2">
                                    <asp:GridView ID="gv_QCQA" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" Visible="True" AllowPaging="true" PageSize="15" OnRowDataBound="gv_QCQA_RowDataBound" OnPageIndexChanging="gv_QCQA_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL # ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right; font-size: 12px;"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="jobid" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvqcjobid" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobid"))%>'
                                                        Width="80px"></asp:Label>
                                                    <asp:Label ID="lblQCtaskid" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "taskid"))%>'
                                                        Width="80px"></asp:Label>
                                                    <asp:Label ID="lblgvqcprjid" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prjid"))%>'
                                                        Width="80px"></asp:Label>


                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EmpID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvaqcssignuser" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assignuser")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BatchID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvqcbatchid" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchid"))%>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Create <br> Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvqccreatedate" runat="server" Height="16px"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "createdate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Role Type " Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvqcvelocitytype" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roletype")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Project Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvqcprojectName" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "projectName")) %>'
                                                        Width="150px"></asp:Label>
                                                    <asp:Label ID="lblgvqcorder" runat="server"
                                                        class='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "ordertype"))=="Pilot") ? " badge badge-pill badge-danger":" badge badge-pill badge-success" %>'
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ordertype")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Batch <br> Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvqcbatchname" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchname")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EmpName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvqcempname" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Task <br> Title">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvqctasktitle" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tasktitle")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Role <br> Type ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvqcroletypedesc" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roletypedesc")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Annator ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvqcannoid" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "annoid")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Assign<br> Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvqcvelocityqty" runat="server" Height="16px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "assignqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Job Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvqcjobstatus" runat="server" CssClass="badge badge-pill badge-info"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobstatus")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Done<br>QTY">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvqcdoneqty" runat="server" Height="16px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "doneqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px"></asp:Label>
                                                    <asp:LinkButton runat="server" ID="btnqclink" OnClick="btnqclink_Click" Visible='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "doneqty")) > 0 ? true:false %>' CssClass="text-primary pr-2 pl-2" ToolTip="Assign for QC"><i class="fa fa-user-plus"></i></asp:LinkButton>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Skip<br>QTY">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvqcskipqty" runat="server" Height="16px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "skipqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Spent <br> Hour" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvqctimeinminute" runat="server" Height="16px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "timeinminute")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Pending <br> QTY">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvqcpendingqty" runat="server" Height="16px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pendingqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:HyperLink runat="server" ID="hybtnqclink" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="text-primary pr-2 pl-2" ToolTip="view"><i class="fa fa-eye"></i></asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <PagerStyle CssClass="gvPagination" />

                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnelQA" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="Label39" runat="server">Page Size</asp:Label>
                                        <asp:DropDownList ID="ddlpagingQC1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpagingQC1_SelectedIndexChanged" CssClass="form-control form-control-sm">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label ID="Label40" runat="server">Task Title</asp:Label>
                                        <asp:DropDownList ID="ddllistoftask" runat="server" AutoPostBack="True" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 mt-3">
                                        <asp:LinkButton runat="server" ID="btnsearhqc1" OnClick="btnsearhqc1_Click" CssClass="btn btn-primary btn-sm">Search</asp:LinkButton>

                                    </div>
                                </div>




                                <div class="table-responsive mt-2">
                                    <asp:GridView ID="gv_AssignQA" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" Visible="True" AllowPaging="true" PageSize="15" OnRowDataBound="gv_AssignQA_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL # ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right; font-size: 12px;"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="jobid" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvqajobid" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobid"))%>'
                                                        Width="80px"></asp:Label>
                                                    <asp:Label ID="lblQAtaskid" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "taskid"))%>'
                                                        Width="80px"></asp:Label>
                                                    <asp:Label ID="lblqaprjid" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prjid"))%>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EmpID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvaqassignuser" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assignuser")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BatchID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvqabatchid" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchid"))%>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Create <br> Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvqacreatedate" runat="server" Height="16px"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "createdate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Role Type " Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvqavelocitytype" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roletype")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Project Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvqaprojectName" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "projectName")) %>'
                                                        Width="150px"></asp:Label>
                                                    <asp:Label ID="lblgvqaorder" runat="server"
                                                        class='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "ordertype"))=="Pilot") ? " badge badge-pill badge-danger":" badge badge-pill badge-success" %>'
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ordertype")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Batch <br> Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvqabatchname" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchname")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EmpName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvqaempname" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Task <br> Title">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvqatasktitle" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tasktitle")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Role <br> Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvqaroletypedesc" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roletypedesc")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Annator<br> ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvqaannoid" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "annoid")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Assign<br> Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvqavelocityqty" runat="server" Height="16px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "assignqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Job Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvqajobstatus" runat="server" CssClass="badge badge-pill badge-info"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobstatus")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Done<br>QTY">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvqadoneqty" runat="server" Height="16px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "doneqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                    <asp:LinkButton runat="server" ID="btnqalink" OnClick="btnqalink_Click" Visible='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "doneqty")) > 0 ? true:false %>' CssClass="text-primary pr-2 pl-2" ToolTip="Assign for QC"><i class="fa fa-user-plus"></i></asp:LinkButton>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Skip<br>QTY">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvqaskipqty" runat="server" Height="16px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "skipqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Spent <br> Hour" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvqatimeinminute" runat="server" Height="16px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "timeinminute")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pending<br>QTY">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvqapendingqty" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pendingqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>

                                                    <asp:HyperLink runat="server" ID="hybtnqalink" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="text-primary pr-2 pl-2" ToolTip="view"><i class="fa fa-eye"></i></asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <PagerStyle CssClass="gvPagination" />

                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnelFeedBack" runat="server" Visible="false">
                                <h2>Feedback</h2>
                            </asp:Panel>
                            <asp:Panel ID="Pneldelivery" runat="server" Visible="false">
                                <div class="table-responsive">
                                    <asp:GridView ID="gv_Delivery" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" Visible="True" AllowPaging="true" PageSize="15" OnRowDataBound="gv_Delivery_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL # ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right; font-size: 12px;"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="jobid" Visible="false">
                                                <ItemTemplate>
                                                    
                                                    <asp:Label ID="tblgvdelicurrencydesc" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "currency"))%>'
                                                        Width="80px"></asp:Label>
                                                    <asp:Label ID="lblgvdelicustomer" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "customer"))%>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvadeliordertype" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ordertypedesc")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Project Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvadelissignuser" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "projectName")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <%--  <asp:TemplateField HeaderText="BatchID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvdelibatchid" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchid"))%>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="DataSet <br> Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvdelidatasettype" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "datasettype")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="Work <br> Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvdelitasktitle" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wrktypedesc")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Project <br> Type ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvdelivelocitytype" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prjtype")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Assign Qty" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvdeliquantity" runat="server" Height="16px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "quantity")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                           

                                            <asp:TemplateField HeaderText="Done<br>QTY">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvdelidoneqty" runat="server" Height="16px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "doneqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                         


                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>

                                                    <asp:HyperLink runat="server" Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ordertypedesc"))=="Pilot"? false : true  %>' ID="lnkInvoice" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="text-primary pr-2 pl-2"><i class="btn btn-primary btn-sm">Invoice</i></asp:HyperLink>
                                                    <asp:LinkButton runat="server" ID="lnkbtnsow" OnClick="lnkbtnsow_Click" Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ordertypedesc"))=="Pilot"? true : false  %>' CssClass="btn btn-primary btn-sm">Convert SOW</asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <PagerStyle CssClass="gvPagination" />

                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnelAReject" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="Label41" runat="server">Page Size</asp:Label>
                                        <asp:DropDownList ID="ddlAcceptReject_pagging" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAcceptReject_pagging_SelectedIndexChanged" CssClass="form-control form-control-sm">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label ID="Label42" runat="server">Task Title</asp:Label>
                                        <asp:DropDownList ID="ddlAcceptRejecttask" runat="server" AutoPostBack="True" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 mt-3">
                                        <asp:LinkButton runat="server" ID="btnAcceptRejectsearch" OnClick="btnAcceptRejectsearch_Click" CssClass="btn btn-primary btn-sm">Search</asp:LinkButton>

                                    </div>
                                </div>
                                <div class="table-responsive mt-2">
                                    <asp:GridView ID="gv_AcceptReject" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" Visible="True" AllowPaging="true" PageSize="15" OnRowDataBound="gv_AcceptReject_RowDataBound" >
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL # ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right; font-size: 12px;"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="jobid" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvarjobid" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobid"))%>'
                                                        Width="80px"></asp:Label>
                                                     <asp:Label ID="lblacptreassignuser" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assignuser"))%>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Project ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblartaskid" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "taskid"))%>'
                                                        Width="80px"></asp:Label>
                                                    <asp:Label ID="lblgvarprjid" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prjid")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BatchID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvarbatchid" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchid"))%>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Create <br> Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvarcreatedate" runat="server" Height="16px"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "createdate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Project Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvarprojectName" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "projectName")) %>'
                                                        Width="150px"></asp:Label>
                                                    <asp:Label ID="lblgvreject" runat="server"
                                                        class='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "ordertype"))=="Pilot") ? " badge badge-pill badge-danger":" badge badge-pill badge-success" %>'
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ordertype")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Batch <br> Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvarbatchname" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchname")) %>'
                                                        Width="80px"></asp:Label>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EmpName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvarempname" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Task <br> Title">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvartasktitle" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tasktitle")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Role <br> Type ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvarroletypedesc" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roletypedesc")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Annator ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvarannoid" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "annoid")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Assign<br> Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvarvelocityqty" runat="server" Height="16px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "assignqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Job Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvarjobstatus" runat="server" CssClass="badge badge-pill badge-info"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobstatus")) %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Done<br>QTY">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvardoneqty" runat="server" Height="16px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "doneqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px"></asp:Label>
                                                    <asp:LinkButton runat="server" ID="btnarlink" OnClick="btnarlink_Click" Visible='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "doneqty")) > 0 ? true:false %>' CssClass="text-primary pr-2 pl-2" ToolTip="Assign for QC"><i class="fa fa-user-plus"></i></asp:LinkButton>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Skip<br>QTY">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvarskipqty" runat="server" Height="16px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "skipqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pending<br>QTY">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvarpendingqty" runat="server" Height="16px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pendingqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Spent <br> Hour" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvartimeinminute" runat="server" Height="16px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "timeinminute")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Pediction <br> Time" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvarpendingtime" runat="server" Height="16px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pendingqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>

                                                    <asp:HyperLink runat="server" ID="hybtnarlink" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="text-primary pr-2 pl-2" ToolTip="view"><i class="fa fa-eye"></i></asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <PagerStyle CssClass="gvPagination" />

                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="penlInvoice" runat="server" Visible="false">
                                <asp:Label runat="server" ID="lblsircode" Visible="false"></asp:Label>
                                <asp:GridView ID="gv_Invoice" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Visible="True" AllowPaging="true" PageSize="15">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL # ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right; font-size: 12px;"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                                    ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code" Visible="false">
                                            <ItemTemplate>

                                                <asp:Label ID="lblgvInsircode" runat="server" Font-Bold="True" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode")) %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblgvInprjid" runat="server" Font-Bold="True" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prjid")) %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Invoice No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvIninvno" runat="server" Width="120px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "invno")) %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Customer Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvIncustomer" runat="server" Width="250px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "customer")) %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvInprojectname" runat="server" Width="200px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "projectname")) %>'>
                                                </asp:Label>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Data Set">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvIndataset" runat="server" Width="90px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dataset")) %>'>
                                                </asp:Label>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvInsubjects" runat="server" Width="90px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subjects")) %>'>
                                                </asp:Label>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QTY">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvInqty" runat="server" Width="60px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;(#,##0); ") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvtotalrate" runat="server" Width="60px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalrate")).ToString("#,##0;(#,##0); ") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvIntotalamount" runat="server" Width="70px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalamount")).ToString("#,##0;(#,##0); ") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Information" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvInnationality" runat="server" Width="70px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nationality")) %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblgvIncity" runat="server" Width="70px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "city")) %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblgvInaddres" runat="server" Width="70px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "addres")) %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblgvInnotes" runat="server" Width="70px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "notes")) %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblgvIncurrency" runat="server" Width="70px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "currency")) %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Invoice Print">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="btninvoiceprint" OnClick="btninvoiceprint_Click"><i class="fa fa-print fa-xl"></i></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Create Date" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvIninvoicedate" runat="server" Width="100px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "invoicedate")).ToString("dd-MMM-yyyy") %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblgvInduedate" runat="server" Width="100px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "duedate")).ToString("dd-MMM-yyyy") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerStyle CssClass="gvPagination" />

                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </asp:Panel>
                            <asp:Panel ID="pnelCollection" runat="server" Visible="false">
                                <h2>Collection</h2>
                            </asp:Panel>
                        </div>
                    </div>

                    <div id="pnlSidebar" class="card pnlSidebarCl" runat="server" visible="false">
                        <div class="modal-content" id="pnlProjectadd" runat="server" visible="false">
                            <div class="modal-header pt-0 pb-0 bg-light">
                                <h6 class="modal-title">Add Project</h6>
                                <asp:LinkButton ID="pnlsidebarClose" OnClick="pnlsidebarClose_Click" CssClass="btn btn-danger  btn-sm pr-2 pl-2" runat="server">&times;</asp:LinkButton>
                            </div>
                            <div class="modal-body">
                                <asp:Label runat="server" Visible="false" ID="lblsowproject"></asp:Label>
                                <asp:Label runat="server" ID="lblproj" Visible="false"></asp:Label>

                                <asp:GridView ID="gvProjectInfo" runat="server" AutoGenerateColumns="False" CssClass="table-bordered gview"
                                    ShowFooter="False" ShowHeader="false" AllowPaging="false" Visible="True" Width="100%">

                                    <Columns>

                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                    Width="49px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <div class="row">
                                                    <div class="col-md-8">
                                                        <asp:Label ID="lgcResDesc1" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                            Width="120px" ForeColor="Black" Font-Size="12px">
                                                        </asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:LinkButton ID="btnAdd" Visible="false" runat="server" OnClick="btnAdd_Click" CssClass="float-right text-primary pr-2 pl-2"><i class="fa fa-plus-circle"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgval" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>

                                                <asp:TextBox ID="txtgvVal" runat="server"
                                                    CssClass="form-control" BackColor="Transparent"
                                                    BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdatat")) %>'>
                                                </asp:TextBox>

                                                <asp:TextBox ID="txtgvdVal" runat="server" AutoCompleteType="Disabled"
                                                    CssClass="form-control" BackColor="Transparent"
                                                    BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "gdatad")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"": Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "gdatad")).ToString("dd-MMM-yyyy") %>'>
                                                </asp:TextBox>

                                                <cc1:CalendarExtender ID="txtgvdVal_CalendarExtender" runat="server"
                                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdVal" PopupPosition="TopLeft" PopupButtonID="txtgvdVal"></cc1:CalendarExtender>


                                                <asp:DropDownList ID="ddlval" runat="server" Visible="false"
                                                    CssClass="chzn-select form-control" TabIndex="2">
                                                </asp:DropDownList>

                                                <asp:TextBox ID="lgvgdatan" runat="server" Visible="false" CssClass="form-control"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdatan")) %>'></asp:TextBox>

                                            </ItemTemplate>
                                            <HeaderStyle Width="350" />
                                            <ItemStyle Width="600" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                                <asp:LinkButton ID="btnProjectSave" runat="server" OnClick="btnProjectSave_Click" CssClass="btn btn-primary btn-sm  float-right">Project Save</asp:LinkButton>
                                <asp:LinkButton ID="btnsowConvert" runat="server" OnClick="btnsowConvert_Click" Visible="false" CssClass="btn btn-primary btn-sm  float-right">Projects Save</asp:LinkButton>

                            </div>
                        </div>

                        <div class="modal-content" id="pnlBatchadd" runat="server" visible="false">
                            <div class="modal-header bg-light p-1">
                                <h6 class="modal-title">Batch Add</h6>
                                <asp:LinkButton ID="LinkButton1" OnClick="pnlsidebarClose_Click" CssClass="btn btn-danger  btn-sm pr-2 pl-2" runat="server">&times;</asp:LinkButton>


                            </div>
                            <div class="modal-body">
                                <div class="well">
                                    <div class="row">
                                        <div class="col-lg-4 col-md-4 col-sm-6">
                                            <asp:HiddenField ID="hiidenBatcid" runat="server" Value="0" />
                                            <asp:HiddenField ID="hiddPrjid" runat="server" />
                                            <div class="form-group">
                                                <asp:Label ID="Label6" runat="server">Project Name</asp:Label>
                                                <asp:TextBox ID="txtproj" runat="server" Enabled="false" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                <asp:Label ID="tblpactcode" runat="server" Visible="false" Text='<%#Eval("pactcode").ToString()%>'></asp:Label>
                                                <%--<asp:Label ID="" runat="server"  Text='<%#Eval("prjid").ToString()%>'></asp:Label>--%>
                                            </div>
                                        </div>

                                        <div class="col-lg-4 col-md-4 col-sm-6">
                                            <div class="form-group">
                                                <asp:Label ID="Label10" runat="server">DataSet Type</asp:Label>
                                                <asp:TextBox ID="txtdataset" runat="server" CssClass="form-control" Enabled="false" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-6">
                                            <div class="form-group">
                                                <asp:Label ID="Label11" runat="server">Work Type</asp:Label>
                                                <asp:TextBox ID="txtworktype" runat="server" CssClass="form-control" Enabled="false" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4 col-md-4 col-sm-6">
                                            <div class="form-group">
                                                <asp:Label ID="Label7" runat="server">Batch Name 
                                                <span>
                                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="vldtxtBatch" runat="server" ForeColor="Red" ControlToValidate="txtBatch" ValidationGroup="NewBatchAdd"
                                                        ErrorMessage="*" /></span>
                                                </asp:Label>
                                                <asp:TextBox ID="txtBatch" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-lg-3 col-md-6 col-sm-6">
                                            <div class="form-group row">
                                                <asp:Label ID="Label9" runat="server">Total Hour</asp:Label>
                                                <asp:TextBox ID="tbltotalOur" runat="server" CssClass="form-control"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="col-lg-2 col-md-3 col-sm-6">

                                            <div class="form-group">
                                                <asp:Label ID="Label12" runat="server">Time Type</asp:Label>

                                                <asp:DropDownList ID="ddlphdm" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="0">Hour</asp:ListItem>
                                                    <asp:ListItem Value="1">Minute</asp:ListItem>
                                                    <asp:ListItem Value="2">Day</asp:ListItem>
                                                    <asp:ListItem Value="3">Mounth</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-6">
                                            <div class="form-group">
                                                <asp:Label ID="Label13" runat="server">Start Date</asp:Label>
                                                <asp:TextBox ID="txtstartdate" runat="server" CssClass="form-control"></asp:TextBox>
                                                <cc1:CalendarExtender runat="server" Format="dd-MMM-yyyy" TargetControlID="txtstartdate"></cc1:CalendarExtender>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-lg-3 col-md-3 col-sm-6">
                                            <div class="form-group">
                                                <asp:Label ID="Label8" runat="server">Order Quantity
                                                <span>
                                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="vldtxtbatchQuantity" runat="server" ForeColor="Red" ControlToValidate="txtbatchQuantity"
                                                        ValidationGroup="NewBatchAdd"
                                                        ErrorMessage="*" /></span>
                                                </asp:Label>
                                                <asp:TextBox ID="txtbatchQuantity" runat="server" placeholder="0" AutoPostBack="true" OnTextChanged="calculateAmount_TextChanged" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-lg-3 col-md-3 col-sm-6">
                                            <div class="form-group">
                                                <asp:Label ID="Label18" runat="server">Order Rate</asp:Label>
                                                <asp:TextBox ID="txtrate" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="calculateAmount_TextChanged" placeholder="0.00 $"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-6">
                                            <div class="form-group">
                                                <asp:Label ID="Label19" runat="server">Order Amount <span id="spnCurrncy" class="text-danger" runat="server"></span></asp:Label>
                                                <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="calculateAmount_TextChanged" placeholder="0.00 $"></asp:TextBox>

                                            </div>
                                        </div>


                                        <div class="col-lg-3 col-md-3 col-sm-6">
                                            <div class="form-group">
                                                <asp:Label ID="Label14" runat="server">Delivery Date</asp:Label>
                                                <asp:TextBox ID="textdelevery" runat="server" CssClass="form-control"></asp:TextBox>
                                                <cc1:CalendarExtender runat="server" Format="dd-MMM-yyyy" TargetControlID="textdelevery"></cc1:CalendarExtender>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card well p-0">
                                    <div class="card-header pt-1 pb-1">
                                        <h5 class="text-center">Project Planing Analysis</h5>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-lg-3 col-md-3 col-sm-6">
                                                <div class="form-group">
                                                    <asp:Label ID="Label15" runat="server">Work Per-Hour</asp:Label>
                                                    <asp:TextBox ID="txtPerhour" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-6">
                                                <div class="form-group">
                                                    <asp:Label ID="Label16" runat="server">Employee Capacity</asp:Label>
                                                    <asp:TextBox ID="textEmpcap" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-5 col-md-3 col-sm-6">
                                                <div class="form-group">
                                                    <asp:Label ID="Label17" runat="server">Estimated  ManPower</asp:Label>
                                                    <asp:TextBox ID="TextmanPower" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-1 col-md-1 col-sm-12">
                                                <asp:LinkButton runat="server" ID="tblSaveBatch" OnClick="tblSaveBatch_Click" CssClass="btn mt-3 btn-primary btn-sm"
                                                    ValidationGroup="NewBatchAdd" CausesValidation="true">Save</asp:LinkButton>

                                                <asp:LinkButton runat="server" ID="btnbatchUpdate" OnClick="btnbatchUpdate_Click1" Visible="false" CssClass="btn mt-3 btn-primary btn-sm"
                                                    ValidationGroup="NewBatchAdd" CausesValidation="true">Update</asp:LinkButton>
                                            </div>
                                        </div>

                                    </div>

                                </div>

                                <hr />
                                <div class="row">
                                    <asp:GridView ID="gv_gridBatch" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" Visible="True" AllowPaging="true" PageSize="15" Width="100%">
                                        <Columns>

                                            <asp:TemplateField HeaderText="SL # ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right; font-size: 12px;"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                                        ForeColor="Black"></asp:Label>

                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Project Name" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBatchid" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "id")) %>'></asp:Label>
                                                    <asp:Label ID="lblhourtype" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phdm")) %>'></asp:Label>

                                                    <asp:Label ID="lblinfdesc" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "projname")) %>'
                                                        Width="250px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Batch Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbatchname" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchid")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Start Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstartdate" runat="server" Height="16px"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "startdate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delivery Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldeliverydate" runat="server" Height="16px"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "deliverydate")).ToString("dd-MMM-yyyy") %>' Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dataset QTY">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldatasetqty" runat="server" Height="16px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "datasetqty")) %>' Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Duration (H:M)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldatastotalhour" runat="server" Height="16px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalhour")) %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldatasetRate" runat="server" Height="16px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")) %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldatasetAmount" runat="server" Height="16px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")) %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <span class="badge badge-pill badge-info">New</span>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnbatchEdit" runat="server" CssClass="text-primary" OnClick="btnbatchEdit_Click" ToolTip="edit"><i class="fa fa-edit"></i></asp:LinkButton>

                                                    <asp:LinkButton runat="server" ID="btnDelete" CommandName="Delete"
                                                        ClientIDMode="Static"
                                                        OnClick="btnbatchremoveRow_Click" ToolTip="Delete"
                                                        CssClass="text-danger pr-2 isdeleteRow" CausesValidation="false"
                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>'
                                                        OnClientClick="return sweetAlertConfirm(this);"><i class="fa fa-trash"></i></asp:LinkButton>


                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Middle" />

                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="gvPagination" />

                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="modal-footer">
                            </div>
                        </div>

                        <div class="modal-content" runat="server" id="pnlAssginUser" visible="false">
                            <asp:HiddenField ID="HiddinTaskid" runat="server" Value="0" />

                            <asp:Label runat="server" Visible="false" ID="lblabatchid"></asp:Label>
                            <asp:Label runat="server" Visible="false" ID="lblproprjid"></asp:Label>
                            <asp:Label runat="server" ID="lblDoneAnnot" Visible="false"></asp:Label>
                            <asp:Label runat="server" ID="lblDoneQC" Visible="false"></asp:Label>
                            <asp:Label runat="server" ID="lblDoneQA" Visible="false"></asp:Label>
                            <div class="modal-header bg-light p-1">
                                <h6 class="modal-title">Assign User</h6>
                                <asp:LinkButton ID="LinkButton2" OnClick="pnlsidebarClose_Click" CssClass="btn btn-danger  btn-sm pr-2 pl-2" runat="server">&times;</asp:LinkButton>

                            </div>
                            <div class="modal-body well">
                                <div class="form-group row">

                                    <div class="d-flex w-100" style="padding: 10px 8px 4px 0px;">
                                        <asp:Label ID="lbltaskname" runat="server" CssClass="float-left">Task Name</asp:Label>
                                        <asp:Label ID="Label20" runat="server" CssClass="float-left">Task Name &nbsp;</asp:Label>
                                        <span class="text-danger">&nbsp;PendingAnnotator(
                                            <asp:Label runat="server" ID="lblcountannotid"></asp:Label>)</span>
                                        <span class="text-danger">&nbsp; &nbsp;PendingQC(
                                            <asp:Label runat="server" ID="lblcountQC"></asp:Label>)</span>
                                        <span class="text-danger">&nbsp; &nbsp; PendingQA(
                                            <asp:Label runat="server" ID="lblcountQA"></asp:Label>)</span>

                                    </div>

                                    <asp:TextBox ID="txttasktitle" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>


                                <div class="form-group row d-none">
                                    <asp:Label ID="Label21" runat="server">Task Description</asp:Label>
                                    <asp:TextBox ID="txtdesc" runat="server" CssClass="form-control"></asp:TextBox>


                                </div>
                                <div class="form-group row d-none">
                                    <asp:Label ID="Label22" runat="server">Remakrs</asp:Label>
                                    <asp:TextBox ID="txtremaks" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-4 col-md-3 col-sm-12 pl-0">
                                        <asp:Label ID="Label23" runat="server">Assigne Team Members</asp:Label>
                                        <asp:DropDownList ID="ddlassignmember" runat="server" CssClass="form-control chzn-select" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-12">
                                        <asp:Label ID="Label24" runat="server">Role Type</asp:Label>
                                        <asp:DropDownList ID="ddlUserRoleType" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlUserRoleType_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-lg-3 col-md-4 col-sm-12">
                                        <asp:Label ID="Label25" runat="server">Annotation ID</asp:Label>
                                        <asp:DropDownList ID="ddlAnnotationid" runat="server" CssClass="form-control chzn-select" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-2 col-md-3 col-sm-12 pl-0">
                                        <asp:Label ID="Label26" runat="server">Assigned Type</asp:Label>
                                        <asp:DropDownList ID="ddlassigntype" runat="server" CssClass="form-control chzn-select" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>

                                </div>
                                <div class="form-group row">


                                    <div class="col-lg-3 col-md-3 col-sm-12">
                                        <asp:Label ID="Label27" runat="server"> Assigned QYT</asp:Label>
                                        <asp:TextBox ID="txtquantity" min="0" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class=" col-lg-3 col-md-3 col-sm-12">
                                        <asp:Label ID="Label28" runat="server">Work Hour</asp:Label>
                                        <asp:TextBox ID="txtworkhour" runat="server" min="0" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class=" col-lg-2 col-md-2 col-sm-12 mt-4">
                                        <asp:CheckBox runat="server" ID="checkinoutsourcing" OnCheckedChanged="checkinoutsourcing_CheckedChanged" AutoPostBack="True" ForeColor="red"></asp:CheckBox>

                                        <asp:Label ID="Label30" runat="server">&nbsp;  Freelancing</asp:Label>
                                    </div>
                                    <div class=" col-lg-3 col-md-3 col-sm-12" id="perrate" runat="server" visible="false">
                                        <asp:Label ID="Label29" runat="server">Per Rate</asp:Label>
                                        <asp:TextBox ID="textrate" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class=" col-lg-1 col-md-1 col-sm-12 mt-4 ">
                                        <asp:LinkButton ID="btnaddrow" runat="server" OnClick="btnaddrow_Click" CssClass=" btn btn-primary ml-auto btn-sm mt20 mr-1 float-left"><i class="fa fa-plus"></i></asp:LinkButton>

                                    </div>
                                </div>

                                <div class="form-group">

                                    <asp:GridView ID="GridVirtual" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" Width="">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL # ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right; font-size: 12px;"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Member" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbljobid" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobid")) %>'></asp:Label>

                                                    <asp:Label ID="lblempid" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>' ForeColor="Black" Font-Size="12px"></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Member">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmember" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>' Width="170px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Role Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="tblroleType" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roledesc")) %>' Width="50px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Annotation <br> ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="tblAnnotation" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "annoid")) %>' ForeColor="Black" Font-Size="12px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Assign  <br> Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="tbltype" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assigndesc")) %>' Width="80px" ForeColor="Black" Font-Size="12px"></asp:Label>

                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="fgvsup" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right">Total :</asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Assign <br> QTY">
                                                <ItemTemplate>
                                                    <asp:Label ID="tblValoquantity" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assignqty")) %>' Width="50px" ForeColor="Black" Font-Size="12px"></asp:Label>

                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="tblsumValoquantity" runat="server" Width="100px" ForeColor="Black" Font-Size="12px" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Work <br> Hour">
                                                <ItemTemplate>
                                                    <asp:Label ID="tblworkhour" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "workhour")) %>' Width="50px" ForeColor="Black" Font-Size="12px"></asp:Label>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Work <br> Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="tblworkrate" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "workrate")) %>' Width="50px" ForeColor="Black" Font-Size="12px"></asp:Label>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="btnvrdelete" OnClick="btnvrdelete_Click" OnClientClick="return confirm('Are You Sure?')" CssClass="text-danger"><i class="fa fa-trash"></i></asp:LinkButton>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <%--<FooterStyle CssClass="grvFooter" />--%>
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                                <asp:LinkButton runat="server" ID="btntaskSave" OnClick="btntaskSave_Click" CssClass="btn btn-primary btn-sm  float-right">Task Save</asp:LinkButton>


                            </div>
                        </div>
                    </div>

                    <%-- Add Customer Modal --%>
                    <div id="btnAdd" class="modal " role="dialog" data-keyboard="false" data-backdrop="static">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header bg-light p-1">
                                    <h6 class="modal-title">Customer Create</h6>
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                </div>
                                <div class="modal-body">
                                    <div class="row well">
                                        <div class="col-md-6 ">
                                            <div class="form-group">
                                                <asp:Label ID="lblel1" runat="server">Customer Name</asp:Label>
                                                <asp:TextBox runat="server" ID="txtcustomername" CssClass="form-control form-control-sm"></asp:TextBox>
                                            </div>

                                        </div>

                                    </div>
                                    <div class="modal-footer text-center d-block">
                                        <asp:LinkButton runat="server" ID="btncustAdd" OnClick="btncustAdd_Click" OnClientClick="closecustomeradd()" CssClass="btn btn-primary btn-sm">Save</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                </div>

            </div>


        </ContentTemplate>

    </asp:UpdatePanel>

    <script>
        function sweetAlertConfirm(btnDelete) {

            if (btnDelete.dataset.confirmed) {
                btnDelete.dataset.confirmed = false;
                return true;
            } else {
                // Ask the user to confirm/cancel the action
                event.preventDefault();
                swal({
                    title: 'Are you sure?',
                    text: "You won't be able to revert this !!",
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Ok'
                })
                    .then(function () {
                        // Set data-confirmed attribute to indicate that the action was confirmed
                        btnDelete.dataset.confirmed = true;
                        // Trigger button click programmatically
                        btnDelete.click();
                    }).catch(function (reason) {
                        // The action was canceled by the user
                        return false
                    });
            }
        }
    </script>


</asp:Content>
