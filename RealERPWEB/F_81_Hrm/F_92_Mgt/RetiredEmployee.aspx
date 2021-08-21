<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RetiredEmployee.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_92_Mgt.RetiredEmployee" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

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
            <div class="container moduleItemWrpper">
                <div class="contentPart">

                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-1 pading5px asitCol3">
                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                        <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindCompany" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindCompany_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlCompanyName" runat="server" Width="300px" CssClass=" chzn-select form-control inputTxt pull-left" OnSelectedIndexChanged="ddlCompanyName_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblCompanyName" runat="server" Width="300px" CssClass="form-control inputTxt" Visible="False"></asp:Label>

                                    </div>
                                    <div class="col-md-1">


                                        <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lnkbtnShow_Click">Ok</asp:LinkButton>


                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblDept" runat="server" CssClass="lblTxt lblName">Department</asp:Label>
                                        <asp:TextBox ID="txtSrcDepartment" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnDeptSrch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnDeptSrch_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlDepartment" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" runat="server" Width="300px" CssClass=" chzn-select  form-control inputTxt" TabIndex="6" AutoPostBack="True">
                                        </asp:DropDownList>

                                        <asp:Label ID="lblDeptDesc" runat="server" CssClass="form-control inputTxt" Visible="False" Width="300px"></asp:Label>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName">Section</asp:Label>
                                        <asp:TextBox ID="txtSrcSecion" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnSection" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnSection_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol4">
                                        <%--  <asp:DropDownList I" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" runat="server" Width="300px" CssClass="form-control inputTxt" TabIndex="15" AutoPostBack="true">
                                        </asp:DropDownList>--%>
                                        <asp:DropDownList ID="ddlSection1" OnSelectedIndexChanged="ddlSection1_SelectedIndexChanged" runat="server" Width="300px" CssClass=" chzn-select  form-control inputTxt" TabIndex="15" AutoPostBack="true">
                                        </asp:DropDownList>

                                        <asp:Label ID="lblSectionDesc" runat="server" CssClass="form-control inputTxt" Visible="False" Width="300px"></asp:Label>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName">Employee</asp:Label>
                                        <asp:TextBox ID="txtSrcEmployee" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnEmployee" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnEmployee_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlEmployee" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" AutoPostBack="true" runat="server" Width="300px" CssClass="chzn-select form-control inputTxt" TabIndex="6">
                                        </asp:DropDownList>

                                        <asp:Label ID="lblddlEmployee" runat="server" CssClass="form-control inputTxt" Visible="False" Width="300px"></asp:Label>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName">Designation</asp:Label>
                                        <asp:TextBox ID="txtSrcEmployee0" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnEmpName" runat="server" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol4">

                                        <asp:TextBox ID="lblDesig" runat="server" CssClass="form-control inputTxt" Width="300px"></asp:TextBox>


                                    </div>

                                </div>

                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <asp:Panel ID="PnlSepType" runat="server" Visible="False">
                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">

                                        <div class="form-group">
                                            <div class="col-md-3 pading5px">
                                                <asp:Label ID="lblfrmDate" runat="server" CssClass="lblTxt lblName">Date</asp:Label>
                                                <asp:TextBox ID="txtSepDate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtSepDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtSepDate"></cc1:CalendarExtender>

                                            </div>
                                            <div class="col-md-4 pading5px">
                                                <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName">Separation Type</asp:Label>


                                                <asp:DropDownList ID="ddlSepType" runat="server" Width="233" CssClass="form-control inputTxt pull-left" TabIndex="2">
                                                </asp:DropDownList>

                                                <div class="pull-left">
                                                    <asp:LinkButton ID="lnkbtnUpdate" runat="server" CssClass="btn btn-danger primaryBtn pull-left" OnClick="lnkbtnUpdate_Click">Update</asp:LinkButton>
                                                </div>
                                            </div>

                                            <div class="col-md-3 pull-right">
                                                <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                            </div>

                                        </div>
                                    </div>
                            </div>


                        </asp:Panel>
                    </div>

                </div>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


