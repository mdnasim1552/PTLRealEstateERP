<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptGrpDailyReportJq.aspx.cs" Inherits="RealERPWEB.F_46_GrMgtInter.RptGrpDailyReportJq" %>

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
        var tdaycollamtSum= 0;
        var tdayamtSum= 0;
        var perotcolleSum = 0;

        //fotter & graph variables for D
        var acamtSum= 0;
        var reconamtSum= 0;
        var depchqSum= 0;
        var inhrchqSum= 0;
        var inhfchqSum= 0;
        var inhpchqSum= 0;
        var repchqSum= 0;
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
        var amt1Sum=0;
        var amt2Sum=0;
        var amt3Sum=0;
        var amt4Sum=0;
        var amt5Sum=0;
        var amt6Sum=0;
        var tamtSum =0;
        
        //fotter & graph variables for H
        var recpamisSum=0;
        var payamisSum = 0;

        //fotter & graph variables for I
        var mrramtSum=0;
        var monplanSum = 0;
        var excutionSum = 0;

        //fotter & graph variables for J
        var revamtSum=0;
        var usoldamtSum=0;
        var soldamtSum=0;
        var recamtSum=0;
        var recabamtSum = 0;

        //fotter & graph variables for K
        var costamtSum=0;
        var collamtSum=0;
        var netamtSum = 0;

        //fotter & graph variables for L
        var curempnoSum=0;
        var curpaySum = 0;

        var sdate ='';
        var endDate = '';

        var url = "../ASMX_46_GrMgtInter/RptGrpMisDailyActiviteisWebService.asmx/PrintRpt";
        var prntVal = 'PDF';


        $(document).ready(function () {
            //SetCompCode();
            HideLabels();
            $("#lbtnOk").click(function () {
                GetStartDate();
                GetEndDate();
                ShowData();
                return false;
                
            });
            $('#txtDate').change(function () {      
                GetStartDate();
            });
            $('#txttodate').change(function () {
                GetEndDate();
            });

            $("[id$=lnkPrint]").click(function () {
                
                PrintAction(url, prntVal);
                return false;

            });


            $("[id$=DDPrintOpt]").change(function () {
                prntVal = ($("[id$=DDPrintOpt]").val());
                //alert(prntVal);
                return false;

            });

            });
    </script>
    <style>
        .gvTopHeader tr:nth-child(1) {
            height: 14px !important;
            font-size: 12px !important;
            font-weight: bold !important;
        }
        .grvContentarea > thead > tr > th, .grvContentarea > tbody > tr > th, .grvContentarea > tfoot > tr > th, .grvContentarea > thead > tr > td, .grvContentarea > tbody > tr > td, .grvContentarea > tfoot > tr > td {
             font-size: 10px !important;
             text-transform:capitalize !important;
        }
        .smLbl_to {
            color:green;
        }
    </style>
    <asp:CheckBox ID="chkGrp" runat="server" BackColor="Black" Font-Bold="True" Visible="false"
        Font-Size="12px" Style="color: Black; text-align: center; height: 20px;"
        TabIndex="4" Text="Graph" Width="80px" />

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <asp:Panel ID="Panel2" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-7 pading5px asitCol7">
                                            <asp:Label ID="lblDatefrom" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                            <asp:TextBox ID="txtDate" runat="server" CssClass="inputtextbox " ClientIDMode="Static"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtDate" Enabled="true"></cc1:CalendarExtender>


                                            <asp:Label ID="lblDateTo" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                            <asp:TextBox ID="txttodate" runat="server" CssClass="inputtextbox" ClientIDMode="Static"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txttodate" Enabled="true"></cc1:CalendarExtender>
                                            <asp:LinkButton ID="lbtnOk" runat="server"  CssClass="btn btn-primary primaryBtn" ClientIDMode="Static">Show</asp:LinkButton>
                                            <div id="pb" style="width: 300px;"></div>
                                        </div>
                                    </div>
                                </asp:Panel>


                            </div>
                        </fieldset>
                    </div>


                    <fieldset>
                        <asp:Label ID="lblToDayAc" runat="server"
                            Text="A. TO-DAY'S ACHIEVEMENT"  CssClass="smLbl_to" ClientIDMode="Static"></asp:Label>
                    </fieldset>
                    <div class="row">
                        <div class=" col-md-8 col-sm-8 col-lg-8">
                            <table id="grvToAch"  border="1" style="width:740px;" class="table-striped table-hover table-bordered grvContentarea cmntbls">
                                
                            </table>
                        </div>
                        <div class=" col-md-4 col-sm-4 col-lg-4" id="grvToAchDiv">
                           
                            <canvas id="ToAchbrchrt" width="236" height="200"></canvas>
                       
                        </div>
                    </div>


                    <fieldset>
                        <asp:Label ID="lblSales" runat="server" CssClass="smLbl_to" Text="B. SALES STATUS" ClientIDMode="Static"></asp:Label>
                    </fieldset>
                    <div class="row">
                        <div class=" col-md-8 col-sm-8 col-lg-8">
                            <table id="gvDayWSale" border="1" style="width:740px;" class="table-striped table-hover table-bordered grvContentarea cmntbls">
                                
                            </table>
                        </div>
                        <div class=" col-md-4 col-sm-4 col-lg-4" id="gvDayWSaleDiv">
                            <canvas id="gvDayWSalebrchrt" width="236" height="200"></canvas>
                        </div>
                    </div>

                    <fieldset>
                        <asp:Label ID="lblColl" runat="server" CssClass="smLbl_to" Text="C. COLLECTION AGAINST SALES" ClientIDMode="Static"></asp:Label>
                    </fieldset>

                    <div class="row">
                        <div class=" col-md-8 col-sm-8 col-lg-8">
                            <table id="gvCollSt" border="1" style="width:740px;" class="table-striped table-hover table-bordered grvContentarea cmntbls">
                                
                            </table>
                        </div>
                        <div class=" col-md-4 col-sm-4 col-lg-4" id="gvCollStbrchrtDiv">
                            <canvas id="gvCollStbrchrt" width="236" height="200"></canvas>
                        </div>
                    </div>

                    <fieldset>
                        <asp:Label ID="lblCollBrk" runat="server" CssClass="smLbl_to" Text="D. COLLECTION BREAK DOWN OF C" ClientIDMode="Static"></asp:Label>
                    </fieldset>

                    <div class="row">
                        <div class=" col-md-8 col-sm-8 col-lg-8">
                            <table id="gvrcoll" border="1" style="width:740px;"  class="table-striped table-hover table-bordered grvContentarea cmntbls">
                                
                            </table>
                        </div>
                        <div class=" col-md-4 col-sm-4 col-lg-4" id="gvrcollDiv">
                           <canvas id="gvrcollbrchrt" width="236" height="200"></canvas>
                        </div>
                    </div>

                    <fieldset>
                        <asp:Label ID="lblRecPay" runat="server" CssClass="smLbl_to" Text="E. RECEIPTS & PAYMENTS" ClientIDMode="Static"></asp:Label>
                    </fieldset>

                    <div class="row">
                        <div class=" col-md-8 col-sm-8 col-lg-8">
                            <table id="grvRecPay" border="1" style="width:740px;" class="table-striped table-hover table-bordered grvContentarea cmntbls">
                                
                            </table>
                        </div>
                        <div class=" col-md-4 col-sm-4 col-lg-4" id="grvRecPaytDiv">
                           <canvas id="grvRecPaytbrchrt" width="236" height="200"></canvas>
                        </div>
                    </div>
                    <fieldset>
                        <asp:Label ID="lblAvFund" runat="server" CssClass="smLbl_to" Text="F. AVAILABLE FUND STATUS" ClientIDMode="Static"></asp:Label>
                    </fieldset>

                    <div class="row">
                        <div class=" col-md-8 col-sm-8 col-lg-8">
                            <table id="grvAvFund" border="1" style="width:100%;" class="table-striped table-hover table-bordered grvContentarea cmntbls">
                                
                            </table>
                        </div>
                        <div class=" col-md-4 col-sm-4 col-lg-4" id="grvAvFundiv">
                            <canvas id="grvAvFundbrchrt" width="236" height="200"></canvas>
                           
                        </div>
                    </div>
                    <fieldset>
                        <asp:Label ID="lblChqisu" runat="server" CssClass="smLbl_to" Text="G. PAYMENTS DURING THE PERIOD" ClientIDMode="Static"></asp:Label>
                    </fieldset>

                    <div class="row">
                        <div class=" col-md-8 col-sm-8 col-lg-8">
                            <table id="gvChqIsu" border="1" style="width:100%;" class="table-striped table-hover table-bordered grvContentarea cmntbls">
                                
                            </table>
                        </div>
                        <div class=" col-md-4 col-sm-4 col-lg-4" id="gvChqIsuDiv">
                            <canvas id="gvChqIsubrchrt"  width="236" height="200"></canvas>
                           
                        </div>
                    </div>

                    <fieldset>
                        <asp:Label ID="lblRecPayiss" runat="server" CssClass="smLbl_to" Text="H. CHEQUED RECEIPTS & ISSUED" ClientIDMode="Static"></asp:Label>
                    </fieldset>

                    <div class="row">
                        <div class=" col-md-8 col-sm-8 col-lg-8">
                            <table id="gvRecPayiss" border="1" style="width:340px;" class="table-striped table-hover table-bordered grvContentarea cmntbls">
                                
                            </table>
                        </div>
                        <div class=" col-md-4 col-sm-4 col-lg-4" id="gvRecPayissDiv">
                           <canvas id="gvRecPayissbrchrt"  width="236" height="200"></canvas>
                        </div>
                    </div>

                    <fieldset>
                        <asp:Label ID="lblProcurement" runat="server" CssClass="smLbl_to" Text="I. PROCUREMENT & CONSTRACTION" ClientIDMode="Static"></asp:Label>
                    </fieldset>

                    <div class="row">
                        <div class=" col-md-8 col-sm-8 col-lg-8">
                            <table id="gvprocure" border="1" style="width:440px;" class="table-striped table-hover table-bordered grvContentarea cmntbls">
                                
                            </table>
                           
                        </div>
                        <div class=" col-md-4 col-sm-4 col-lg-4" id="gvprocureDiv">
                            <canvas id="gvprocurebrchrt"  width="236" height="200"></canvas>
                           
                        </div>
                    </div>
                    <fieldset>
                        <asp:Label ID="lblStock" runat="server" CssClass="smLbl_to" Text="J. STOCK, UNSOLD, SOLD, RECEIVED & RECEIVABLE" ClientIDMode="Static"></asp:Label>
                    </fieldset>

                    <div class="row">
                        <div class=" col-md-8 col-sm-8 col-lg-8">
                             <table id="gvpstk" border="1" style="width:100%;" class="table-striped table-hover table-bordered grvContentarea cmntbls">
                                
                            </table>
                        </div>
                        <div class=" col-md-4 col-sm-4 col-lg-4" id="gvpstkDiv">
                            
                           <canvas id="gvpstkbrchrt"  width="236" height="200"></canvas>
                        </div>
                    </div>
                    <fieldset>
                        <asp:Label ID="lblMonProStatus" runat="server" CssClass="smLbl_to" Text="K. PROJECT STATUS (DURING THE PERIOD)" ClientIDMode="Static"></asp:Label>
                    </fieldset>

                    <div class="row">
                        <div class=" col-md-8 col-sm-8 col-lg-8">
                            <table id="gvmonprost" border="1" style="width:440px;" class="table-striped table-hover table-bordered grvContentarea cmntbls">
                                
                            </table>
                        </div>
                        <div class=" col-md-4 col-sm-4 col-lg-4" id="gvmonprostDiv">
                            
                           <canvas id="gvmonprostbrchrt"  width="236" height="200"></canvas>
                        </div>
                    </div>
                    <fieldset class="hidden">
                        <asp:Label ID="lblHrMgt" runat="server" CssClass="smLbl_to" Text="L. HR MANAGEMENT" ClientIDMode="Static"></asp:Label>
                    </fieldset>

                    <div class="row hidden">
                        <div class=" col-md-8 col-sm-8 col-lg-8">
                            <table id="gvHremp" border="1" style="width:340px;" class="table-striped table-hover table-bordered grvContentarea cmntbls">
                                
                            </table>
                        </div>
                        <div class=" col-md-4 col-sm-4 col-lg-4">
                            
                           
                        </div>
                       
                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

