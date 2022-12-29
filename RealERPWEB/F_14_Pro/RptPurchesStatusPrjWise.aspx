<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptPurchesStatusPrjWise.aspx.cs" Inherits="RealERPWEB.F_14_Pro.RptPurchesStatusPrjWise" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {
            //$(".pop").on("click", function () {
            //    $('#imagepreview').attr('src', $(this).attr('src')); // here asign the image to the modal when the user click the enlarge link
            //    $('#imagemodal').modal('show'); // imagemodal is the id attribute assigned to the bootstrap modal, then i use the show function
            //});
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
                <div class="card-header bg-light">
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server">From</asp:Label>
                                <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server">To</asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true"></asp:TextBox>
                                <cc1:CalendarExtender runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>
                        </div>
                         <div class="col-md-2 mt-4">
                            <div class="form-group-">
                                <asp:LinkButton ID="lnkbtnok" runat="server"  CssClass=" btn btn-primary btn-sm mt20">Ok</asp:LinkButton></li>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-body">
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
