<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AIInVoiceCreate.aspx.cs" Inherits="RealERPWEB.F_38_AI.AIInVoiceCreate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .chzn-drop {
            width: 100% !important;
        }
        .chzn-container{
            width: 100% !important;
            height:28px!important;
        }

        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
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
                    <div class="card-header p-1">
                        <h4>New Invoice</h4>
                    </div>
                    <div class="card-body  well">
                        <div class="row mb-1">
                            <asp:Label ID="Label4" CssClass="col-lg-1 col-form-label" runat="server">Date</asp:Label>
                            <div class="col-lg-3">
                                <asp:TextBox runat="server" ID="txtdate" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="row mb-1">

                            <asp:Label ID="Label7" CssClass="col-lg-1 col-form-label" runat="server">Invoice No
                                    <span>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="vldtxtInvoice" runat="server" ForeColor="Red" ControlToValidate="txtInvoiceno" ValidationGroup="NewInvoice"
                                            ErrorMessage="*" /></span>

                            </asp:Label>
                            <div class="col-lg-3 col-md-3 col-sm-6">
                                <asp:TextBox runat="server" ID="txtInvoiceno" placeholder="AI-INV-000001" CssClass="form-control form-control-sm"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row mb-1">
                            <asp:Label ID="Label11" CssClass="col-lg-1 col-form-label" runat="server">InVoice Date</asp:Label>
                            <div class="col-lg-3">
                                <asp:TextBox runat="server" ID="txtinvoicedate" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender runat="server" Format="dd-MMM-yyyy" TargetControlID="txtinvoicedate"></cc1:CalendarExtender>
                            </div>
                            <asp:Label ID="Label12" CssClass="col-lg-1 col-form-label" runat="server">&nbsp; Due Date</asp:Label>
                            <div class="col-lg-3">
                                <asp:TextBox runat="server" ID="txtduedate" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender runat="server" Format="dd-MMM-yyyy" TargetControlID="txtduedate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="row mb-1">
                            <asp:Label ID="Label3" CssClass="col-lg-1 col-form-label" runat="server">Clients</asp:Label>
                            <div class="col-lg-3 col-md-3 col-sm-6">
                                <asp:DropDownList ID="ddlsuplier" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlsuplier_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <asp:Label ID="Label1" CssClass="col-lg-1 col-form-label" runat="server">&nbsp; ProjectName</asp:Label>
                            <div class="col-lg-3 col-md-3 col-sm-6">
                                <asp:DropDownList ID="ddlprojname" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm chzn-select">
                                </asp:DropDownList>
                            </div>
                            <asp:Label ID="Label2" CssClass="col-lg-1 col-form-label" runat="server"> &nbsp;&nbsp; BatchName</asp:Label>
                            <div class="col-lg-3 col-md-3 col-sm-6">
                                <asp:DropDownList ID="ddlbatchname" runat="server" CssClass="form-control form-control-sm chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>   
                        <div class="row mb-1">                           
                                <asp:Label ID="Label5" CssClass="col-lg-1 col-form-label" runat="server">Data Set</asp:Label>
                             <div class="col-lg-3 col-md-3 col-sm-6">
                                <asp:DropDownList ID="ddldataset" runat="server" CssClass="form-control form-control-sm chzn-select">
                                </asp:DropDownList>
                            </div>
                           <asp:Label ID="Label9" CssClass="col-lg-1 col-form-label" runat="server">&nbsp; Quantity</asp:Label>
                         <div class="col-lg-3 col-md-3 col-sm-6">
                            <asp:TextBox runat="server" ID="txtdoneqty" CssClass="form-control form-control-sm"></asp:TextBox>
                        </div>                              
                            <asp:Label ID="Label8"  CssClass="col-lg-1 col-form-label" runat="server">&nbsp;Rate</asp:Label>
                         <div class="col-lg-3 col-md-3 col-sm-6">
                            <asp:TextBox runat="server" ID="txtrate" CssClass="form-control form-control-sm"></asp:TextBox>
                        </div>
                    </div>
                   <div class="row mb-1">
                        <asp:Label ID="Label6"  CssClass="col-lg-1 col-form-label" runat="server">Subject</asp:Label>
                         <div class="col-lg-6 col-md-3 col-sm-6">
                            <asp:TextBox runat="server" ID="TextBox1" CssClass="form-control form-control-sm" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <div class="mt-1">                       
                            <asp:LinkButton ID="lnkbtnok" runat="server" CssClass=" btn btn-primary btn-sm mt20">ADD</asp:LinkButton></li>
                    </div>
                   </div>
                </div>
            </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
