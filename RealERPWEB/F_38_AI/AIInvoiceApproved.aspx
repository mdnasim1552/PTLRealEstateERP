<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AIInvoiceApproved.aspx.cs" Inherits="RealERPWEB.F_38_AI.AIInvoiceApproved" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        .chzn-drop {
            width: 100% !important;
        }

        .chzn-container {
            width: 100% !important;
            height: 28px !important;
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

            <div class="card mt-3">
                <div class="card-header pt-2 pb-2">
                    <div class="row">
                        <div class="col-lg-1 col-md-2 col-sm-6">
                            <asp:Label ID="Label1" runat="server">Voucher Date</asp:Label>
                            <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            <cc1:CalendarExtender runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-6">
                            <asp:Label ID="Label7" runat="server">Voucher No</asp:Label>
                            <asp:TextBox ID="txtvounum1" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                           
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-6 p-1 mt-3">
                            <asp:TextBox ID="txtvounum2" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                        </div>
                        <div class="col-lg-1 col-md-2 col-sm-6">
                            <asp:Label ID="Label2" runat="server">Invoice No</asp:Label>
                            <asp:TextBox ID="tblinvo" runat="server" Enabled="false" CssClass="form-control form-control-sm"></asp:TextBox>

                        </div>
                        
                        <div class="col-lg-1 col-md-1 p-1 mt-3">
                            <div class="form-group-">
                                <asp:LinkButton ID="lnkbtnok" runat="server" OnClick="lnkbtnok_Click" CssClass=" btn btn-primary btn-sm mt20">Ok</asp:LinkButton></li>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="card-body">
                    <div class="row">
                         <div class="table-responsive mt-2">
                                <asp:GridView ID="gvInvoApp" runat="server" AutoGenerateColumns="False" CssClass=" table-striped  table-bordered grvContentarea"
                                        ShowFooter="True" Visible="True" AllowPaging="true" PageSize="15" >
                                    <Columns>

                                    </Columns>
                                </asp:GridView>
                         </div>
                    </div>
                    <asp:Panel ID="pnlupdate" runat="server" Visible="false">
                        <div class="row ">
                            <div class="col-lg-2 col-md-2 col-sm-6">
                                <asp:Label ID="Label4" runat="server">Ref./Cheq No/Slip No</asp:Label>
                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-6">
                                <asp:Label ID="Label5" runat="server">Other ref.(if any)</asp:Label>
                                <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            </div>
                            <div class="col-lg-1 col-md-1 mt-3">
                                <div class="form-group-">
                                    <asp:LinkButton ID="lnkbtnUpdate" runat="server" CssClass=" btn btn-primary btn-sm mt20">Update</asp:LinkButton></li>
                                </div>
                            </div>
                        </div>
                        <div class="row  p-2">
                            <div class="col-lg-6 col-md-6 col-sm-6">
                                <asp:Label ID="Label6" runat="server">Naration</asp:Label>
                                <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
