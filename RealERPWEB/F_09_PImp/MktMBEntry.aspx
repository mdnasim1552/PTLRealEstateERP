<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="MktMBEntry.aspx.cs" Inherits="RealERPWEB.F_09_PImp.MktMBEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }

        .grvContentarea {
            margin-right: 0px;
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
            $('.chzn-select').chosen({ search_contains: true });
            $(".chosen-select").chosen({
                search_contains: true,
                no_results_text: "Sorry, no match!",
                allow_single_deselect: true
            });



        };
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <div class="card card-fluid mb-2">
                <div class="card-body">

                    <div class="row">
                        <div class="col-sm-1 col-md-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblOrderDate" runat="server" class="control-label" Text="MB Date"></asp:Label>
                                <asp:TextBox ID="txtCurOrderDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtCurOrderDate_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd.MM.yyyy" TargetControlID="txtCurOrderDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-1 col-md-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblOrderNo" runat="server" class="control-label" Text="MB No"></asp:Label>
                                <asp:Label ID="lblCurOrderNo1" runat="server" class="control-label" Text="POR00- "></asp:Label>
                                <asp:TextBox ID="txtCurOrderNo2" runat="server" CssClass="form-control form-control-sm" Text="00000" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2 col-md-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblRefNo" runat="server" class="control-label" Text="Ref. No"></asp:Label>
                                <asp:TextBox ID="txtOrderRefNo" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-1 col-md-1 col-lg-1">
                            <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm" Style="margin-top: 20px;"></asp:LinkButton>
                        </div>

                        <div class="col-sm-2 col-md-2 col-lg-2">
                            <label id="lblpreviousmb" runat="server">   Previous MB</label>
                                <asp:LinkButton ID="lbtnPrevOrderList" runat="server" CssClass="lblTxt lblName" OnClick="lbtnPrevOrderList_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                            <asp:DropDownList ID="ddlPrevOrderList" runat="server" Width="180px" CssClass="form-control chzn-select" >
                            </asp:DropDownList>
                        </div>

                    </div>


                     <div class="row">
                        <div class="col-sm-2 col-md-2 col-lg-">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" class="control-label" Text="Project"></asp:Label>
                                  <asp:DropDownList ID="ddlProject" runat="server"  CssClass="form-control chzn-select" >
                            </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" class="control-label" Text="Contracttor"></asp:Label>
                               
                                 <asp:DropDownList ID="ddlContractor" runat="server" CssClass="form-control chzn-select" >
                            </asp:DropDownList>
                            </div>
                        </div>

                      
                    </div>




                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
