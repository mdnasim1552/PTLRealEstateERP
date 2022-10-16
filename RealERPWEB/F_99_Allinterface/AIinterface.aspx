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
            height: 100vh;
            background: #edeae9;
            position: absolute;
            right: 0;
        }

            .pnlSidebarCl .form-control {
                height: 25px;
                line-height: 25px;
                padding: 2px;
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

        function checkEmptyNote() {
            OpenAddCustomer();
            OpenAddProject();
            OpenAddTask();
            OpenAddBatch();
            closecustomeradd();
        }
        function OpenAddCustomer() {

            $('#AddCustomerModal').modal('toggle');
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
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
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
                                    <asp:LinkButton ID="lnkbtnok" runat="server" CssClass=" btn btn-primary btn-sm mt20">Ok</asp:LinkButton></li>
                                </div>
                            </div>
                            <div class="col-md-2">

                                <div class=" btn-group" role="group" aria-label="Button group with nested dropdown">
                                    <div class="btn-group" role="group">
                                        <button id="btnGroupDrop4" type="button" class="btn btn-success ml-auto bw-100 btn-sm mt20 mr-2 dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Operation</button>
                                        <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                            <div class="dropdown-arrow"></div>

                                            <asp:LinkButton ID="btnaddPrj" OnClick="btnaddPrj_Click" runat="server">Add Project</asp:LinkButton>
                                            <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl="~/F_38_AI/CustomerList" CssClass="dropdown-item" Style="padding: 0 10px">Customer List</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" NavigateUrl="~/F_38_AI/AddProject" CssClass="dropdown-item" Style="padding: 0 10px">All Project's</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" NavigateUrl="~/F_38_AI/CreateTask" CssClass="dropdown-item" Style="padding: 0 10px">Create Task</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink4" runat="server" Target="_blank" NavigateUrl="~/StepofOperationNew?moduleid=14" CssClass="dropdown-item" Style="padding: 0 10px">Assign Team Leader</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink5" runat="server" Target="_blank" NavigateUrl="~/StepofOperationNew?moduleid=14" CssClass="dropdown-item" Style="padding: 0 10px">Creating Invoice</asp:HyperLink>
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
                                            <asp:ListItem Value="2"><div class="circle-tile"><a><div class="circle-tile-heading purple counter">0</div></a><div class="circle-tile-content purple"><div class="circle-tile-description txt-white">Assign</div></div></div></asp:ListItem>
                                            <asp:ListItem Value="3"><div class="circle-tile"><a><div class="circle-tile-heading  bg-success counter">0</div></a><div class="circle-tile-content bg-success"><div class="circle-tile-description txt-white">Production</div></div></div></asp:ListItem>
                                            <asp:ListItem Value="4"><div class="circle-tile"><a><div class="circle-tile-heading  deep-pink counter">0</div></a><div class="circle-tile-content  deep-pink"><div class="circle-tile-description txt-white">QC & QA</div></div></div></asp:ListItem>
                                            <asp:ListItem Value="5"><div class="circle-tile"><a><div class="circle-tile-heading  orange counter">0</div></a><div class="circle-tile-content  orange"><div class="circle-tile-description txt-white">Accept/Reject</div></div></div></asp:ListItem>
                                            <asp:ListItem Value="6"><div class="circle-tile"><a><div class="circle-tile-heading  bg-success counter">0</div></a><div class="circle-tile-content  bg-info"><div class="circle-tile-description txt-white">QA</div></div></div></asp:ListItem>
                                            <asp:ListItem Value="7"><div class="circle-tile"><a><div class="circle-tile-heading  bg-pink counter">0</div></a><div class="circle-tile-content  bg-pink"><div class="circle-tile-description txt-white">FeedBack</div></div></div></asp:ListItem>
                                            <asp:ListItem Value="8"><div class="circle-tile"><a><div class="circle-tile-heading  orange counter">0</div></a><div class="circle-tile-content  orange"><div class="circle-tile-description txt-white">Delivery</div></div></div></asp:ListItem>
                                            <asp:ListItem Value="9"><div class="circle-tile"><a><div class="circle-tile-heading bg-dark bg-gradient counter">0</div></a><div class="circle-tile-content bg-dark bg-gradient"><div class="circle-tile-description txt-white">Invoice</small></div></div></div></asp:ListItem>
                                            <asp:ListItem Value="10"><div class="circle-tile"><a><div class="circle-tile-heading  bg-info text-white counter">0</div></a><div class="circle-tile-content bg-info"><div class="circle-tile-description txt-white text-white">Collection</div></div></div></asp:ListItem>
                                            <%-- <asp:ListItem Value="7"><div class="circle-tile"><a><div class="circle-tile-heading  bg-danger text-white counter">0</div></a><div class="circle-tile-content bg-danger"><div class="circle-tile-description txt-white text-white">Loan Cancelled</div></div></div></asp:ListItem>--%>
                                        </asp:RadioButtonList>


                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <hr />
                        <asp:Panel ID="pnlAllProject" runat="server" Visible="false">
                            <div class="table-responsive">
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
                                                <asp:Label ID="lblordertype" runat="server" Height="16px"
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "ordertype"))%>'
                                                    ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ProjectName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblprojectName" runat="server" Height="16px"
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

                                <asp:GridView ID="gv_BatchList" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Visible="True" AllowPaging="true" PageSize="15" Width="100%" OnRowDataBound="gv_BatchList_RowDataBound">
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
                                                <asp:Label ID="lblid" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "id")) %>'></asp:Label>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Projectcode" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblprjid" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prjid")) %>'></asp:Label>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ProjectName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblinfdesc" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "projname")) %>'
                                                    Width="200px"></asp:Label>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Batch Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbatchid" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchid")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Data <br> Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldatasettype" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "datasettype")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Work <br> Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblworktype" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "worktype")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Data Set <br>  QTY">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldatasetqty" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "datasetqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total <br> Hour">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltotalhour" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalhour")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Start <br> Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblstartdate" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "startdate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delivery <br> Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldeliverydate" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "deliverydate")).ToString("dd-MMM-yyyy") %>' Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Man <br>Power">
                                            <ItemTemplate>
                                                <asp:Label ID="lblestimatemanpower" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "estimatemanpower")).ToString("#,##0;(#,##0); ") %>' Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="btnview" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="text-primary pr-2 pl-2" ToolTip="view"><i class="fa fa-eye"></i></asp:HyperLink>

                                                <asp:HyperLink ID="btnupdate" runat="server" CssClass="text-primary" ToolTip="edit"><i class="fa fa-edit"></i></asp:HyperLink>
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
                            <div class="table-responsive">
                                <asp:GridView ID="gvAssingJob" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Visible="True" AllowPaging="true" PageSize="15" OnRowDataBound="gvAssingJob_RowDataBound">

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
                                                <asp:Label ID="lblpactcode" runat="server" Text='<%#Eval("pactcode").ToString()%>' Width="10px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EmpID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblempid" runat="server" Text='<%#Eval("empid").ToString()%>' Width="10px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblordertype" runat="server" Height="16px"
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "ordertype"))%>'
                                                    ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Task Title">
                                            <ItemTemplate>
                                                <asp:Label ID="lblprojectName" runat="server" Height="16px"
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "tasktitle"))%>'
                                                    ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Batch Name">
                                            <ItemTemplate>
                                                <asp:Label ID="tblproj" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchname")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="DataSet Type">
                                            <ItemTemplate>
                                                <asp:Label ID="tbldataset" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "datasettype")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="WorkType">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvworktype" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "worktype")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvempname" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Create date">
                                            <ItemTemplate>
                                                <asp:Label ID="tblcreatedate" runat="server" Width="80px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "startdate")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"": Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "startdate")).ToString("dd-MMM-yyyy")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Assigned QTY">
                                            <ItemTemplate>
                                                <asp:Label ID="tbladdress" runat="server" Width="80px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "assignqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Assign Per Hour">
                                            <ItemTemplate>
                                                <asp:Label ID="tblcountry" runat="server" Width="80px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "empworkhour")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
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
                            <div class="table-responsive">
                                <asp:GridView ID="gv_Production" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Visible="True" AllowPaging="true" PageSize="15" OnRowDataBound="gv_Production_RowDataBound">
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
                                                <asp:Label ID="lblgvjobid" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobid"))%>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EmpID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvassignuser" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assignuser")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BatchID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvbatchid" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchid"))%>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Create <br> Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcreatedate" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "createdate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Role Type " Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvvelocitytype" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "velocitytype")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvprojectName" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "projectName")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Batch <br> Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvbatchname" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchname")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EmpName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvempname" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Task <br> Title">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvtasktitle" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tasktitle")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Annator ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvannoid" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "annoid")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Assigned<br> Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvvelocityqty" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "assignqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Job Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvjobstatus" runat="server" Height="16px" CssClass="badge badge-pill badge-info"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobstatus")) %>'
                                                    hieght="20px" Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Done<br>QTY">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdoneqty" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "doneqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Skip<br>QTY">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvskipqty" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "skipqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Spent <br> Hour">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvtimeinminute" runat="server" Height="16px"
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
                                </asp:GridView>
                            </div>

                        </asp:Panel>
                        <asp:Panel ID="pnelQC" runat="server" Visible="false">
                            <div class="table-responsive">
                                <asp:GridView ID="gv_QCQA" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Visible="True" AllowPaging="true" PageSize="15" OnRowDataBound="gv_QCQA_RowDataBound">
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
                                                <asp:Label ID="lblgvqcjobid" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobid"))%>'
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
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "velocitytype")) %>'
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
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Annator ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvqcannoid" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "annoid")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Assign<br> Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvqcvelocityqty" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "assignqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Job Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvqcjobstatus" runat="server" Height="16px" CssClass="badge badge-pill badge-info"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobstatus")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Done<br>QTY">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvqcdoneqty" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "doneqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Skip<br>QTY">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvqcskipqty" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "skipqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Spent <br> Hour">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvqctimeinminute" runat="server" Height="16px"
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
                                                <asp:HyperLink runat="server" ID="hybtnqclink" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="text-primary pr-2 pl-2" ToolTip="view"><i class="fa fa-eye"></i></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnelQA" runat="server" Visible="false">
                            <h2>QC</h2>
                        </asp:Panel>
                        <asp:Panel ID="pnelFeedBack" runat="server" Visible="false">
                            <h2>Feedback</h2>
                        </asp:Panel>
                        <asp:Panel ID="Pneldelivery" runat="server" Visible="false">
                            <h2>delevery</h2>
                        </asp:Panel>
                        <asp:Panel ID="pnelAReject" runat="server" Visible="false">
                            <div class="table-responsive">
                                <asp:GridView ID="gv_AcceptReject" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Visible="True" AllowPaging="true" PageSize="15">
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
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project ID" Visible="false">
                                            <ItemTemplate>
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
                                        <asp:TemplateField HeaderText="Annator ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvarannoid" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "annoid")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Assign<br> Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvarvelocityqty" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "assignqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Job Status" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvarjobstatus" runat="server" Height="16px" CssClass="badge badge-pill badge-info"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobstatus")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Done<br>QTY">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvardoneqty" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "doneqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Skip<br>QTY">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvarskipqty" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "skipqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
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
                                                <asp:Label ID="lblgvarpendingqty" runat="server" Height="16px"
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
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="penlInvoice" runat="server" Visible="false">
                            <h2>Invoice</h2>
                        </asp:Panel>
                        <asp:Panel ID="pnelCollection" runat="server" Visible="false">
                            <h2>Collection</h2>
                        </asp:Panel>






                    </div>



                    <div id="pnlSidebar" class="card pnlSidebarCl" runat="server" visible="false">
                        <div class="modal-content" id="pnlProjectadd" runat="server" visible="false">
                            <div class="modal-header pt-0 pb-0 bg-light">
                                <h6 class="modal-title">Add Project</h6>
                                <asp:LinkButton ID="pnlsidebarClose" OnClick="pnlsidebarClose_Click" CssClass="btn btn-danger  btn-sm pr-2 pl-2" runat="server">&times;</asp:LinkButton>
                            </div>
                            <div class="modal-body">
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
                                                <asp:Label ID="lgcResDesc1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="120px" ForeColor="Black" Font-Size="12px">
                                                </asp:Label>
                                                <asp:LinkButton ID="btnAdd" Visible="false" runat="server" OnClick="btnAdd_Click" CssClass="text-primary pr-2 pl-2"><i class="fa fa-plus-circle"></i></asp:LinkButton>

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
                                            <HeaderStyle Width="250" />
                                            <ItemStyle Width="250" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                                <asp:LinkButton ID="btnProjectSave" runat="server" OnClick="btnProjectSave_Click" CssClass="btn btn-primary btn-sm  float-right">Project Save</asp:LinkButton>

                            </div>
                        </div>

                        <div class="modal-content" id="pnlBatchadd" runat="server" visible="false">
                            <div class="modal-header bg-light">
                                <h6 class="modal-title">Batch Add</h6>
                                <asp:LinkButton ID="LinkButton1" OnClick="pnlsidebarClose_Click" CssClass="btn btn-danger  btn-sm pr-2 pl-2" runat="server">&times;</asp:LinkButton>


                            </div>
                            <div class="modal-body">
                                <div class="row bg-light">
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
                                            <asp:TextBox ID="txtrate" runat="server" CssClass="form-control"  AutoPostBack="true" OnTextChanged="calculateAmount_TextChanged" placeholder="0.00 $"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-6">
                                        <div class="form-group">
                                            <asp:Label ID="Label19" runat="server">Order Amount <span id="spnCurrncy"  class="text-danger" runat="server"></span></asp:Label>
                                            <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control"  AutoPostBack="true" OnTextChanged="calculateAmount_TextChanged" placeholder="0.00 $"></asp:TextBox>

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
                                                    ValidationGroup="NewBatchAdd"  causesvalidation="true">Save</asp:LinkButton>
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
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "id")) %>'
                                                        ></asp:Label>
                                                    <asp:Label ID="lblhourtype" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phdm")) %>'
                                                        ></asp:Label>

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
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")) %>' Width="80px"></asp:Label>
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
