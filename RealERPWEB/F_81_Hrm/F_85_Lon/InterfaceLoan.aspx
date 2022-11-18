<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="InterfaceLoan.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_85_Lon.InterfaceLoan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

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
        }

        .tbMenuWrp table tr td {
            /*height: 50px;*/
            width: 150px;
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
        }


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

        function OpenApplyLoan() {

            $('#ApplyLoan').modal('toggle');
        }

        function ViewLoan() {
            $('#ApplyLoan').modal('toggle');
        }

        function EditLoan() {
            $('#ApplyLoan').modal('toggle');
        }

        function ModalLoanClose() {
            $('#ApplyLoan').appendTo("body").modal('hide');

            $('.modal').remove();


            $('.modal-backdrop show').remove();
            $('body').removeClass("modal-open");
            $('.modal-backdrop').remove()
            $(document.body).removeClass("modal-open");

        }
        function OpenDeleteModal() {
            $('#deleteModal').modal('toggle');
        }

        function PrintRpt() {
            window.open('<%= ResolveUrl("../../RDLCViewerWin.aspx?PrintOpt=PDF") %>', '_blank');
      }
    </script>

    <%--    <script>
        function ResetInput() {
            const inputs = document.querySelectorAll('<%=txtLoanAmt.ClientID%>, <%=txtInstNum.ClientID%>', <%=txtStd.ClientID%>, <%=txtOI.ClientID%>, <%=txtrt.ClientID%>,<%=txtOD.ClientID%>,<%=txtEffDate.ClientID%>,<%=txtLoanDescc.ClientID%>');

        inputs.forEach(input => {
                input.value = '';
            });
        }
    </script>--%>


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
                            <div class="col-lg-2">
                                <asp:Label ID="Label1" runat="server">From</asp:Label>
                                <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                            </div>
                            <div class="col-lg-2">
                                <asp:Label ID="Label2" runat="server">To</asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>
                            <div class="col-lg-3" style="display: none;">
                                <asp:Label ID="Label19" runat="server">Loan Type</asp:Label>
                                <asp:DropDownList ID="ddlLoanTypeSearch" runat="server" CssClass="form-control form-control-sm">
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-2">
                                <asp:Label ID="Label3" runat="server">ID Card #</asp:Label>
                                <div class="input-group input-group-alt input-group-sm">
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    <div class="input-group-prepend ">
                                        <asp:LinkButton ID="lnkbtfind" runat="server" CssClass=" btn btn-light btn-sm "><i class="fa fa-search"></i></asp:LinkButton></li>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-1">
                                <div class="form-group-">
                                    <asp:LinkButton ID="lnkbtnok" runat="server" CssClass=" btn btn-primary btn-sm mt20" OnClick="lbtnOk_Click">Ok</asp:LinkButton></li>
                                </div>
                            </div>
                            <asp:LinkButton ID="lnkApplyModal" runat="server" CssClass="btn btn-primary ml-auto bw-100 btn-sm mt20 mr-2" OnClick="lnkApplyModal_Click"><i class="fa fa-plus"></i> Apply Loan</asp:LinkButton>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="panel with-nav-tabs panel-primary">
                            <fieldset class="tabMenu">
                                <div class="form-horizontal">
                                    <div class="tbMenuWrp nav nav-tabs rptPurInt">

                                        <asp:RadioButtonList ID="LoantState" Visible="false" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="LoantState_SelectedIndexChanged">
                                            <asp:ListItem Value="0"><div class="circle-tile"><a><div class="circle-tile-heading deep-sky-blue counter">0</div></a><div class="circle-tile-content deep-sky-blue"><div class="circle-tile-description txt-white">Loan Queue</div></div></div></asp:ListItem>
                                            <asp:ListItem Value="1"><div class="circle-tile"><a><div class="circle-tile-heading purple counter">0</div></a><div class="circle-tile-content purple"><div class="circle-tile-description txt-white">Loan Process <small>(HOD)</small></div></div></div></asp:ListItem>
                                            <asp:ListItem Value="2"><div class="circle-tile"><a><div class="circle-tile-heading  deep-pink counter">0</div></a><div class="circle-tile-content  deep-pink"><div class="circle-tile-description txt-white">Loan Request QC <small>(HOHR)</small></div></div></div></asp:ListItem>
                                            <asp:ListItem Value="3"><div class="circle-tile"><a><div class="circle-tile-heading  deep-pink counter">0</div></a><div class="circle-tile-content  deep-pink"><div class="circle-tile-description txt-white">Fund Management <small>(HO Finance)</small></div></div></div></asp:ListItem>
                                            <asp:ListItem Value="4"><div class="circle-tile"><a><div class="circle-tile-heading  orange counter">0</div></a><div class="circle-tile-content  orange"><div class="circle-tile-description txt-white">Loan Approval</div></div></div></asp:ListItem>
                                            <asp:ListItem Value="5"><div class="circle-tile"><a><div class="circle-tile-heading  deep-green counter">0</div></a><div class="circle-tile-content  deep-green"><div class="circle-tile-description txt-white">Loan Generate <small>(HOHR)</small></div></div></div></asp:ListItem>
                                            <asp:ListItem Value="6"><div class="circle-tile"><a><div class="circle-tile-heading  bg-danger text-white counter">0</div></a><div class="circle-tile-content bg-danger"><div class="circle-tile-description txt-white text-white">Loan Cancelled</div></div></div></asp:ListItem>
                                            <asp:ListItem Value="7"><div class="circle-tile"><a><div class="circle-tile-heading  bg-danger text-white counter">0</div></a><div class="circle-tile-content bg-danger"><div class="circle-tile-description txt-white text-white">Loan Cancelled</div></div></div></asp:ListItem>
                                        </asp:RadioButtonList>

                                        <asp:RadioButtonList ID="loanSteps" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="loanSteps_SelectedIndexChanged"></asp:RadioButtonList>

                                    </div>
                                </div>
                            </fieldset>
                            <div>
                                <asp:Panel ID="pnlQue" runat="server" Visible="false">
                                    <div class="row mt-3">
                                        <div class="table table-sm table-responsive">
                                            <asp:GridView CssClass="table-striped table-hover table-bordered grvContentarea" ID="gvPending" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvPending_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblslpend" runat="server" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan ID">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblidPend" Visible="false" Text='<%# Convert.ToString(Eval("id")) %>'></asp:Label>
                                                            <asp:Label ID="lblnnoPend" runat="server">Ln-<%#  Convert.ToString(Eval("id")) %></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan</br> Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcreatedate" runat="server" Text='<%# Convert.ToDateTime( Eval("createdate")).ToString("dd-MMM-yyyy")%>' Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="ID #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpendempid" runat="server" Text='<%#Eval("empid")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblempidPend" runat="server" Text='<%#Eval("idcard")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblempnamepend" runat="server" Text='<%#Eval("empname")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Designation">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldesigpend" runat="server" Text='<%#Eval("desig")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Department">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldeptpend" runat="server" Text='<%#Eval("dept")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblloantypePend" runat="server" Text='<%#Eval("lnname")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblloanamtPend" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "loanamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="# Installment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblinstPend" runat="server" Text='<%#Eval("instlnum")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="center" />

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Per </br> Installment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblperinstlamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perinstlamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstatus" runat="server" 

                                                                CssClass='<%#Convert.ToBoolean(Eval("isaproved"))==false? "badge badge-danger" :"badge badge-success" %>'
                                                                Text='<%#Eval("stepslug")%>'> 
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Effective</br> Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblapplydatPend" runat="server" Text='<%# Convert.ToDateTime( Eval("effdate")).ToString("dd-MMM-yyyy")%>' Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <div class="btn-group">
                                                                <asp:LinkButton ID="pendlnView" OnClick="pendlnView_Click" CssClass="" runat="server" ToolTip="View Loan"><i class="fa fa-eye"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="confmDelModal" OnClick="confmDelModal_Click" runat="server"  ><i class="fa fa-trash"></i></asp:LinkButton>

                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                </Columns>
                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                                <RowStyle CssClass="grvRows" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="pnlLoanProc" runat="server" Visible="false">
                                    <div class="row mt-3">
                                        <div class="table table-sm table-responsive">
                                            <asp:GridView CssClass=" table-striped table-hover table-bordered" ID="gvProcess" OnRowDataBound="gvProcess_RowDataBound" runat="server" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblslpend" runat="server" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan ID">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblidPend" Visible="false" Text='<%# Convert.ToString(Eval("id")) %>'></asp:Label>
                                                            <asp:Label runat="server" ID="lbllnstatus" Visible="false" Text='<%# Convert.ToString(Eval("lnstatus")) %>'></asp:Label>

                                                            <asp:Label ID="lblnnoPend" runat="server">Ln-<%#  Convert.ToString(Eval("id")) %></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Effective</br> Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblapplydatPend" runat="server" Text='<%# Convert.ToDateTime( Eval("effdate")).ToString("dd-MMM-yyyy")%>' Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ID #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpendempid" runat="server" Text='<%#Eval("empid")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblempidPend" runat="server" Text='<%#Eval("idcard")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblempnamepend" runat="server" Text='<%#Eval("empname")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Designation">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldesigpend" runat="server" Text='<%#Eval("desig")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Department">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldeptpend" runat="server" Text='<%#Eval("dept")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblloantypePend" runat="server" Text='<%#Eval("lnname")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblloanamtPend" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "loanamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="# Installment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblinstPend" runat="server" Text='<%#Eval("instlnum")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="center" />

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Per </br> Installment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblperinstlamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perinstlamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <div class="btn-group">
                                                                <asp:LinkButton ID="proslnView" OnClick="proslnView_Click" runat="server" ToolTip="View Loan"><i class="fa fa-eye"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="pendlnEdit" OnClick="pendlnEdit_Click" Visible="false" runat="server" ToolTip="Edit Loan"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="pendlnAproved" OnClick="AprvProcsView" Visible="false" runat="server" ToolTip="Approve Loan"><i class="fa fa-check"></i></asp:LinkButton>

                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                </Columns>
                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                                <RowStyle CssClass="grvRows" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="pnlStep3HOHR" runat="server" Visible="false">
                                    <div class="row mt-3">
                                        <div class="table table-sm table-responsive">
                                            <asp:GridView CssClass=" table-striped table-hover table-bordered" ID="gvStep3HOHR" OnRowDataBound="gvStep3HOHR_RowDataBound" runat="server" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblslpend" runat="server" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan ID">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblidPend" Visible="false" Text='<%# Convert.ToString(Eval("id")) %>'></asp:Label>
                                                            <asp:Label runat="server" ID="lbllnstatus" Visible="false" Text='<%# Convert.ToString(Eval("lnstatus")) %>'></asp:Label>

                                                            <asp:Label ID="lblnnoPend" runat="server">Ln-<%#  Convert.ToString(Eval("id")) %></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Effective</br> Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblapplydatPend" runat="server" Text='<%# Convert.ToDateTime( Eval("effdate")).ToString("dd-MMM-yyyy")%>' Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ID #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpendempid" runat="server" Text='<%#Eval("empid")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblempidPend" runat="server" Text='<%#Eval("idcard")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblempnamepend" runat="server" Text='<%#Eval("empname")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Designation">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldesigpend" runat="server" Text='<%#Eval("desig")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Department">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldeptpend" runat="server" Text='<%#Eval("dept")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblloantypePend" runat="server" Text='<%#Eval("lnname")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblloanamtPend" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "loanamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="# Installment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblinstPend" runat="server" Text='<%#Eval("instlnum")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="center" />

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Per </br> Installment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblperinstlamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perinstlamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <div class="btn-group">
                                                                <asp:LinkButton ID="Step3HOHRView" OnClick="Step3HOHRView_Click" runat="server" ToolTip="View Loan"><i class="fa fa-eye"></i></asp:LinkButton>

                                                                <asp:LinkButton ID="pendlnAproved_Step3" OnClick="AprvProcsView_Step3" Visible="false" runat="server" ToolTip="Approve Loan"><i class="fa fa-check"></i></asp:LinkButton>

                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                </Columns>
                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                                <RowStyle CssClass="grvRows" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="pnlStepHOF" runat="server" Visible="false">
                                    <div class="row mt-3">
                                        <div class="table table-sm table-responsive">
                                            <asp:GridView CssClass=" table-striped table-hover table-bordered" ID="gridViewHOFinance" OnRowDataBound="gridViewHOFinance_RowDataBound" runat="server" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblslpend" runat="server" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan ID">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblidPend" Visible="false" Text='<%# Convert.ToString(Eval("id")) %>'></asp:Label>
                                                            <asp:Label runat="server" ID="lbllnstatus" Visible="false" Text='<%# Convert.ToString(Eval("lnstatus")) %>'></asp:Label>

                                                            <asp:Label ID="lblnnoPend" runat="server">Ln-<%#  Convert.ToString(Eval("id")) %></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Effective</br> Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblapplydatPend" runat="server" Text='<%# Convert.ToDateTime( Eval("effdate")).ToString("dd-MMM-yyyy")%>' Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ID #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpendempid" runat="server" Text='<%#Eval("empid")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblempidPend" runat="server" Text='<%#Eval("idcard")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblempnamepend" runat="server" Text='<%#Eval("empname")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Designation">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldesigpend" runat="server" Text='<%#Eval("desig")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Department">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldeptpend" runat="server" Text='<%#Eval("dept")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblloantypePend" runat="server" Text='<%#Eval("lnname")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblloanamtPend" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "loanamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="# Installment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblinstPend" runat="server" Text='<%#Eval("instlnum")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="center" />

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Per </br> Installment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblperinstlamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perinstlamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <div class="btn-group">
                                                                <asp:LinkButton ID="ViewHOFinance" OnClick="ViewHOFinance_Click" runat="server" ToolTip="View Loan"><i class="fa fa-eye"></i></asp:LinkButton>


                                                                <asp:LinkButton ID="pendlnAproved_Step4" OnClick="AprvProcsView_Step4" Visible="false" runat="server" ToolTip="Approve Loan"><i class="fa fa-check"></i></asp:LinkButton>

                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                </Columns>
                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                                <RowStyle CssClass="grvRows" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="pnlLoanAppr" runat="server" Visible="false">
                                    <div class="row mt-3">
                                        <div class="table table-sm table-responsive">
                                            <asp:GridView CssClass=" table-striped table-hover table-bordered" ID="gvApproved" runat="server" OnRowDataBound="gvApproved_RowDataBound" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblslpend" runat="server" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan ID">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblidPend" Visible="false" Text='<%# Convert.ToString(Eval("id")) %>'></asp:Label>
                                                            <asp:Label runat="server" ID="lbllnstatus" Visible="false" Text='<%# Convert.ToString(Eval("lnstatus")) %>'></asp:Label>
                                                            <asp:Label ID="lblnnoPend" runat="server">Ln-<%#  Convert.ToString(Eval("id")) %></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Effective</br> Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblapplydatPend" runat="server" Text='<%# Convert.ToDateTime( Eval("effdate")).ToString("dd-MMM-yyyy")%>' Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ID #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpendempid" runat="server" Text='<%#Eval("empid")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblempidPend" runat="server" Text='<%#Eval("idcard")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblempnamepend" runat="server" Text='<%#Eval("empname")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Designation">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldesigpend" runat="server" Text='<%#Eval("desig")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Department">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldeptpend" runat="server" Text='<%#Eval("dept")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblloantypePend" runat="server" Text='<%#Eval("lnname")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblloanamtPend" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "loanamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="# Installment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblinstPend" runat="server" Text='<%#Eval("instlnum")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="center" />

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Per </br> Installment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblperinstlamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perinstlamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <div class="btn-group">
                                                                <asp:LinkButton ID="ViewApproved" OnClick="ViewApproved_Click" runat="server" ToolTip="View Loan"><i class="fa fa-eye"></i></asp:LinkButton>

                                                                <asp:LinkButton ID="AprlnView" OnClick="AprlnView_Click" runat="server" Visible="false" ToolTip="Aprove Loan"><i class="fa fa-check"></i></asp:LinkButton>

                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--               <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <div class="btn-group">
                                                                <asp:LinkButton ID="pendlnEditApr" OnClick="pendlnEditApr_Click" runat="server" ToolTip="Edit Loan"><i class="fa fa-edit"></i></asp:LinkButton>

                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                    <%-- <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <div class="btn-group">
                                                               <asp:LinkButton ID="confmDelModal" OnClick="confmDelModal_Click" runat="server" ><i class="fa fa-trash"></i></asp:LinkButton>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                </Columns>
                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                                <RowStyle CssClass="grvRows" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlLoangen" runat="server" Visible="false">
                                    <div class="row mt-3">
                                        <div class="table table-sm table-responsive">
                                            <asp:GridView CssClass=" table-striped table-hover table-bordered" ID="gvGen" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvGen_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblslpend" runat="server" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan ID">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblidPend" Visible="false" Text='<%# Convert.ToString(Eval("id")) %>'></asp:Label>
                                                            <asp:Label runat="server" ID="lbllnstatus" Visible="false" Text='<%# Convert.ToString(Eval("lnstatus")) %>'></asp:Label>
                                                            <asp:Label ID="lblnnoPend" runat="server">Ln-<%#  Convert.ToString(Eval("id")) %></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Effective</br> Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblapplydatPend" runat="server" Text='<%# Convert.ToDateTime( Eval("effdate")).ToString("dd-MMM-yyyy")%>' Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ID #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpendempid" runat="server" Text='<%#Eval("empid")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblempidPend" runat="server" Text='<%#Eval("idcard")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblempnamepend" runat="server" Text='<%#Eval("empname")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Designation">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldesigpend" runat="server" Text='<%#Eval("desig")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Department">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldeptpend" runat="server" Text='<%#Eval("dept")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblloantypePend" runat="server" Text='<%#Eval("lnname")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblloanamtPend" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "loanamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="# Installment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblinstPend" runat="server" Text='<%#Eval("instlnum")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="center" />

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Per </br> Installment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblperinstlamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perinstlamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <div class="btn-group">
                                                                <asp:LinkButton ID="LoGenlnView" OnClick="LoGenlnView_Click" runat="server" ToolTip="View Loan"><i class="fa fa-eye"></i></asp:LinkButton>

                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <div class="btn-group">
                                                                <asp:HyperLink ID="lnkbtnInd" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs" ToolTip="Aprove Loan"><span class=" fa fa-check"></span>
                                                                </asp:HyperLink>
                                                                <asp:LinkButton ID="LoGenprint" runat="server" OnClick="LoGenprint_Click" CssClass="btn btn-default btn-xs" ToolTip="print Loan"><i class="fa fa-print"></i></asp:LinkButton>

                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <div class="btn-group">
                                                               <asp:LinkButton ID="confmDelModal" OnClick="confmDelModal_Click" runat="server" ><i class="fa fa-trash"></i></asp:LinkButton>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                </Columns>
                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                                <RowStyle CssClass="grvRows" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlLoanComp" runat="server" Visible="false">
                                    <div class="row mt-3">
                                        <div class="table table-sm table-responsive">
                                            <asp:GridView CssClass=" table-striped table-hover table-bordered" ID="gvCompleted" runat="server" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblslpend" runat="server" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan ID">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblidPend" Visible="false" Text='<%# Convert.ToString(Eval("id")) %>'></asp:Label>
                                                            <asp:Label runat="server" ID="lbllnstatus" Visible="false" Text='<%# Convert.ToString(Eval("lnstatus")) %>'></asp:Label>
                                                            <asp:Label ID="lblnnoPend" runat="server">Ln-<%#  Convert.ToString(Eval("id")) %></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Final</br> Loan ID">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lbldatalnno" Visible="false" Text='<%# Convert.ToString(Eval("lnno")) %>'></asp:Label>
                                                            <asp:Label ID="lbllnno" runat="server"><%#  Convert.ToString(Eval("lnno")) %></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Effective</br> Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblapplydatPend" runat="server" Text='<%# Convert.ToDateTime( Eval("effdate")).ToString("dd-MMM-yyyy")%>' Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ID #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpendempid" runat="server" Text='<%#Eval("empid")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblempidPend" runat="server" Text='<%#Eval("idcard")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblempnamepend" runat="server" Text='<%#Eval("empname")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Designation">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldesigpend" runat="server" Text='<%#Eval("desig")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Department">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldeptpend" runat="server" Text='<%#Eval("dept")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblloantypePend" runat="server" Text='<%#Eval("lnname")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblloanamtPend" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "loanamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="# Installment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblinstPend" runat="server" Text='<%#Eval("instlnum")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="center" />

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Per </br> Installment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblperinstlamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perinstlamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <%--<asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <div class="btn-group">
                                                                <asp:LinkButton ID="LoGenlnView" OnClick="LoGenlnView_Click" runat="server"><i class="fa fa-eye"></i></asp:LinkButton>

                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <%-- <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <div class="btn-group">
                                                                 <asp:HyperLink ID="lnkbtnInd" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs"><span class=" fa fa-check"></span>
                                                                    </asp:HyperLink>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                </Columns>
                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                                <RowStyle CssClass="grvRows" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlCanc" runat="server" Visible="false">
                                    <div class="row mt-3">
                                        <div class="table table-sm table-responsive">
                                            <asp:GridView CssClass="table-striped table-hover table-bordered grvContentarea" ID="gvCanc" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvCanc_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblslpend" runat="server" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan ID">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblidPend" Visible="false" Text='<%# Convert.ToString(Eval("id")) %>'></asp:Label>
                                                            <asp:Label ID="lblnnoPend" runat="server">Ln-<%#  Convert.ToString(Eval("id")) %></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan</br> Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcreatedate" runat="server" Text='<%# Convert.ToDateTime( Eval("createdate")).ToString("dd-MMM-yyyy")%>' Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="ID #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpendempid" runat="server" Text='<%#Eval("empid")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblempidPend" runat="server" Text='<%#Eval("idcard")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblempnamepend" runat="server" Text='<%#Eval("empname")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Designation">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldesigpend" runat="server" Text='<%#Eval("desig")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Department">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldeptpend" runat="server" Text='<%#Eval("dept")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblloantypePend" runat="server" Text='<%#Eval("lnname")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblloanamtPend" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "loanamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="# Installment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblinstPend" runat="server" Text='<%#Eval("instlnum")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="center" />

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Per </br> Installment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblperinstlamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perinstlamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                               
                                                    <asp:TemplateField HeaderText="Effective</br> Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblapplydatPend" runat="server" Text='<%# Convert.ToDateTime( Eval("effdate")).ToString("dd-MMM-yyyy")%>' Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <div class="btn-group">
                                                                <asp:LinkButton ID="CancelpendlnView" OnClick="CancelpendlnView_Click" runat="server" ToolTip="View Loan"><i class="fa fa-eye"></i></asp:LinkButton>

                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <div class="btn-group">
                                                                <asp:LinkButton ID="pendlnEdit" OnClick="pendlnEdit_Click" runat="server" ><i class="fa fa-edit"></i></asp:LinkButton>
                                                                
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <div class="btn-group">
                                                                <asp:LinkButton ID="confmDelModal" OnClick="confmDelModal_Click" Visible="false" runat="server" CssClass='<%#Convert.ToBoolean(Eval("isaproved"))==false? "" :"d-none" %>'><i class="fa fa-trash"></i></asp:LinkButton>

                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                                <RowStyle CssClass="grvRows" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="ApplyLoan" class="modal " role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog modal-xl">
                    <div class="modal-content">
                        <div class="modal-header bg-light">
                            <h6 class="modal-title">Apply Loan</h6>
                            <asp:LinkButton ID="ModalLoanClose" runat="server" CssClass="close close_btn" OnClientClick="ModalLoanClose();" data-dismiss="modal"> &times; </asp:LinkButton>
                            <%-- <button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                        </div>
                        <div class="modal-body">

                            <div class="row mt-2">
                                <div class="col-lg-1">
                                    <div class="form-group">
                                        <asp:Label ID="lblLoanId" runat="server">Loan Id</asp:Label>
                                        <asp:TextBox ID="txtLoanId" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblcreateDate" runat="server">Create Date 
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red" ValidationGroup="one"
                                                   ControlToValidate="txtcreateDate" ErrorMessage="Required" Font-Size="8" Font-Italic="true"></asp:RequiredFieldValidator>
                                        </asp:Label>
                                        <asp:TextBox ID="txtcreateDate" runat="server" CssClass="form-control form-control-sm  mr-2" ValidationGroup="one"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtcreateDate"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label6" runat="server">Employee Name</asp:Label>
                                        <asp:DropDownList ID="ddlEmpList" runat="server" CssClass="form-control form-control-sm chzn-select" OnSelectedIndexChanged="ddlEmpList_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblLoanAmt" runat="server">Loan Amount *
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Font-Size="8" Font-Italic="true" runat="server" ForeColor="Red" ValidationGroup="one"
                                                ControlToValidate="txtLoanAmt" ErrorMessage="Required">
                                            </asp:RequiredFieldValidator>
                                        </asp:Label>
                                        <asp:TextBox ID="txtLoanAmt" runat="server" CssClass="form-control form-control-sm" onKeyUp="sum()" onkeypress="return isNumberKey(this, event);" ValidationGroup="one"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label4" runat="server" CssClass="">Instalment No *
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ValidationGroup="one"
                                            ControlToValidate="txtInstNum" ErrorMessage="Required" Font-Size="8" Font-Italic="true"></asp:RequiredFieldValidator>
                                        </asp:Label>
                                        <asp:TextBox ID="txtInstNum" runat="server" CssClass="form-control form-control-sm" onKeyUp="sum()" Text="1" onkeypress="return isNumberKey(this, event);" ValidationGroup="four"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblAmtPerIns" runat="server">Amount Per Inst</asp:Label>
                                        <asp:TextBox ID="txtAmtPerIns" runat="server" CssClass="form-control form-control-sm" Text="0" Enabled="false" onkeypress="return isNumberKey(this, event);"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblStd" runat="server" CssClass="">Statutory Deduction</asp:Label>
                                        <asp:TextBox ID="txtStd" runat="server" CssClass="form-control form-control-sm" onkeypress="return isNumberKey(this, event);"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblPloanAmt" runat="server">Previous loan Amount</asp:Label>
                                        <asp:TextBox ID="txtPloanAmt" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label5" runat="server">Gross Monthly Salary</asp:Label>
                                        <asp:TextBox ID="txtGMS" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblOI" runat="server">Other Income</asp:Label>
                                        <asp:TextBox ID="txtOI" runat="server" CssClass="form-control form-control-sm" onkeypress="return isNumberKey(this, event);"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblrt" runat="server">Intrest Rate(%)</asp:Label>
                                        <asp:TextBox ID="txtrt" runat="server" CssClass="form-control form-control-sm" onkeypress="return isNumberKey(this, event);"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblPFAmt" runat="server">Provident Fund</asp:Label>
                                        <asp:TextBox ID="txtPFAmt" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group ">
                                        <asp:Label ID="lblTax" runat="server">Income Tax</asp:Label>
                                        <asp:TextBox ID="txtTax" runat="server" CssClass="form-control form-control-sm" Enabled="false" onkeypress="return isNumberKey(this, event);"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblOD" runat="server">Other Deduction</asp:Label>
                                        <asp:TextBox ID="txtOD" runat="server" CssClass="form-control form-control-sm" onkeypress="return isNumberKey(this, event);"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblEffDate" runat="server">Effective Date *
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Font-Size="8" Font-Italic="true" ForeColor="Red" ValidationGroup="one"
                                             ControlToValidate="txtEffDate" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        </asp:Label>
                                        <asp:TextBox ID="txtEffDate" runat="server" CssClass="form-control form-control-sm  mr-2" ValidationGroup="one"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtEffDate"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblLoanType" runat="server">Loan Type *</asp:Label>
                                        <asp:DropDownList ID="ddlLoanType" runat="server" CssClass="form-control form-control-sm">
                                        </asp:DropDownList>
                                    </div>
                                </div>



                            </div>


                            <div class="row">
                                <div class="col-lg-6" runat="server" id="dibNote" visible="false">
                                    <div class="form-group">
                                        <asp:Label ID="lblnote" runat="server">Approved Note</asp:Label>
                                        <asp:TextBox ID="txtnote" runat="server" CssClass="form-control form-control-sm" TextMode="MultiLine" Rows="3" Style="min-height: 70px;"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <asp:Label ID="lblLoanDesc" runat="server">Purpose of loan</asp:Label>
                                        <asp:TextBox ID="txtLoanDescc" runat="server" CssClass="form-control form-control-sm" TextMode="MultiLine" Rows="3" Style="min-height: 70px;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row" id="pnlApprovalStatus">
                                <div class="col-md-12">
                                    <div class="table table-sm table-responsive">
                                        <asp:GridView CssClass="table-striped table-hover table-bordered grvContentarea" ID="gvLoanApprovalLog" runat="server" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Loglblslpend" runat="server" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Approved</br> Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Loglblcreatedate" runat="server" Text='<%# Convert.ToDateTime( Eval("posteddat")).ToString("dd-MMM-yyyy")%>' Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Approved by">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Loglblempnamepend" runat="server" Text='<%#Eval("empname")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Designation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Loglbldesigpend" runat="server" Text='<%#Eval("desig")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LoglblRemarks" runat="server" Text='<%#Eval("remarks")%>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                            <RowStyle CssClass="grvRows" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>


                            <div class="rowmt-2">
                                <div class="d-flex justify-content-center">
                                    <asp:LinkButton ID="lnkForwardStep" CssClass="btn btn-danger btn-sm m-2 p2  bw-100" runat="server" OnClick="lnkForwardStep_Click" Visible="false" ValidationGroup="one">Forward Back</asp:LinkButton>
                                    <asp:LinkButton ID="lnkCancel" CssClass="btn btn-danger btn-sm m-2 p2  bw-100" runat="server" OnClick="lnkCancel_Click" Visible="false" ValidationGroup="one">Request Cancel</asp:LinkButton>

                                       <asp:LinkButton ID="PrintLoan" CssClass="btn btn-primary btn-sm m-2 p2  bw-100" runat="server" OnClick="PrintLoan_Click" ValidationGroup="one" Visible="false">Print</asp:LinkButton>
                                    <asp:LinkButton ID="lnkAdd" CssClass="btn btn-success btn-sm m-2 p2  bw-100" runat="server" OnClick="lnkAdd_Click" ValidationGroup="one" Visible="false">Save</asp:LinkButton>
                                    <asp:LinkButton ID="lnkEdited" CssClass="btn btn-success btn-sm m-2 p2  bw-100" runat="server" OnClick="lnkEdited_Click" ValidationGroup="one" Visible="false">Update</asp:LinkButton>
                                    <asp:LinkButton ID="lnkUpdate" CssClass="btn btn-primary btn-sm m-2 p2  bw-100" runat="server" OnClick="lnkUpdate_Click" ValidationGroup="one" Visible="false">Loan Process</asp:LinkButton>
                                    <asp:LinkButton ID="lnkApprov" CssClass="btn btn-success btn-sm m-2 p2  bw-100" runat="server" OnClick="lnkApprov_Click" Visible="false" ValidationGroup="one">Approve</asp:LinkButton>
                                    <button type="button" class="btn btn-sm m-2 p2  bw-100 btn-secondary" data-dismiss="modal">Close</button>

                                    <%--<asp:LinkButton ID="lnkCancel" CssClass="btn btn-danger btn-sm m-2 p2  bw-100" runat="server" Visible="false" data-dismiss="modal" ValidationGroup="one">Cancel</asp:LinkButton>--%>
                                    <asp:HiddenField ID="stepIDNEXT" runat="server" />
                                    <asp:HiddenField ID="loanID" runat="server" />

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%-- delete modal --%>
            <div class="modal fade" id="deleteModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLabel">Are you sure, want to delete?</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <p class="text-center">
                                <asp:LinkButton runat="server" ID="confirmDelete" OnClick="confirmDelete_click" CssClass="btn btn-danger btn-sm bw-100">Yes, Delete</asp:LinkButton>
                                <asp:HiddenField runat="server" ID="delid" />
                                <asp:HiddenField runat="server" ID="delempid" />
                                <%-- <p runat="server" id="delid" visible="false"></p>
                                <p runat="server" id="delempid" visible="false"></p>--%>
                            </p>
                        </div>

                    </div>
                </div>
            </div>

            <asp:HiddenField ID="hiddenSeletedIndex" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

