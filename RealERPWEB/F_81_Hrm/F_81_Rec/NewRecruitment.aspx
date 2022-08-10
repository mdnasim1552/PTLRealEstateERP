<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="NewRecruitment.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_81_Rec.NewRecruitment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .mt20 {
            margin-top: 20px;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .chzn-container {
            width: 100% !important;
        }

        .chzn-container-single .chzn-single {
            height: 35px !important;
            line-height: 35px !important;
        }

        .card-body {
            min-height: 400px !important;
        }

        .pd4 {
            padding: 4px !important;
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
            <div class="card mt-5">
                <div class="card-header">
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-5">

                        <div class="form-group row">
                            <asp:Label ID="lblname" runat="server" CssClass="col-4">Name</asp:Label>
                            <asp:TextBox ID="txtname" runat="server" CssClass="form-control form-control-sm pd4 col-8"></asp:TextBox>
                        </div>

                        <div class="form-group row">
                            <asp:Label ID="lbldept" runat="server" CssClass="col-4">Department</asp:Label>
                             <asp:DropDownList ID="ddldept" runat="server"  CssClass="form-control form-control-sm pd4 col-8"  AutoPostBack="True" ></asp:DropDownList>
                        </div>

                                    <div class="form-group row">
                            <asp:Label ID="lbldesig" runat="server" CssClass="col-4">Designation</asp:Label>
                             <asp:DropDownList ID="ddldesig" runat="server"  CssClass="form-control form-control-sm pd4 col-8"  AutoPostBack="True" ></asp:DropDownList>
                        </div>
                        <div class="form-group row">
                            <asp:Label ID="lblmobile" runat="server" CssClass="col-4">Mobile No</asp:Label>
                            <asp:TextBox ID="txtmobile" runat="server" CssClass="form-control form-control-sm pd4 col-8"></asp:TextBox>
                        </div>
                        <div class="form-group row">
                            <asp:Label ID="lblemail" runat="server" CssClass="col-4">Email Address</asp:Label>
                            <asp:TextBox ID="txtemail" runat="server" CssClass="form-control form-control-sm pd4 col-8"></asp:TextBox>
                        </div>
                        <div class="form-group row">
                            <asp:Label ID="lblpreAdd" runat="server" CssClass="col-4">Present Address</asp:Label>
                            <asp:TextBox ID="txtPreAdd" runat="server" CssClass="form-control form-control-sm pd4 col-8"></asp:TextBox>
                        </div>
                        <div class="form-group row">
                            <asp:Label ID="lblPerAdd" runat="server" CssClass="col-4">Permanent Address</asp:Label>
                            <asp:TextBox ID="txtPerAdd" runat="server" CssClass="form-control form-control-sm pd4 col-8"></asp:TextBox>
                        </div>

                        <div class="form-group row">
                            <asp:Label ID="lblfile" runat="server" Text="Attach File:" CssClass="col-4"></asp:Label>
                            <asp:FileUpload ID="imgFileUpload" CssClass="form-control col-8" runat="server" AllowMultiple="true" accept=".pdf" />
                            <asp:RequiredFieldValidator ForeColor="Red" runat="server" ControlToValidate="imgFileUpload" ValidationGroup="group1" ErrorMessage="Please enter an image" />
                        </div>
                                   <div class="form-group row">
                        <asp:LinkButton ID="lnkSave" ValidationGroup="group1" CssClass="btn btn-success btn-sm mt20" runat="server" OnClick="lnkSave_Click">Save</asp:LinkButton>
           
                        </div>

                            </div>
                        <div class="col-7">


                         </div>
                    </div>

                </div>
            </div>




        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkSave" />
        </Triggers>

    </asp:UpdatePanel>
</asp:Content>

