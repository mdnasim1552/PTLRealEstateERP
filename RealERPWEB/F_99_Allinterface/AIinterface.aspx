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
        }

        .tbMenuWrp table tr td {
            /*height: 50px;*/
            width: 130px;
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
            margin-top: 5px;
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

        .btn-group a {
            margin: 5px;
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
                                            <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl="~/F_38_AI/CustomerList" CssClass="dropdown-item" Style="padding: 0 10px">Customer List</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" NavigateUrl="~/F_38_AI/AddProject" CssClass="dropdown-item" Style="padding: 0 10px">New Project</asp:HyperLink>
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
                                        <asp:ListItem Value="6"><div class="circle-tile"><a><div class="circle-tile-heading bg-dark bg-gradient counter">0</div></a><div class="circle-tile-content bg-dark bg-gradient"><div class="circle-tile-description txt-white">Invoice</small></div></div></div></asp:ListItem>
                                        <asp:ListItem Value="7"><div class="circle-tile"><a><div class="circle-tile-heading  bg-info text-white counter">0</div></a><div class="circle-tile-content bg-info"><div class="circle-tile-description txt-white text-white">Collection</div></div></div></asp:ListItem>
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
                                    ShowFooter="True" Visible="True" AllowPaging="true" PageSize="15" OnPageIndexChanging="gvInterface_PageIndexChanging">

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
                                        <asp:TemplateField HeaderText="Customer Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblinfdesc" runat="server" Height="16px"
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))%>'
                                                    ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblinfdesc" runat="server" Height="16px"
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "ordertype"))%>'
                                                    ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ProjectName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblinfdesc" runat="server" Height="16px"
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
                                                <asp:Label ID="tbldataset" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dataset")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="WorkType">
                                            <ItemTemplate>
                                                <asp:Label ID="tblwrktype" runat="server"
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
                                            
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkView" runat="server" Target="_blank" NavigateUrl="~/F_38_AI/JobAnalytics"  CssClass="text-primary pr-2 pl-2" ToolTip="view"><i class="fa fa-eye"></i></asp:HyperLink>
                                                <asp:LinkButton ID="btnRemove" runat="server" CssClass="text-danger pr-2" ToolTip="delete"><i class="fa fa-trash"></i></asp:LinkButton>
                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="text-primary" ToolTip="edit"><i class="fa fa-edit"></i></asp:LinkButton>
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
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "id")) %>'
                                                          ></asp:Label>
                                                        
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Projectcode" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblprjid" runat="server" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prjid")) %>'
                                                          ></asp:Label>
                                                        
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
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchid")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Data <br> Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldatasettype" runat="server" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "datasettype")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Work <br> Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblworktype" runat="server" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "worktype")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Data <br> Set">
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
                                                        <asp:HyperLink ID="btnview" runat="server" Target="_blank"  ForeColor="Black" Font-Underline="false"  CssClass="text-primary pr-2 pl-2" ToolTip="view"><i class="fa fa-eye"></i></asp:HyperLink>
                                                      <asp:HyperLink ID="btndelete" runat="server" CssClass="text-danger pr-2" ToolTip="delete"><i class="fa fa-trash"></i></asp:HyperLink>
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
                                        <asp:TemplateField HeaderText="ProjectName" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpactcode" runat="server" Text='<%#Eval("pactcode").ToString()%>' Width="10px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         
                                        <asp:TemplateField HeaderText="Order Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblinfdesc" runat="server" Height="16px"
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "ordertype"))%>'
                                                    ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ProjectName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblinfdesc" runat="server" Height="16px"
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "projectName"))%>'
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
                                                <asp:Label ID="tblwrktype" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "worktype")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Employee Name">
                                            <ItemTemplate>
                                                <asp:HyperLink runat="server" ID="btnMytask" Target="_blank" NavigateUrl="~/F_38_AI/MyTasks?Empid="  >
                                                <asp:Label ID="tblwrktype" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'></asp:Label></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Create date">
                                            <ItemTemplate>
                                                <asp:Label ID="tblcreatedate" runat="server" Width="80px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "startdate")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"": Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "startdate")).ToString("dd-MMM-yyyy")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Velocity QTY">
                                            <ItemTemplate>
                                                <asp:Label ID="tbladdress" runat="server" Width="80px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "velocityqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Velocity Per Hour">
                                            <ItemTemplate>
                                                <asp:Label ID="tblcountry" runat="server" Width="80px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "empworkhour")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                     
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                              <asp:HyperLink ID="lnkView" runat="server" Target="_blank"  NavigateUrl="~/F_38_AI/MyTasks?Empid=930100101005"   CssClass="text-primary pr-2 pl-2" ToolTip="view"><i class="fa fa-eye"></i></asp:HyperLink>
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
                            <h2>Production</h2>
                        </asp:Panel>
                        <asp:Panel ID="pnelQC" runat="server" Visible="false">
                            <h2>QC</h2>
                        </asp:Panel>
                        <asp:Panel ID="pnelAReject" runat="server" Visible="false">
                            <h2>Acept/ Reject</h2>
                        </asp:Panel>
                        <asp:Panel ID="penlInvoice" runat="server" Visible="false">
                            <h2>Invoice</h2>
                        </asp:Panel>
                        <asp:Panel ID="pnelCollection" runat="server" Visible="false">
                            <h2>Collection</h2>
                        </asp:Panel>

                    </div>

                </div>

            </div>


        </ContentTemplate>

    </asp:UpdatePanel>



</asp:Content>
