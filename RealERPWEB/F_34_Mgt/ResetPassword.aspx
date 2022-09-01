<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.ResetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {

            $(".chosen-select").chosen({
                search_contains: true,
                no_results_text: "Sorry, no match!",
                allow_single_deselect: true
            });
            $('.chzn-select').chosen({ search_contains: true });
        };

    </script>

    <style>
       
    </style>
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
    <div class="card card-fluid container-data mt-5" id='printarea'>
        <div class="card-header">
            <div class="row">
                <div class="col-lg-3 col-md-3 col-sm-6">
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server">User</asp:Label>
                        <asp:DropDownList ID="ddlUserList" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlUserList_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6">
                    <div class="form-group">
                        <asp:Label ID="lblmUserId" runat="server" Visible="false"></asp:Label>

                        <asp:Label ID="Label9" runat="server">User ID </asp:Label>
                        <asp:TextBox ID="txtmUesrId" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>

                    </div>

                </div>
                <div class="col-lg-3 col-md-3 col-sm-6">
                    <div class="form-group">
                        
                        <asp:Label ID="Label11" runat="server">Full Name</asp:Label>
                                                <asp:TextBox ID="txtmFullName" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>

                    </div>

                </div>
                 <div class="col-lg-3 col-md-3 col-sm-6">                   
                    <div class="form-group">
                        <asp:Label ID="Label6" runat="server">Active</asp:Label>
                        <asp:CheckBox ID="chkmUserActive" runat="server" />
                    </div>
                </div>
            </div>

            <div class="row mb-1">
                <div class="col-lg-3 col-md-3 col-sm-6">
                    <div class="form-group">
                        <asp:Label ID="Label2" runat="server">User Name</asp:Label>
                        <asp:TextBox ID="txtmShortName" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label5" runat="server">Password</asp:Label>
                        <asp:TextBox ID="txtmPassword" runat="server" AutoCompleteType="Disabled" CssClass="form-control" TextMode="Password" onkeypress="return IsNumberWithOneDecimal(this,event);"></asp:TextBox>

                    </div>
                </div>
               
                <div class="col-lg-3 col-md-3 col-sm-6">
                    <div class="form-group">
                        <asp:Label ID="Label7" runat="server">Employee ID</asp:Label>
                        <asp:DropDownList ID="ddlmEmpId" runat="server" CssClass="form-control chzn-select"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label4" runat="server">Designation</asp:Label>
                        <asp:TextBox ID="txtmDesignation" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

            </div>




            <div class="row mb-1">
                <div class="col-lg-3 col-md-3 col-sm-6">
                    <div class="form-group">
                        <asp:Label ID="Label8" runat="server">User Role</asp:Label>
                        <asp:DropDownList ID="ddlmUserRole" runat="server" CssClass="form-control">
                            <asp:ListItem Value="1">Admin</asp:ListItem>
                            <asp:ListItem Value="2">Managment</asp:ListItem>
                            <asp:ListItem Selected Value="3">User</asp:ListItem>
                            <asp:ListItem Value="4">HR</asp:ListItem>
                            <asp:ListItem Value="5">IT</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                </div>
                <div class="col-lg-3 col-md-3 col-sm-6">
                    <div class="form-group">
                        <asp:Label ID="Label3" runat="server">Home Menu Link</asp:Label>                      
                        <asp:DropDownList ID="ddlMenuLink" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 text-center">

                            <asp:LinkButton ID="lbtnSaveUser" runat="server" CssClass="btn btn-sm btn-success"   OnClick="lbtnSaveUser_Click"> Update </asp:LinkButton>

                                    <asp:TextBox ID="txtmGraph" runat="server" AutoCompleteType="Disabled" CssClass="form-control d-none"></asp:TextBox>
                                    <asp:TextBox ID="txtmUserEmail" runat="server" AutoCompleteType="Disabled" CssClass="form-control d-none"></asp:TextBox>
                                    <asp:TextBox ID="txtmWebMailPass" runat="server" AutoCompleteType="Disabled" CssClass="form-control d-none" TextMode="Password" onkeypress="return IsNumberWithOneDecimal(this,event);"></asp:TextBox>

            </div>
            </div>


        </div>
    </div>
</asp:Content>
