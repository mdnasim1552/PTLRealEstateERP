<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="CodeDataTrans.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.CodeDataTrans" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
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
    <style type="text/css">
         .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>




            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="CodeTransfer" runat="server">
                    <div class="card card-fluid mb-1 mt-2">
                        <div class="card-body">

                            <div class="row">
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="14px"
                                            ForeColor="Black"
                                            Style="border-top: 1px solid Black; border-bottom: 1px solid Black; text-align: right;"
                                            Text="Present Code:" Width="100px" CssClass="pull-left"></asp:Label>
                                    </div>



                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <asp:Label ID="lblCurDate" runat="server"
                                            Text="Date:"></asp:Label>

                                        <asp:TextBox ID="txtCurDate" runat="server" AutoCompleteType="Disabled"
                                            CssClass="form-control  form-control-sm">
                                        </asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>
                                    </div>

                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblcontrolAccHead" runat="server">Accounts Code:</asp:Label>
                                        <asp:TextBox ID="txtserceacc" runat="server" CssClass=" inputTxt inputName inpPixedWidth d-none"></asp:TextBox>

                                        <asp:LinkButton ID="imgbtnFindAccount" runat="server" OnClick="imgbtnFindAccount_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>


                                        <asp:DropDownList ID="ddlAccHead" runat="server" AutoPostBack="true" CssClass="chzn-select form-control  form-control-sm" OnSelectedIndexChanged="ddlAccHead_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblAccCodedesc" runat="server" Visible="False" CssClass="form-control  form-control-sm"></asp:Label>
                                    </div>


                                </div>






                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lbltxtdetailsCode" runat="server" CssClass="lblTxt lblName">Details Code:</asp:Label>
                                        <asp:TextBox ID="txtserDetailsCode" runat="server" CssClass=" inputTxt inputName inpPixedWidth d-none"></asp:TextBox>

                                        <asp:LinkButton ID="imgbtnFindDetailsCode" runat="server" OnClick="imgbtnFindDetailsCode_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>


                                        <asp:DropDownList ID="ddlresuorcecode" runat="server" AutoPostBack="true" CssClass="chzn-select form-control  form-control-sm" OnSelectedIndexChanged="ddlresuorcecode_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblResCodedesc" runat="server" Visible="False" CssClass="form-control  form-control-sm"></asp:Label>
                                    </div>

                                </div>





                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lbltxtSpecification" runat="server" CssClass="lblTxt lblName">Specification:</asp:Label>
                                        <asp:TextBox ID="txtserSpecification" runat="server" CssClass=" inputTxt inputName inpPixedWidth d-none"></asp:TextBox>

                                        <asp:LinkButton ID="imgbtnFindSpecification" runat="server" OnClick="imgbtnFindSpecification_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>


                                        <asp:DropDownList ID="ddlSpclfication" runat="server" CssClass="chzn-select form-control  form-control-sm">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblSpcCodedesc" runat="server" Visible="False" CssClass="form-control  form-control-sm"></asp:Label>
                                    </div>

                                </div>



                                <div class="col-md-1" style="margin-top: 22px;">
                                    <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-sm btn-primary">Ok</asp:LinkButton>
                                </div>


                            </div>
                        </div>
                    </div>

                    <div class="card card-fluid mb-1 mt-2">
                        <div class="card-body">

                            <asp:Panel ID="PnlNewCode" runat="server" Visible="False">
                                <div class="row">

                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="14px"
                                                ForeColor="Black"
                                                Style="border-top: 1px solid Black; border-bottom: 1px solid Black; text-align: right;"
                                                Text="New Code:" Width="100px"></asp:Label>
                                        </div>
                                    </div>


                                    <div class="col-md-3">
                                        <div class="form-group">

                                            <asp:Label ID="lblcontrolAccHeadN" runat="server">Accounts Code:</asp:Label>
                                            <asp:TextBox ID="txtserceaccN" runat="server" CssClass=" inputTxt inputName inpPixedWidth d-none"></asp:TextBox>

                                            <asp:LinkButton ID="imgbtnFindAccountN" runat="server" OnClick="imgbtnFindAccountN_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>


                                            <asp:DropDownList ID="ddlAccHeadN" runat="server" AutoPostBack="true" CssClass=" chzn-select form-control form-control-sm" OnSelectedIndexChanged="ddlAccHeadN_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>

                                    </div>







                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="lbltxtdetailsCodeN" runat="server" CssClass="lblTxt lblName">Details Code:</asp:Label>
                                            <asp:TextBox ID="txtserDetailsCodeN" runat="server" CssClass=" inputTxt inputName inpPixedWidth d-none"></asp:TextBox>

                                            <asp:LinkButton ID="imgbtnFindDetailsCodeN" runat="server" OnClick="imgbtnFindDetailsCodeN_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>


                                            <asp:DropDownList ID="ddlresuorcecodeN" runat="server" AutoPostBack="true" CssClass="chzn-select form-control form-control-sm" OnSelectedIndexChanged="ddlresuorcecodeN_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>





                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="lbltxtSpecificationN" runat="server" CssClass="lblTxt lblName">Specification:</asp:Label>
                                            <asp:TextBox ID="txtserSpecificationN" runat="server" CssClass=" inputTxt inputName inpPixedWidth d-none"></asp:TextBox>

                                            <asp:LinkButton ID="imgbtnFindSpecificationN" runat="server" OnClick="imgbtnFindSpecificationN_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>


                                            <asp:DropDownList ID="ddlSpclficationN" runat="server" CssClass="chzn-select form-control form-control-sm">
                                            </asp:DropDownList>
                                        </div>
                                    </div>


                                    <div class="col-md-1" style="margin-top: 22px;">
                                        <asp:LinkButton ID="lnkFinalUpdate" runat="server" Text="Ok" OnClientClick="javascript:return FunConfirmSave();" OnClick="lnkFinalUpdate_Click" CssClass="btn btn-sm  btn-danger">Final Update</asp:LinkButton>
                                    </div>

                                    <div class="col-md-3 pading5px pull-right">
                                        <div class="msgHandSt">
                                            <asp:Label ID="lblmsg1" CssClass="btn-danger btn disabled primaryBtn d-none" runat="server" Visible="false"></asp:Label>
                                        </div>
                                    </div>

                                </div>


                            </asp:Panel>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="View2" runat="server">
                </asp:View>
            </asp:MultiView>



        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

