<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptGrpDailyReportJq.aspx.cs" Inherits="RealERPWEB.F_46_GrMgtInter.RptGrpDailyReportJq" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../Content/jquery-ui.css" rel="stylesheet" />
    <script src="../Scripts_Own/S_46_GrMgtInter/rptGrpDaily.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <script src="../Scripts/Chart.js"></script>
    <script src="../Scripts/Chart.min.js"></script>
    <script src="../Scripts_Own/print.js"></script>
    <script>


        var comcode = '';
        //fotter & graph variables for A
        var tsalSum = 0;
        var tcollSum = 0;
        var trecSum = 0;
        var tpaySum = 0;
        var tcrecSum = 0;
        var tcisuSum = 0;

        //fotter & graph variables for B
        var tsaleamtSum = 0;
        var tosaleamtSum = 0;
        var acsaleamtSum = 0;
        var tdayamtSum = 0;
        var perotsaleSum = 0;

        //fotter & graph variables for C
        var tcollamtSum = 0;
        var tastcollamtSum = 0;
        var tdaycollamtSum = 0;
        var tdayamtSum = 0;
        var perotcolleSum = 0;

        //fotter & graph variables for D
        var acamtSum = 0;
        var reconamtSum = 0;
        var depchqSum = 0;
        var inhrchqSum = 0;
        var inhfchqSum = 0;
        var inhpchqSum = 0;
        var repchqSum = 0;
        var ncollamtSum = 0;

        //fotter & graph variables for E
        var lmrecamtSum = 0;
        var cmrecamtSum = 0;
        var otrecamtqSum = 0;
        var recpamSum = 0;
        var payamSum = 0;

        //fotter & graph variables for F
        var ainhfchqSum = 0;
        var ainhrchqSum = 0;
        var adepchqSum = 0;
        var arepchqSum = 0;
        var closbalSum = 0;
        var bankbalSum = 0;
        var ainhpchqSum = 0;
        var tavamtSum = 0;

        //fotter & graph variables for G
        var amt1Sum = 0;
        var amt2Sum = 0;
        var amt3Sum = 0;
        var amt4Sum = 0;
        var amt5Sum = 0;
        var amt6Sum = 0;
        var tamtSum = 0;

        //fotter & graph variables for H
        var recpamisSum = 0;
        var payamisSum = 0;

        //fotter & graph variables for I
        var mrramtSum = 0;
        var monplanSum = 0;
        var excutionSum = 0;

        //fotter & graph variables for J
        var revamtSum = 0;
        var usoldamtSum = 0;
        var soldamtSum = 0;
        var recamtSum = 0;
        var recabamtSum = 0;

        //fotter & graph variables for K
        var costamtSum = 0;
        var collamtSum = 0;
        var netamtSum = 0;

        //fotter & graph variables for L
        var curempnoSum = 0;
        var curpaySum = 0;

        var sdate = '';
        var endDate = '';

        //var url = "../ASMX_46_GrMgtInter/RptGrpMisDailyActiviteisWebService.asmx/PrintRpt";
        //var prntVal = 'PDF';


        $(document).ready(function () {
            //SetCompCode();
            HideLabels();
            $("#lbtnOk").click(function () {
                GetStartDate();
                GetEndDate();
                ShowData1();
                return false;

            });
            $('#txtDate').change(function () {
                GetStartDate();
            });
            $('#txttodate').change(function () {
                GetEndDate();
            });

        });

        function ShowData1() {
            StartProgressBar();
            $.ajax({
                type: "POST",
                async: true,
                url: "<%= this.ResolveUrl("~/F_46_GrMgtInter/RptGrpDailyReportJq.aspx/GetDailyGrpRpt")%>",
                contentType: "application/json;charset=utf-8",
                data: '{frdate: "' + sdate + '" ,  todate: "' + endDate + '"}',
                dataType: "json",

                //  data: Sys.Serialization.JavaScriptSerializer.serialize({ 'frdate': sdate, 'todate': endDate }),
                success: function onSuccess(data) {
                    console.log(data);
                    HideLabels();
                    displayTable(data);
                    CreateBarChart();
                    ShowLabels();
                    $("#pb").hide();

                }

            });


        }
    </script>
    <style>
        .gvTopHeader tr:nth-child(1) {
            height: 14px !important;
            font-size: 12px !important;
            font-weight: bold !important;
        }

        .grvContentarea > thead > tr > th, .grvContentarea > tbody > tr > th, .grvContentarea > tfoot > tr > th, .grvContentarea > thead > tr > td, .grvContentarea > tbody > tr > td, .grvContentarea > tfoot > tr > td {
            font-size: 10px !important;
            text-transform: capitalize !important;
        }

        .smLbl_to {
            color: green;
        }
    </style>
    <asp:CheckBox ID="chkGrp" runat="server" BackColor="Black" Font-Bold="True" Visible="false"
        Font-Size="12px" Style="color: Black; text-align: center; height: 20px;"
        TabIndex="4" Text="Graph" Width="80px" />

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card">
                <div class="card-header">
                    <div class="row">
                        <div class="col-md-4 p-0">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend ">
                                    <button class="btn btn-secondary" type="button">From</button>
                                </div>
                                <asp:TextBox ID="txtDate" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary" id="Button1" runat="server" type="button">To</button>
                                </div>
                                <asp:TextBox ID="txttodate" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="Cal3" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                <div class="input-group-prepend">
                                    <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" ClientIDMode="Static"> Show</asp:LinkButton>

                                </div>

                            </div>
                        </div>
                        <div class="col-md-6">

                            <div id="pb" style="width: 300px;"></div>


                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-8 sortable-tile">
                        <!-- .card -->
                        <section class="card card-fluid">
                            <header class="card-header drag-handle p-2 mb-1">A. TO-DAY'S ACHIEVEMENT </header>
                            <!-- .sortable-lists -->
                            <table id="grvToAch" border="1" class="table-striped table-hover table-bordered grvContentarea cmntbls">
                            </table>
                            <!-- /.sortable-lists -->
                        </section>
                        <!-- /.card -->
                    </div>
                    <div class="col-lg-4 sortable-tile">
                        <!-- .card -->
                        <section class="card card-fluid">
                            <header class="card-header drag-handle p-2 mb-1">Graph </header>
                            <!-- .sortable-lists -->
                            <canvas id="ToAchbrchrt" width="236" height="150"></canvas>

                            <!-- /.sortable-lists -->
                        </section>
                        <!-- /.card -->
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-8 sortable-tile">
                        <!-- .card -->
                        <section class="card card-fluid">
                            <header class="card-header drag-handle p-2 mb-1">B. SALES STATUS</header>
                            <!-- .sortable-lists -->
                            <table id="gvDayWSale" border="1" class="table-striped table-hover table-bordered grvContentarea cmntbls">
                            </table>
                            <!-- /.sortable-lists -->
                        </section>
                        <!-- /.card -->
                    </div>
                    <div class="col-lg-4 sortable-tile">
                        <!-- .card -->
                        <section class="card card-fluid">
                            <header class="card-header drag-handle p-2 mb-1">Graph </header>
                            <!-- .sortable-lists -->
                            <canvas id="gvDayWSalebrchrt" width="236" height="150"></canvas>

                            <!-- /.sortable-lists -->
                        </section>
                        <!-- /.card -->
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-8 sortable-tile">
                        <!-- .card -->
                        <section class="card card-fluid">
                            <header class="card-header drag-handle p-2 mb-1">C. COLLECTION AGAINST SALES</header>
                            <!-- .sortable-lists -->
                            <table id="gvCollSt" border="1" class="table-striped table-hover table-bordered grvContentarea cmntbls">
                            </table>
                            <!-- /.sortable-lists -->
                        </section>
                        <!-- /.card -->
                    </div>
                    <div class="col-lg-4 sortable-tile">
                        <!-- .card -->
                        <section class="card card-fluid">
                            <header class="card-header drag-handle p-2 mb-1">Graph </header>
                            <!-- .sortable-lists -->
                            <canvas id="gvCollStbrchrt" width="236" height="150"></canvas>

                            <!-- /.sortable-lists -->
                        </section>
                        <!-- /.card -->
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-8 sortable-tile">
                        <!-- .card -->
                        <section class="card card-fluid">
                            <header class="card-header drag-handle p-2 mb-1">D. COLLECTION BREAK DOWN OF C</header>
                            <!-- .sortable-lists -->
                            <table id="gvrcoll" border="1" class="table-striped table-hover table-bordered grvContentarea cmntbls">
                            </table>
                            <!-- /.sortable-lists -->
                        </section>
                        <!-- /.card -->
                    </div>
                    <div class="col-lg-4 sortable-tile">
                        <!-- .card -->
                        <section class="card card-fluid">
                            <header class="card-header drag-handle p-2 mb-1">Graph </header>
                            <!-- .sortable-lists -->
                            <canvas id="gvrcollbrchrt" width="236" height="200"></canvas>

                            <!-- /.sortable-lists -->
                        </section>
                        <!-- /.card -->
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-8 sortable-tile">
                        <!-- .card -->
                        <section class="card card-fluid">
                            <header class="card-header drag-handle p-2 mb-1">E. RECEIPTS & PAYMENTS</header>
                            <!-- .sortable-lists -->
                            <table id="grvRecPay" border="1" class="table-striped table-hover table-bordered grvContentarea cmntbls">
                            </table>
                            <!-- /.sortable-lists -->
                        </section>
                        <!-- /.card -->
                    </div>
                    <div class="col-lg-4 sortable-tile">
                        <!-- .card -->
                        <section class="card card-fluid">
                            <header class="card-header drag-handle p-2 mb-1">Graph </header>
                            <!-- .sortable-lists -->
                            <canvas id="grvRecPaytbrchrt" width="236" height="150"></canvas>

                            <!-- /.sortable-lists -->
                        </section>
                        <!-- /.card -->
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-8 sortable-tile">
                        <!-- .card -->
                        <section class="card card-fluid">
                            <header class="card-header drag-handle p-2 mb-1">F. AVAILABLE FUND STATUS</header>
                            <!-- .sortable-lists -->
                            <table id="grvAvFund" border="1" class="table-striped table-hover table-bordered grvContentarea cmntbls">
                            </table>
                            <!-- /.sortable-lists -->
                        </section>
                        <!-- /.card -->
                    </div>
                    <div class="col-lg-4 sortable-tile">
                        <!-- .card -->
                        <section class="card card-fluid">
                            <header class="card-header drag-handle p-2 mb-1">Graph </header>
                            <!-- .sortable-lists -->
                            <canvas id="grvAvFundbrchrt" width="236" height="200"></canvas>

                            <!-- /.sortable-lists -->
                        </section>
                        <!-- /.card -->
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-8 sortable-tile">
                        <!-- .card -->
                        <section class="card card-fluid">
                            <header class="card-header drag-handle p-2 mb-1">G. PAYMENTS DURING THE PERIOD</header>
                            <!-- .sortable-lists -->
                            <table id="gvChqIsu" border="1" class="table-striped table-hover table-bordered grvContentarea cmntbls">
                            </table>
                            <!-- /.sortable-lists -->
                        </section>
                        <!-- /.card -->
                    </div>
                    <div class="col-lg-4 sortable-tile">
                        <!-- .card -->
                        <section class="card card-fluid">
                            <header class="card-header drag-handle p-2 mb-1">Graph </header>
                            <!-- .sortable-lists -->
                            <canvas id="gvChqIsubrchrt" width="236" height="200"></canvas>

                            <!-- /.sortable-lists -->
                        </section>
                        <!-- /.card -->
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-8 sortable-tile">
                        <!-- .card -->
                        <section class="card card-fluid">
                            <header class="card-header drag-handle p-2 mb-1">H. CHEQUED RECEIPTS & ISSUED</header>
                            <!-- .sortable-lists -->
                            <table id="gvRecPayiss" border="1" class="table-striped table-hover table-bordered grvContentarea cmntbls">
                            </table>
                            <!-- /.sortable-lists -->
                        </section>
                        <!-- /.card -->
                    </div>
                    <div class="col-lg-4 sortable-tile">
                        <!-- .card -->
                        <section class="card card-fluid">
                            <header class="card-header drag-handle p-2 mb-1">Graph </header>
                            <!-- .sortable-lists -->
                            <canvas id="gvRecPayissbrchrt" width="236" height="200"></canvas>

                            <!-- /.sortable-lists -->
                        </section>
                        <!-- /.card -->
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-8 sortable-tile">
                        <!-- .card -->
                        <section class="card card-fluid">
                            <header class="card-header drag-handle p-2 mb-1">I. PROCUREMENT & CONSTRACTION</header>
                            <!-- .sortable-lists -->
                            <table id="gvprocure" border="1" class="table-striped table-hover table-bordered grvContentarea cmntbls">
                            </table>
                            <!-- /.sortable-lists -->
                        </section>
                        <!-- /.card -->
                    </div>
                    <div class="col-lg-4 sortable-tile">
                        <!-- .card -->
                        <section class="card card-fluid">
                            <header class="card-header drag-handle p-2 mb-1">Graph </header>
                            <!-- .sortable-lists -->
                            <canvas id="gvprocurebrchrt" width="236" height="200"></canvas>

                            <!-- /.sortable-lists -->
                        </section>
                        <!-- /.card -->
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-8 sortable-tile">
                        <!-- .card -->
                        <section class="card card-fluid">
                            <header class="card-header drag-handle p-2 mb-1">J. STOCK, UNSOLD, SOLD, RECEIVED & RECEIVABLE</header>
                            <!-- .sortable-lists -->
                            <table id="gvpstk" border="1" class="table-striped table-hover table-bordered grvContentarea cmntbls">
                            </table>
                            <!-- /.sortable-lists -->
                        </section>
                        <!-- /.card -->
                    </div>
                    <div class="col-lg-4 sortable-tile">
                        <!-- .card -->
                        <section class="card card-fluid">
                            <header class="card-header drag-handle p-2 mb-1">Graph </header>
                            <!-- .sortable-lists -->
                            <canvas id="gvpstkbrchrt" width="236" height="200"></canvas>

                            <!-- /.sortable-lists -->
                        </section>
                        <!-- /.card -->
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-8 sortable-tile">
                        <!-- .card -->
                        <section class="card card-fluid">
                            <header class="card-header drag-handle p-2 mb-1">K. PROJECT STATUS (DURING THE PERIOD)</header>
                            <!-- .sortable-lists -->
                            <table id="gvmonprost" border="1" class="table-striped table-hover table-bordered grvContentarea cmntbls">
                            </table>
                            <!-- /.sortable-lists -->
                        </section>
                        <!-- /.card -->
                    </div>
                    <div class="col-lg-4 sortable-tile">
                        <!-- .card -->
                        <section class="card card-fluid">
                            <header class="card-header drag-handle p-2 mb-1">Graph </header>
                            <!-- .sortable-lists -->
                            <canvas id="gvmonprostbrchrt" width="236" height="200"></canvas>

                            <!-- /.sortable-lists -->
                        </section>
                        <!-- /.card -->
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-8 sortable-tile">
                        <!-- .card -->
                        <section class="card card-fluid">
                            <header class="card-header drag-handle p-2 mb-1">L. HR MANAGEMENT</header>
                            <!-- .sortable-lists -->
                            <table id="gvHremp" border="1" class="table-striped table-hover table-bordered grvContentarea cmntbls">
                            </table>
                            <!-- /.sortable-lists -->
                        </section>
                        <!-- /.card -->
                    </div>
                    <div class="col-lg-4 sortable-tile">
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

