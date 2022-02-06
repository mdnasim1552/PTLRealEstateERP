<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RetiredEmployee.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_92_Mgt.RetiredEmployee" %>

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

            <div class="card card-fluid container-data mt-5" style="min-height: 1000px;">

                <div class="card-header">
                    <div class="row mb-2">
                        <div class="col-md-3">
                            <div class="input-group input-group-alt ">
                                <div class="input-group-prepend">
                                    <asp:LinkButton ID="ibtnFindCompany" runat="server" CssClass="btn btn-secondary ml-1" OnClick="ibtnFindCompany_Click">
                                                  Company</asp:LinkButton>
                                </div>
                                <asp:DropDownList ID="ddlCompanyName" runat="server" OnSelectedIndexChanged="ddlCompanyName_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="input-group input-group-alt ">
                                <div class="input-group-prepend">
                                    <asp:LinkButton ID="imgbtnDeptSrch" runat="server" CssClass="btn btn-secondary ml-1" OnClick="imgbtnDeptSrch_Click">
                                                  Department</asp:LinkButton>

                                </div>
                                <asp:DropDownList ID="ddlDepartment" runat="server" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="input-group input-group-alt ">
                                <div class="input-group-prepend">
                                    <asp:LinkButton ID="imgbtnSection" runat="server" CssClass="btn btn-secondary ml-1" OnClick="imgbtnSection_Click">
                                                  Section</asp:LinkButton>

                                </div>
                                <asp:DropDownList ID="ddlSection1" runat="server" OnSelectedIndexChanged="ddlSection1_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>



                    </div>
                    <div class="row mb-2">
                        <div class="col-md-3">
                            <div class="input-group input-group-alt ">
                                <div class="input-group-prepend">
                                    <asp:LinkButton ID="imgbtnEmployee" runat="server" CssClass="btn btn-secondary ml-1" OnClick="imgbtnEmployee_Click">
                                                  Employee</asp:LinkButton>

                                </div>
                                <asp:DropDownList ID="ddlEmployee" runat="server" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control chzn-select" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="input-group input-group-alt ">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary ml-1" type="button">Separation Type</button>
                                </div>
                                <asp:DropDownList ID="ddlSepType" runat="server" CssClass="form-control" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="input-group input-group-alt ">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary ml-1" type="button">Date</button>
                                </div>
                                <asp:TextBox ID="txtSepDate" runat="server" CssClass=" form-control "></asp:TextBox>
                                <cc1:CalendarExtender ID="txtSepDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtSepDate"></cc1:CalendarExtender>

                            </div>
                        </div>

                        <div class="col-md-3">
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary" OnClick="lnkbtnUpdate_Click">Add</asp:LinkButton>
                                <asp:LinkButton ID="lnkbtnUpdate" runat="server" CssClass="btn btn-danger primaryBtn pull-left" OnClick="lnkbtnUpdate_Click">Update</asp:LinkButton>

                        </div> 
                    </div>
                     
                </div>
            </div>
             
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


