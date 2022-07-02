<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptLoanEmpwise.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_85_Lon.RptLoanEmpwise" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<style>
        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;

        }
           .chzn-container{
             width: 100% !important;
        }

        .chzn-drop {
            width: 100% !important;
        }
                        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }
    </style>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
           <%-- var gvAnnIncre = $('#<%=this.gvAnnIncre.ClientID %>');
            gvAnnIncre.Scrollable();--%>

            <%--var gridview = $('#<%=this.gvAnnIncre.ClientID %>');
            $.keynavigation(gridview);--%>

            $('.chzn-select').chosen({ search_contains: true });

        };
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

              <div class="card mt-5">
                <div class="card-header">
                        <div class="row">
                            <div class="col-md-3 col-sm-6 col-lg-3 col-xs-12">
                                <div class="form-group">
                                    <asp:Label ID="lblCompany" runat="server" class="control-label" Text="Company"></asp:Label>
                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control chzn-select form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6 col-lg-3 col-xs-12">
                                <div class="form-group">
                                    <asp:Label ID="lblDepartment" runat="server" class="control-label" Text="Department"></asp:Label>
                                    <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control chzn-select form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6 col-lg-3 col-xs-12">
                                <div class="form-group">
                                    <asp:Label ID="lblSection" runat="server" class="control-label" Text="Section"></asp:Label>
                                    <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control chzn-select form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                           
                       
                             <div class="col-md-3 col-sm-6 col-lg-2 col-xs-12">
                                <div class="form-group">
                                    <asp:Label ID="lblEmployee" runat="server" class="control-label">Employee</asp:Label>
                                    <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control chzn-select form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                             <div class="col-md-1 col-sm-1 col-lg-1 col-xs-12">

                                <asp:LinkButton ID="lnkbtnShow" runat="server" Text="Ok"  CssClass="btn btn-primary btn-sm" Style="margin-top: 20px;"></asp:LinkButton>
                            </div>

                                        
                        </div>
                </div>
                  <div class="card-body">

                  </div>
       </div>

        
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
