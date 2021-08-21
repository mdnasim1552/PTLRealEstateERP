<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="DashBoardAll.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.DashBoardAll" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script>
        var Records = new Array(500);
        var grphRecords = new Array(500);
        var tblLen = 0;
        var fromDate = "";
        var salSum = 0;
        var colSum = 0;

        var purSum = 0;
        var paySum = 0;

        var resSum = 0;
        var paymentSum = 0;

        var comcod = "";

        $(this).load(function () {
            comcod = '<%= this.GetCompCode()%>';
            pageSize = $("#pagePerItem option:selected").val();
            fromDate = $("#txtDatefrom").val();
            hideGrpLbls();

        });
    </script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });
        }
    </script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" />
    <script src="../Scripts_Own/S_34_Mgt/DashBoardAll.js"></script>
    <script src="../Scripts/Chart.js"></script>
    <script src="../Scripts/Chart.min.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <style>
        .gvTopHeader tr:nth-child(1) {
            height: 14px !important;
            font-size: 12px !important;
            font-weight: bold !important;
        }
    </style>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">

                    <div class="row">

                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lbldatefrm" runat="server" CssClass=" lblTxt lblName" Text="Date"></asp:Label>
                                        <asp:TextBox ID="txtDatefrom" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" onchange="javascript:SelectFromDate();" ClientIDMode="Static"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDatefrom_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDatefrom" Enabled="true"></cc1:CalendarExtender>
                                        .

                                      
                                    </div>
                                    <input type="button" id="okbtn" onclick="javascript: GetMonthlyInfo();" value="OK" class="btn btn-primary okBtn" />
                                    <div id="pb" style="width: 300px;"></div>
                                    <div class="col-md-3 pading5px asitCol3">
                                    </div>

                                </div>



                            </div>
                        </fieldset>
                    </div>


                </div>
                <hr class="hrline" />
                <div class="row">
                    <div class="col-sm-4 col-md-4 col-lg-4">

                        <p id="lblMonSalCol" class="GrpHeader" style="margin-left: 15px; margin-bottom: 0; width: 330px;">A. MONTHLY SALES & COLLECTION</p>
                        <div class="col-sm-12">

                            <table id="grvMonthlySales" class="table-striped table-hover table-bordered grvContentarea" style="width: 100%; font-family: Cambria;"></table>

                        </div>

                    </div>
                    <div class="col-sm-8 col-md-8 col-lg-8">
                        <div id="saleCanvDiv">
                            <canvas id="salesbrchrt" width="750" height="290"></canvas>
                        </div>

                    </div>
                    <div class="col-xs-1 col-md-1 col-lg-1">
                        <a id="lblSalesNext" class="btn btn-primary primaryBtn" href='<%=this.ResolveUrl("~/GenPage.aspx?Type=15")%>' target="_blank" style="margin: 10px 0 0 5px; line-height: 18px;">NEXT</a>

                        
                    </div>
                </div>
                <hr class="hrline" />
                <div class="row">
                    <div class="col-sm-4 col-md-4 col-lg-4">
                        <p id="lblMonPurPay" class="GrpHeader" style="margin-left: 15px; margin-bottom: 0; width: 330px;">B. MONTHLY PURCHASE & PAYMENT</p>
                        <div class="col-sm-12">
                            <table id="grvMonthlyPurcse" class="table-striped table-hover table-bordered grvContentarea" style="width: 100%; font-family: Cambria;"></table>

                        </div>

                    </div>
                    <div class="col-sm-8 col-md-8 col-lg-8">
                        <div id="purCanvDiv">
                            <canvas id="purchasechrt" width="750" height="290"></canvas>
                        </div>

                    </div>
                    <div class="col-xs-1 col-md-1 col-lg-1">
                        <a id="lblPurNext" class="btn btn-primary primaryBtn" href='<%=this.ResolveUrl("~/GenPage.aspx?Type=10")%>' target="_blank" style="margin: 10px 0 0 5px; line-height: 18px;">NEXT</a>

                    </div>
                </div>
                <hr class="hrline" />
                <div class="row">
                    <div class="col-sm-4 col-md-4 col-lg-4">
                        <p id="lblMonResPay" class="GrpHeader" style="margin-left: 15px; margin-bottom: 0; width: 330px;">C. MONTHLY RECEIPT & PAYMENT</p>
                        <div class="col-sm-12">
                            <table id="grvMonthlyAcc" class="table-striped table-hover table-bordered grvContentarea" style="width: 100%; font-family: Cambria;"></table>

                        </div>

                    </div>
                    <div class="col-sm-8 col-md-8 col-lg-8">
                        <div id="accCanvDiv">
                            <canvas id="accchrt" width="750" height="290"></canvas>
                        </div>

                    </div>
                    <div class="col-xs-1 col-md-1 col-lg-1">
                        <a id="lblAccNext" class="btn btn-primary primaryBtn" href='<%=this.ResolveUrl("~/GenPage.aspx?Type=12")%>' target="_blank" style="margin: 10px 0 0 5px; line-height: 18px;">NEXT</a>

                    </div>
                </div>

                 <div class="row">
                    <div class="col-sm-4 col-md-4 col-lg-4">
                        <p id="lblMonTarEx" class="GrpHeader" style="margin-left: 15px; margin-bottom: 0; width: 330px;">D. MONTHLY TARGET & EXECUTION</p>
                        <div class="col-sm-12">
                            <table id="grvMonTarEx" class="table-striped table-hover table-bordered grvContentarea" style="width: 100%; font-family: Cambria;"></table>

                        </div>

                    </div>
                    <div class="col-sm-8 col-md-8 col-lg-8">
                        <div id="TarExCanvDiv">
                            <canvas id="TarExchrt" width="750" height="290"></canvas>
                        </div>

                    </div>
                    <div class="col-xs-1 col-md-1 col-lg-1">
                        <a id="lblMonTarExNext" class="btn btn-primary primaryBtn" href='<%=this.ResolveUrl("~/GenPage.aspx?Type=07")%>' target="_blank" style="margin: 10px 0 0 5px; line-height: 18px;">NEXT</a>

                    </div>
                </div>



            </div>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

