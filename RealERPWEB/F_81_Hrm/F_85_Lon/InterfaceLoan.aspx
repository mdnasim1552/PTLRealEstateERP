<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="InterfaceLoan.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_85_Lon.InterfaceLoan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <style>
        .mt20 {
            margin-top: 20px;
        }

        input#ContentPlaceHolder1_txtSearch {
            height: 29px;
        }

        .bw-100 {
            width: 100px !important;
        }

        .tbMenuWrp table {
            border: none !important;
            background: none !important;
        }

            .tbMenuWrp table tr {
                border: none !important;
                background: none !important;
            }

                .tbMenuWrp table tr td {
                    width: 140px;
                    float: left;
                    list-style: none;
                    margin: 2px 5px;
                    border: 0;
                    cursor: pointer;
                    background: #fff;
                    position: relative;
                    -webkit-border-radius: 5px;
                    -moz-border-radius: 5px;
                    border-radius: 5px;
                }

                    .tbMenuWrp table tr td label {
                        color: #000;
                        cursor: pointer;
                        font-weight: bold;
                        height: 100%;
                        margin: 1px 0;
                        padding: 2px;
                        width: 100%;
                    }

                        .tbMenuWrp table tr td label.active > a, .tbMenuWrp table tr td label.active > .tbMenuWrp table tr td label:focus, .tbMenuWrp table tr td label.active > a:hover {
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
            background-color: #00ff21;
        }

        .tbMenuWrp table tr td label span.lbldata {
            border-radius: 50%;
            color: #fff;
            font-size: 17px;
            font-weight: bold;
            padding: 2px;
        }

        .rptPurInt span.lbldata2 {
            display: block;
            font-size: 12px;
            color: #fff;
            line-height: 22px;
            margin: 5px 0 0;
            padding: 0;
            text-align: center;
        }

        .tbMenuWrp table tr td:nth-child(1) {
            background: #0179a8 !important;
        }

        .tbMenuWrp table tr td:nth-child(2) {
            background: #5f4b8b !important;
        }

        .tbMenuWrp table tr td:nth-child(3) {
            background: #b76ba3!important;
        }

        .tbMenuWrp table tr td:nth-child(4) {
            background: #f7c46c!important;
            
        }

        .tbMenuWrp table tr td:nth-child(5) {
            background:#00a28a !important;
        }
    </style>

    <script>
        function sum() {

            var txtFirstNumberValue = document.getElementById("<%=txtLoanAmt.ClientID%>").value;
            var txtSecondNumberValue = document.getElementById("<%=txtInstNum.ClientID%>").value;
            var result = parseInt(txtFirstNumberValue) / parseFloat(txtSecondNumberValue);
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
    </script>

    <script>
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
            $('#ApplyLoan').modal('toggle');
        }
        function OpenDeleteModal() {
            $('#deleteModal').modal('toggle');
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
                <div class="card mt-5">
                    <div class="card-header pt-2 pb-2">
<<<<<<< HEAD

=======
>>>>>>> ca90ee60beedbcfdb94ecd8a9c145f1ffca7a952
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
                            <div class="col-lg-2">

                                <asp:Label ID="Label3" runat="server">Search Emp.</asp:Label>
                                <div class="input-group input-group-alt input-group-sm">
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    <div class="input-group-prepend ">
                                        <asp:LinkButton ID="lnkbtnok" runat="server" CssClass=" btn btn-light btn-sm "><i class="fa fa-search"></i></asp:LinkButton></li>
                                    </div>
                                </div>

                            </div>
                            <div class="col-lg-4">


                                <asp:Label ID="Label19" runat="server">Loan Type</asp:Label>
                                <asp:DropDownList ID="ddlLoanTypeSearch" runat="server" CssClass="form-control form-control-sm">
                                </asp:DropDownList>

                            </div>
                            <div class="col-lg-2 d-flex">

                                <asp:LinkButton ID="lnkApplyModal" runat="server" CssClass="btn btn-primary ml-auto bw-100 btn-sm mt20" OnClick="lnkApplyModal_Click">Apply Loan</asp:LinkButton>
                            </div>
                        </div>
                    </div>

                    <div class="card-body">
                        <div class="panel with-nav-tabs panel-primary">
                            <fieldset class="tabMenu">
                                <div class="form-horizontal">
<<<<<<< HEAD

                                    <div class="tbMenuWrp nav nav-tabs rptPurInt text-center text-white">
                                        <asp:RadioButtonList ID="LoantState" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="LoantState_SelectedIndexChanged">
=======
                                    <div class="tbMenuWrp nav nav-tabs rptPurInt text-center text-white">
                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" RepeatDirection="Horizontal">
>>>>>>> ca90ee60beedbcfdb94ecd8a9c145f1ffca7a952
                                            <asp:ListItem Value="0"><h4 ><span class="font-weight-bold text-white">44</span></h4><span class="font-weight-bold text-white">Loan Queue</span></asp:ListItem>
                                            <asp:ListItem Value="1"><h4><span class="font-weight-bold text-white">44</span></h4><span class="font-weight-bold text-white">Loan Process</span></asp:ListItem>
                                            <asp:ListItem Value="2"><h4><span class="font-weight-bold text-white">44</span></h4><span class="font-weight-bold text-white">Loan Approval</span></asp:ListItem>
                                            <asp:ListItem Value="3"><h4><span class="font-weight-bold text-white">44</span></h4><span class="font-weight-bold text-white">Loan Generate</span></asp:ListItem>
                                            <asp:ListItem Value="4"><h4><span class="font-weight-bold text-white">44</span></h4><span class=" font-weight-bold text-white">Loan Completed</span></asp:ListItem>
                                        </asp:RadioButtonList>
<<<<<<< HEAD

=======
>>>>>>> ca90ee60beedbcfdb94ecd8a9c145f1ffca7a952
                                    </div>

                                </div>
                            </fieldset>
                            <div>
                                <asp:Panel ID="pnlQue" runat="server" Visible="false">
                                    <div class="row mt-3">
                                        <div class="table table-sm table-responsive">
                                            <asp:GridView CssClass=" table-striped table-hover table-bordered" ID="gvPending" runat="server" AutoGenerateColumns="false">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="SL#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblslpend" runat="server" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Loan ID">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblidPend" Visible="false" Text='<%# Convert.ToString(Eval("id")) %>'></asp:Label>
                                                            <asp:Label ID="lblnnoPend" runat="server">Ln-<%#  Convert.ToString(Eval("id")) %></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Apply Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblapplydatPend" runat="server" Text='<%# Convert.ToDateTime( Eval("effdate")).ToString("dd-MMM-yyyy")%>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="ID Card">
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblpendempid" runat="server" Text='<%#Eval("empid")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblempidPend" runat="server" Text='<%#Eval("idcard")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Emp Name">
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
                                                            <asp:Label ID="lblloanamtPend" runat="server" Text='<%#Eval("loanamt")%>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Loan Installment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblinstPend" runat="server" Text='<%#Eval("instlnum")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <div class="btn-group">
                                                                <asp:LinkButton ID="pendlnView" OnClick="pendlnView_Click" runat="server" class="btn btn-info btn-sm mr-1"><i class="fa fa-eye"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="pendlnEdit" OnClick="pendlnEdit_Click" runat="server" class="btn btn-primary btn-sm mr-1 ml-1"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="confmDelModal" OnClick="confmDelModal_Click" runat="server" class="btn btn-danger btn-sm"><i class="fa fa-trash"></i></asp:LinkButton>

                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>




                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="pnlLoanProc" runat="server" Visible="false">
                                    <div class="row mt-3">
                                        <div class="table table-sm table-responsive">
                                            <asp:GridView CssClass=" table-striped table-hover table-bordered" ID="gvProcess" runat="server" AutoGenerateColumns="false">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="SL#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblslproc" runat="server" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Loan ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnnoProc" runat="server">#Loan<%#  Convert.ToString(Eval("id")) %></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Apply Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblapplydatProc" runat="server" Text='<%# Convert.ToDateTime( Eval("effdate")).ToString("dd-MMM-yyyy")%>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Employee ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblempidProc" runat="server" Text='<%#Eval("empid")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Loan Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblloantypeProc" runat="server" Text='<%#Eval("lnname")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Loan Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblloanamtProc" runat="server" Text='<%#Eval("loanamt")%>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Loan Installment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblinstProc" runat="server" Text='<%#Eval("instlnum")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <div class="btn-group">
                                                                <asp:LinkButton ID="proclnView" runat="server" class="btn btn-info btn-sm mr-1"><i class="fa fa-eye"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="proclnEdit" runat="server" class="btn btn-primary btn-sm mr-1 ml-1"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="proclnDel" runat="server" class="btn btn-danger btn-sm"><i class="fa fa-trash"></i></asp:LinkButton>

                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>




                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="pnlLoanAppr" runat="server" Visible="false">
                                    <div class="row mt-3">
                                        <div class="table table-sm table-responsive">
                                            <asp:GridView CssClass=" table-striped table-hover table-bordered" ID="gvApproved" runat="server" AutoGenerateColumns="false">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="SL#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblslapr" runat="server" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Loan ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnnoApr" runat="server">#Loan<%#  Convert.ToString(Eval("id")) %></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Apply Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblaplydatApr" runat="server" Text='<%# Convert.ToDateTime( Eval("effdate")).ToString("dd-MMM-yyyy")%>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Employee ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblempidApr" runat="server" Text='<%#Eval("empid")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Loan Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbllntypeApr" runat="server" Text='<%#Eval("lnname")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Loan Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblloanamtApr" runat="server" Text='<%#Eval("loanamt")%>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Loan Installment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblinstApr" runat="server" Text='<%#Eval("instlnum")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <div class="btn-group">
                                                                <asp:LinkButton ID="aprlnView" runat="server" class="btn btn-info btn-sm mr-1"><i class="fa fa-eye"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="aprclnEdit" runat="server" class="btn btn-primary btn-sm mr-1 ml-1"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="aprclnDel" runat="server" class="btn btn-danger btn-sm"><i class="fa fa-trash"></i></asp:LinkButton>

                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>




                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="pnlLoangen" runat="server" Visible="false">
                                    <div class="row mt-3">
                                        <div class="table table-sm table-responsive">
                                            <asp:GridView CssClass=" table-striped table-hover table-bordered" ID="gvGen" runat="server" AutoGenerateColumns="false">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="SL#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblslgen" runat="server" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Loan ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblidgen" runat="server">#Loan<%#  Convert.ToString(Eval("id")) %></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Apply Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblaplydatGen" runat="server" Text='<%# Convert.ToDateTime( Eval("effdate")).ToString("dd-MMM-yyyy")%>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Employee ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblempidGen" runat="server" Text='<%#Eval("empid")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Loan Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbllntypeGen" runat="server" Text='<%#Eval("lnname")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Loan Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblloanamtGen" runat="server" Text='<%#Eval("loanamt")%>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Loan Installment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblinstGen" runat="server" Text='<%#Eval("instlnum")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <div class="btn-group">
                                                                <asp:LinkButton ID="genlnView" runat="server" class="btn btn-info btn-sm mr-1"><i class="fa fa-eye"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="genlnEdit" runat="server" class="btn btn-primary btn-sm mr-1 ml-1"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="genlnDel" runat="server" class="btn btn-danger btn-sm"><i class="fa fa-trash"></i></asp:LinkButton>

                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>




                                                </Columns>
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
                                                            <asp:Label ID="lblslComp" runat="server" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Loan ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblidComp" runat="server">#Loan<%#  Convert.ToString(Eval("id")) %></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Apply Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblaplydatComp" runat="server" Text='<%# Convert.ToDateTime( Eval("effdate")).ToString("dd-MMM-yyyy")%>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Employee ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblempidComp" runat="server" Text='<%#Eval("empid")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Loan Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbllntypeComp" runat="server" Text='<%#Eval("lnname")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Loan Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblloanamtComp" runat="server" Text='<%#Eval("loanamt")%>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Loan Installment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblinstComp" runat="server" Text='<%#Eval("instlnum")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <div class="btn-group">
                                                                <asp:LinkButton ID="complnView" runat="server" class="btn btn-info btn-sm mr-1"><i class="fa fa-eye"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="complnEdit" runat="server" class="btn btn-primary btn-sm mr-1 ml-1"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="complnDel" runat="server" class="btn btn-danger btn-sm"><i class="fa fa-trash"></i></asp:LinkButton>

                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>




                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>

                            </div>
                        </div>

<<<<<<< HEAD

=======
                        <div class="table table-sm table-responsive">
                            <asp:GridView CssClass=" table-striped table-hover table-bordered" ID="gvloan" runat="server" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsl" runat="server" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Apply Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblapply" runat="server" Text='<%# Convert.ToDateTime( Eval("effdate")).ToString("dd-MMM-yyyy")%>' Width="200px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Loan No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblid" runat="server" Text='<%#  Eval("id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Employee ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblempid" runat="server" Text='<%#Eval("empid")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Loan Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblloantype" runat="server" Text='<%#Eval("loantype")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Loan Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="loanamt" runat="server" Text='<%#Eval("loanamt")%>' Width="100px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Loan Installment">
                                        <ItemTemplate>
                                            <asp:Label ID="lblinst" runat="server" Text='<%#Eval("instlnum")%>' Width="10px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
>>>>>>> ca90ee60beedbcfdb94ecd8a9c145f1ffca7a952

                    </div>
                </div>
            </div>



            <div class="modal" id="ApplyLoan" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog modal-xl">
                    <div class="modal-content">
                        <div class="modal-header bg-light">
                            <h6 class="modal-title">Apply Loan</h6>
                            <asp:LinkButton ID="ModalLoanClose" runat="server" CssClass="close close_btn" OnClientClick="ModalLoanClose();" data-dismiss="modal"> &times; </asp:LinkButton>
                            <%-- <button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                        </div>
                        <div class="modal-body">
                            <div class="row mt-2">
                                <div class="col-lg-3">

                                    <div class="row">
                                        <div class="form-group col-6 p-0">
                                            <asp:Label ID="lblLoanId" runat="server">Loan Id</asp:Label>
                                            <asp:TextBox ID="txtLoanId" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-6">
                                            <asp:Label ID="lblcreateDate" runat="server">Create Date 

<<<<<<< HEAD
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red" ValidationGroup="one"
                                                                                                        ControlToValidate="txtcreateDate" ErrorMessage="Required" Font-Size="8" Font-Italic="true">
=======
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                                                                                                        ControlToValidate="txtcreateDate" ErrorMessage="Enter create date" Font-Size="8" Font-Italic="true">
>>>>>>> ca90ee60beedbcfdb94ecd8a9c145f1ffca7a952
                                                                                                    </asp:RequiredFieldValidator>
                                            </asp:Label>

                                            <asp:TextBox ID="txtcreateDate" runat="server" CssClass="form-control form-control-sm  mr-2" ValidationGroup="one"></asp:TextBox>


<<<<<<< HEAD
=======

>>>>>>> ca90ee60beedbcfdb94ecd8a9c145f1ffca7a952

                                        </div>

                                    </div>



                                    <div class="form-group">
                                        <asp:Label ID="lblStd" runat="server" CssClass="">Statutory Deduction</asp:Label>
                                        <asp:TextBox ID="txtStd" runat="server" CssClass="form-control form-control-sm" onkeypress="return isNumberKey(this, event);"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblrt" runat="server">Intrest Rate(%)</asp:Label>
                                        <asp:TextBox ID="txtrt" runat="server" CssClass="form-control form-control-sm" onkeypress="return isNumberKey(this, event);"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="lblEffDate" runat="server">Effective Date *

<<<<<<< HEAD
                                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Font-Size="8" Font-Italic="true" ForeColor="Red" ValidationGroup="one"
                                                                               ControlToValidate="txtEffDate" ErrorMessage="Required">
=======
                                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Font-Size="8" Font-Italic="true" ForeColor="Red"
                                                                               ControlToValidate="txtEffDate" ErrorMessage="Enter effective date">
>>>>>>> ca90ee60beedbcfdb94ecd8a9c145f1ffca7a952
                                                                           </asp:RequiredFieldValidator>
                                        </asp:Label>

                                        <asp:TextBox ID="txtEffDate" runat="server" CssClass="form-control form-control-sm  mr-2" ValidationGroup="one"></asp:TextBox>

                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtEffDate"></cc1:CalendarExtender>


                                    </div>


                                </div>

                                <div class="col-lg-3">

                                    <div class="form-group">
                                        <asp:Label ID="lblLoanAmt" runat="server">Loan Amount *
<<<<<<< HEAD
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Font-Size="8" Font-Italic="true" runat="server" ForeColor="Red" ValidationGroup="one"
                                                ControlToValidate="txtLoanAmt" ErrorMessage="Required">
=======
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Font-Size="8" Font-Italic="true" runat="server" ForeColor="Red"
                                                ControlToValidate="txtLoanAmt" ErrorMessage="Enter loan amount">
>>>>>>> ca90ee60beedbcfdb94ecd8a9c145f1ffca7a952
                                            </asp:RequiredFieldValidator>

                                        </asp:Label>
                                        <asp:TextBox ID="txtLoanAmt" runat="server" CssClass="form-control form-control-sm" onKeyUp="sum()" onkeypress="return isNumberKey(this, event);" ValidationGroup="one"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="lblPloanAmt" runat="server">Previous loan Amount</asp:Label>
                                        <asp:TextBox ID="txtPloanAmt" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="lblPFAmt" runat="server">Provident Fund</asp:Label>
                                        <asp:TextBox ID="txtPFAmt" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                    </div>


                                    <div class="form-group row">
                                        <asp:Label ID="lblLoanType" runat="server">Loan Type *</asp:Label>
                                        <asp:DropDownList ID="ddlLoanType" runat="server" CssClass="form-control form-control-sm">
                                        </asp:DropDownList>


                                    </div>
                                </div>

                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblInstNum" runat="server" CssClass="">Installment Number *

<<<<<<< HEAD
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="Red" ValidationGroup="one"
                                                                                        ControlToValidate="txtInstNum" ErrorMessage="Required" Font-Size="8" Font-Italic="true">
=======
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="Red"
                                                                                        ControlToValidate="txtInstNum" ErrorMessage="Enter inst number" Font-Size="8" Font-Italic="true">
>>>>>>> ca90ee60beedbcfdb94ecd8a9c145f1ffca7a952
                                                                                    </asp:RequiredFieldValidator>
                                        </asp:Label>
                                        <asp:TextBox ID="txtInstNum" runat="server" CssClass="form-control form-control-sm" onKeyUp="sum()" onkeypress="return isNumberKey(this, event);" ValidationGroup="four"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="lblGMS" runat="server">Gross Monthly Salary</asp:Label>
                                        <asp:TextBox ID="txtGMS" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                    </div>

                                    <div class="form-group ">
                                        <asp:Label ID="lblTax" runat="server">Income Tax</asp:Label>
                                        <asp:TextBox ID="txtTax" runat="server" CssClass="form-control form-control-sm" Enabled="false" onkeypress="return isNumberKey(this, event);"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="lblLoanDesc" runat="server">Purpose of loan</asp:Label>
                                        <asp:TextBox ID="txtLoanDescc" runat="server" CssClass="form-control form-control-sm" TextMode="MultiLine" Rows="3" Style="min-height: 70px;"></asp:TextBox>

                                    </div>

                                </div>

                                <div class="col-lg-3">

                                    <div class="form-group">
                                        <asp:Label ID="lblAmtPerIns" runat="server">Amount Per Installment</asp:Label>
                                        <asp:TextBox ID="txtAmtPerIns" runat="server" CssClass="form-control form-control-sm" Enabled="false" onkeypress="return isNumberKey(this, event);"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="lblOI" runat="server">Other Income</asp:Label>
                                        <asp:TextBox ID="txtOI" runat="server" CssClass="form-control form-control-sm" onkeypress="return isNumberKey(this, event);"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="lblOD" runat="server">Other Deduction</asp:Label>
                                        <asp:TextBox ID="txtOD" runat="server" CssClass="form-control form-control-sm" onkeypress="return isNumberKey(this, event);"></asp:TextBox>
                                    </div>
<<<<<<< HEAD


                                </div>
=======


                                </div>
                            </div>
                            <div class="row mt-1">
                                <div class="col-lg-3">
                                </div>
                                <div class="col-lg-3">
                                </div>
                                <div class="col-lg-3">
                                </div>
                                <div class="col-lg-3">
                                </div>
>>>>>>> ca90ee60beedbcfdb94ecd8a9c145f1ffca7a952
                            </div>

                            <div class="rowmt-2">
                                <div class="d-flex justify-content-center">
<<<<<<< HEAD

                                    <asp:LinkButton ID="lnkAdd" CssClass="btn btn-success btn-sm m-2 p2  bw-100" runat="server" OnClick="lnkAdd_Click" ValidationGroup="one">Save</asp:LinkButton>
                                    <asp:LinkButton ID="lnkUpdate" CssClass="btn btn-primary btn-sm m-2 p2  bw-100" runat="server" OnClick="lnkUpdate_Click" Visible="false"  ValidationGroup="one">Update</asp:LinkButton>
                                    <asp:LinkButton ID="lnkCancel" CssClass="btn btn-danger btn-sm m-2 p2  bw-100" runat="server" Visible="false" data-dismiss="modal"  ValidationGroup="one">Cancel</asp:LinkButton>
=======
                                    <asp:LinkButton ID="lnkAdd" CssClass="btn btn-success btn-sm m-2 p2  bw-100" runat="server" OnClick="lnkAdd_Click">Save</asp:LinkButton>
                                    <button class="btn btn-primary btn-sm p2 m-2 bw-100" type="submit" runat="server" visible="false">Update</button>
>>>>>>> ca90ee60beedbcfdb94ecd8a9c145f1ffca7a952

                                </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade" id="deleteModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLabel"> Are you sure, want to delete?</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                         <p class="text-center">
                             <asp:LinkButton runat="server" ID="confirmDelete" OnClick="confirmDelete_click" CssClass="btn btn-danger btn-sm bw-100">Yes, Delete</asp:LinkButton>
                             <p runat="server" id="delid" visible="false"></p>
                               <p runat="server" id="delempid" visible="false"></p>
                         </p>
                        </div>
                        
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

