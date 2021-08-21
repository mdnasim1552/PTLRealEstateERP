<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptGrpMisDailyActiviteisJq.aspx.cs" Inherits="RealERPWEB.F_45_GrAcc.RptGrpMisDailyActiviteisJq" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../Content/jquery-ui.css" rel="stylesheet" />
    <script src="../Scripts_Own/S_45_GrAcc/RptGrpMisDailyActiviteis.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <script src="../Scripts/Chart.js"></script>
    <script src="../Scripts/Chart.min.js"></script>
    <script src="../Scripts_Own/print.js"></script>
    <script>

        var consolidateCB = '';
        var noOfCompany = '';
        var sdate = '';
        var endDate = '';
        var comnams = '';

        //A foot& graph
        var capacitySum = 0;
        var masbgdSum = 0;
        var bepSum = 0;
        var tsaleamtSum = 0;
        var tosaleamtSum = 0;
        var suamtSum = 0;
        var perotsalSum = 0;

        //B foot& graph
        var capacitySumB = 0;
        var masbgdSumB = 0;
        var bepSumB = 0;
        var tcollamtSumB = 0;
        var tastcollamtSumB = 0;
        var acamtSumB = 0;
        var perotcollSumB = 0;

        //C foot& graph
        var inhrchqSum = 0;
        var inhfchqSum = 0;
        var inhpchqSum = 0;
        var chqinhandSum = 0;

        //D foot& graph
        var recpamSum = 0;
        var payamSum = 0;
        var inhpchqSum = 0;
        var balamSum = 0;

        //E foot& graph
        var closbalSum = 0;
        var closliaSum = 0;
        var avloanSum = 0;

        //F foot& graph
        var recpamSumF = 0;
        var payamSumCF = 0;
        var balamSumF = 0;

        //G foot& graph
        var toamSumG = 0;
        var payamSumCG = 0;
        var pdcamSumG = 0;

        //H foot& graph
        var reqamtSumH = 0;
        var mrramtSumCH = 0;
        var billamtSumH = 0;

        //I foot& graph
        var masplanSumI = 0;
        var monplanSumCI = 0;
        var excutionSumI = 0;
        var peromaspSumI = 0;
        var peromonpSumI = 0;

        //M foot& graph
        var toamSumM = 0;
        var unsoldamSumCM = 0;
        var soldamSumM = 0;
        var ramtSumM = 0;
        var atoduesSumM = 0;
        var bamtSumM = 0;
        var ptoduesSumM = 0;
        var ctoduesSumM = 0;

        //N foot& graph
        var salamtSumM = 0;
        var trecamtSumCM = 0;
        var netexpamtSumM = 0;
        var liaamtSumM = 0;
        var netlnamtSumM = 0;

        //O foot& graph
        var costamtSumO = 0;
        var collamtSumCO = 0;
        var netamtSumO = 0;

        //P foot& graph
        var issuebasisSumP = 0;
        var probasisSumP = 0;
        var matconSumP = 0;

        //R FOOT& GRAPH
        var toamSumR = 0;

        //T FOOT& GRAPH
        var toempSumT = 0;
        var netpaySumT = 0;


        var url = "../ASMX_F_45_GrAcc/RptGrpMisDailyActiviteis.asmx/PrintRpt";
        var prntVal = 'PDF';

        $('document').ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            HideLabels();
            GetStartDate();
            GetEndDate();

            $('#txtDate').change(function () {
                GetStartDate();
            });

            $('#txttodate').change(function () {
                GetEndDate();
            });

            OnConsolidateChng();

            $('#chkConsolidate').click(function () {
                if (this.checked == true)
                    consolidateCB = 'Consolidate'
                else
                    consolidateCB = ''

                OnConsolidateChng();
            });

            $('#chkall').click(function () {
                if (this.checked)
                    CheckAllComp();
                else
                    UnCheckAllComp();
            });


            $('#lbtnOk').click(function () {
                GetSelectedCompanies();
                LoadInfo();
                return false;
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
        function pageLoaded() {

            try {

                $(function () {
                    $('[id*=ddlComCode]').multiselect({
                        includeSelectAllOption: true
                    });

                });

               
            } catch (e) {
                alert(e.message);
            }
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_B">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-2 pading5px">
                                        <asp:Label ID="Label15" runat="server" CssClass="lblTxt lblName" Text="Date "></asp:Label>
                                        <asp:TextBox ID="txtDate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" ClientIDMode="Static"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDate" Enabled="true"></cc1:CalendarExtender>


                                        <asp:Label ID="lbltodate" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" ClientIDMode="Static"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate" Enabled="true"></cc1:CalendarExtender>


                                        </div>
                                      <div class="col-md-3 pading5px">
                                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="Company Name"></asp:Label>
                                            <asp:ListBox ID="ddlComCode" runat="server" CssClass=" form-control" Width="280px"  SelectionMode="Multiple"></asp:ListBox>
                                            
                                           <%-- <asp:CheckBox ID="chkall" runat="server" AutoPostBack="True"
                                                CssClass="chkBoxControl btn btn-primary primaryBtn margin5px"
                                                OnCheckedChanged="chkall_CheckedChanged" Text="Check All" />--%>

                                            
                                        </div>

                                     <div class="col-md-4 pading5px">

                                        <div class="colMdbtn pading5px">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click" ClientIDMode="Static">OK</asp:LinkButton>

                                        </div>

                                    </div>

                                    <div id="pb" class="col-md-5 padingpx"></div>



                                </div>


                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <div class="form-group">

                            <div class="col-md-3 padingpx asitCol3">
                                <asp:Label ID="Label12" runat="server" CssClass="smLbl_to" Text="Company Name:"></asp:Label>
                                <asp:CheckBox ID="chkall" runat="server" Text="Check All" ClientIDMode="Static" />
                            </div>
                            <div class="col-md-3 padingpx asitCol3">
                            </div>
                            <div class="col-md-3 padingpx asitCol3">
                                <asp:CheckBox ID="chkConsolidate" runat="server" Text="With Consolidate" ClientIDMode="Static" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_B">
                            <div class="form-group">
                                <div class="col-md-1">
                                </div>
                                <div class="col-md-11">

                                    <asp:CheckBoxList ID="cblCompany" runat="server" CellPadding="2" CellSpacing="0" CssClass="StyleCheckBoxList"
                                        Font-Bold="True" Font-Size="12px" ForeColor="Black" Height="12px" RepeatColumns="4"
                                        RepeatDirection="Horizontal" Width="1000px" TextAlign="Right" ClientIDMode="Static">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <fieldset>
                            <asp:Label ID="lblSales" runat="server" CssClass="smLbl_to" Text="A. Sales" ClientIDMode="Static"></asp:Label>
                        </fieldset>
                        <div class=" col-md-12 col-sm-12 col-lg-12">
                            <table id="gvDayWSale" border="1" class=" table-striped table-hover table-bordered grvContentarea gvTopHeader"></table>
                        </div>
                        
                    </div>



                    <div class="row">
                        <fieldset>
                            <asp:Label ID="lblCollectionStatus" runat="server" CssClass="smLbl_to" Text="B. Collection Status" ClientIDMode="Static"></asp:Label>
                        </fieldset>

                        <div class=" col-md-12 col-sm-12 col-lg-12">
                            <table id="gvrcoll" border="1" class=" table-striped table-hover table-bordered grvContentarea gvTopHeader"></table>
                        </div>
                       
                    </div>
                    <div class="row">
                        <fieldset>
                            <asp:Label ID="lblChequeInHand" runat="server" CssClass="smLbl_to" Text="C. Cheque In Hand" ClientIDMode="Static"></asp:Label>
                        </fieldset>

                        <div class=" col-md-12 col-sm-12 col-lg-12">
                            <table id="gvchequeinhand" border="1" class=" table-striped table-hover table-bordered grvContentarea gvTopHeader"></table>
                        </div>
                        
                    </div>

                    <div class="row">
                        <fieldset>
                            <asp:Label ID="lblRecAPayEncash" runat="server" CssClass="smLbl_to" Text="D. Receipt &amp; Payment(Encashment Basis)" ClientIDMode="Static"></asp:Label>
                        </fieldset>

                        <div class=" col-md-12 col-sm-12 col-lg-12">
                            <table id="gvarecandpay" border="1" class=" table-striped table-hover table-bordered grvContentarea gvTopHeader"></table>
                        </div>
                        
                    </div>

                    <div class="row">
                        <fieldset>
                            <asp:Label ID="lblBankPosition" runat="server" CssClass="smLbl_to" Text="E. Bank Position" ClientIDMode="Static"></asp:Label>
                        </fieldset>

                        <div class=" col-md-12 col-sm-12 col-lg-12">
                            <table id="gvBankPosition" border="1" class=" table-striped table-hover table-bordered grvContentarea gvTopHeader"></table>
                        </div>
                       
                    </div>

                    <div class="row">
                        <fieldset>
                            <asp:Label ID="lblRecAPayIssue" runat="server" CssClass="smLbl_to" Text="F. Receipt &amp; Payment(Issue Basis)" ClientIDMode="Static"></asp:Label>
                        </fieldset>

                        <div class=" col-md-12 col-sm-12 col-lg-12">
                            <table id="gvarecandpayis" border="1" class=" table-striped table-hover table-bordered grvContentarea gvTopHeader"></table>
                        </div>
                       
                    </div>

                    <div class="row">
                        <fieldset>
                            <asp:Label ID="lblPDCCheque" runat="server" CssClass="smLbl_to" Text="G. PDC Issue Status" ClientIDMode="Static"></asp:Label>
                        </fieldset>
                        <div class=" col-md-12 col-sm-12 col-lg-12">
                            <table id="gvpdc" border="1" class=" table-striped table-hover table-bordered grvContentarea gvTopHeader"></table>
                        </div>
                       
                    </div>


                    <div class="row">
                        <fieldset>
                            <asp:Label ID="lblProcurement" runat="server" CssClass="smLbl_to" Text="H. Procurement" ClientIDMode="Static"></asp:Label>
                        </fieldset>

                        <div class=" col-md-12 col-sm-12 col-lg-12">
                            <table id="gvprocure" border="1" class=" table-striped table-hover table-bordered grvContentarea gvTopHeader"></table>
                        </div>
                        
                    </div>

                    <div class="row">
                        <fieldset>
                            <asp:Label ID="lblConstruction" runat="server" CssClass="smLbl_to" Text="I. Construction" ClientIDMode="Static"></asp:Label>
                        </fieldset>

                        <div class=" col-md-12 col-sm-12 col-lg-12">
                            <table id="gvMMPlanVsAch" border="1" class=" table-striped table-hover table-bordered grvContentarea gvTopHeader"></table>
                        </div>
                      

                    </div>

                    <div class="row">
                        <fieldset>
                            <asp:Label ID="lblFeasibility" runat="server" CssClass="smLbl_to" Text="J. Bussiness Position(Today)" ClientIDMode="Static"></asp:Label>
                        </fieldset>

                        <div class=" col-md-12 col-sm-12 col-lg-12">
                            <table id="gvFeasibility" border="1" class=" table-striped table-hover table-bordered grvContentarea gvTopHeader"></table>
                        </div>
                       
                    </div>
                </div>

                <div class="row">
                    <fieldset>
                        <asp:Label ID="lblStock" runat="server" CssClass="smLbl_to" Text="M. Sold Status" ClientIDMode="Static"></asp:Label>
                    </fieldset>
                    <div class=" col-md-12 col-sm-12 col-lg-12">

                        <table id="gvpstk" border="1" class=" table-striped table-hover table-bordered grvContentarea gvTopHeader">
                        </table>
                    </div>
                   

                </div>

                <div class="row">

                    <fieldset>
                        <asp:Label ID="lblProjectStatus" runat="server" CssClass="smLbl_to" Text="N. Project Status (Upto Date)" ClientIDMode="Static"></asp:Label>
                    </fieldset>
                    <div class=" col-md-12 col-sm-12 col-lg-12">
                        <table id="gvProjectStatus01" border="1" class=" table-striped table-hover table-bordered grvContentarea gvTopHeader"></table>
                    </div>
                    

                </div>

                <div class="row">
                    <fieldset>
                        <asp:Label ID="lblMonProStatus" runat="server" CssClass="smLbl_to" Text="O. Project Status(During the Period)" ClientIDMode="Static"></asp:Label>
                    </fieldset>

                    <div class=" col-md-12 col-sm-12 col-lg-12">
                        <table id="gvmonprost" border="1" class=" table-striped table-hover table-bordered grvContentarea gvTopHeader"></table>
                    </div>
                   

                </div>
                <div class="row">
                    <fieldset>
                        <asp:Label ID="lblInventorystock" runat="server" CssClass="smLbl_to" Text="P. Inventory Status Report" ClientIDMode="Static"></asp:Label>
                    </fieldset>

                    <div class=" col-md-12 col-sm-12 col-lg-12">
                        <table id="gvInventory" border="1" class=" table-striped table-hover table-bordered grvContentarea gvTopHeader"></table>
                    </div>
                   
                </div>

                <div class="row">
                    <fieldset>
                        <asp:Label ID="lblMarketing" runat="server" CssClass="smLbl_to" Text="Q. Marketing Information" ClientIDMode="Static"></asp:Label>
                    </fieldset>

                    <div class=" col-md-12 col-sm-12 col-lg-12">
                        <table id="gvcomwclients" border="1" class=" table-striped table-hover table-bordered grvContentarea gvTopHeader"></table>
                    </div>
                   
                </div>
                <div class="row">


                    <fieldset>
                        <asp:Label ID="lblFixedAssets" runat="server" CssClass="smLbl_to" Text="R. Fixed Asset Status" ClientIDMode="Static"></asp:Label>
                    </fieldset>

                    <div class=" col-md-12 col-sm-12 col-lg-12">
                        <table id="gvFxtAssets" border="1" class=" table-striped table-hover table-bordered grvContentarea gvTopHeader"></table>
                    </div>
                    
                </div>

                <div class="row">
                    <fieldset>
                        <asp:Label ID="lblFinancialst" runat="server" CssClass="smLbl_to" Text="S. Finalcial Statement" ClientIDMode="Static"></asp:Label>
                    </fieldset>

                    <div class=" col-md-12 col-sm-12 col-lg-12">
                        <table id="gvFinalcialst" border="1" class=" table-striped table-hover table-bordered grvContentarea gvTopHeader"></table>
                    </div>
                   

                </div>

                <div class="row">
                    <fieldset>
                        <asp:Label ID="lblHrMgt" runat="server" CssClass="smLbl_to hidden" Text="T. HR Management" ClientIDMode="Static"></asp:Label>
                    </fieldset>

                    <div class=" col-md-12 col-sm-12 col-lg-12 hidden">
                        <table id="gvHremp" border="1" class=" table-striped table-hover table-bordered grvContentarea gvTopHeader"></table>
                    </div>
                   
                </div>



            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
