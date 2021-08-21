
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="UserImage.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.UserImage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  
     
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="page">
                <div class="page-inner">
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
                    <!-- .page-title-bar -->
                    <header class="page-title-bar">
                        <!-- page title stuff goes here -->
                        <h1 class="page-title">EMPLOYEE IMAGE UPLOAD </h1>
                    </header>
                    <!-- /.page-title-bar -->
                    <!-- .page-section -->
                    <div class="page-section">
                        <!-- .section-block -->
                        <div class="section-block">
                          
                              <div class="card">
                                <div class="card-body">
                                    <div class="col-6">
                                        <div class="form-group">
                                            <!-- .input-group -->
                                            <div class="input-group input-group-alt">
                                                <label class="input-group-prepend" for="bi2"><span class="input-group-text">User List</span></label>

                                                <asp:TextBox ID="txtUserSrc" runat="server" CssClass="form-control" placeholder="e.g. UserName"></asp:TextBox>
                                                <div class="input-group-append">

                                                    <asp:LinkButton ID="ibtnUserList" runat="server" CssClass="btn btn-primary" OnClick="ibtnUserList_Click"><span class="fa fa-search"> </span></asp:LinkButton>

                                                </div>
                                                <div class="input-group-append">
                                                    <asp:DropDownList ID="ddlUserName" runat="server" OnSelectedIndexChanged="ddlUserName_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control inputTxt">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <!-- /.input-group -->
                                        </div>
                                    </div>
                                </div>


                            </div>
                            <div class="row">
                                <div class="col-12 col-lg-12 col-xl-6">
                                    <div class="card">
                                        <div class="card-body text-center">
                                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Employee Image</asp:Label>

                                            <div class="form-group">


                                                <asp:Image ID="EmpImg" runat="server" Height="100px" Width="100px" />

                                                <div class="custom-file">
                                                    <asp:FileUpload ID="imgFileUpload" CssClass="custom-file-input" runat="server"
                                                        onchange="submitform();" ToolTip="Employee Image" Width="216px" />

                                                    <label class="custom-file-label" for="customFile">Choose files</label>
                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 col-lg-12 col-xl-6">
                                    <div class="card">
                                        <div class="card-body text-center">
                                            <asp:Label ID="Label2" CssClass=" smLbl_to" runat="server">Employee Signature</asp:Label>

                                            <div class="form-group ">


                                                <asp:Image ID="EmpSig" runat="server" Height="100px" Style="margin-left: 0px"
                                                    Width="100px" />

                                                <div class="custom-file">
                                                    <asp:FileUpload ID="imgSigFileUpload" CssClass="custom-file-input" runat="server"
                                                        onchange="submitform();" ToolTip="Employee Signature" Width="216px" />

                                                    <label class="custom-file-label" for="customFile">Choose files</label>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                            </div>
                           
                        </div>
                        <!-- /.section-block -->
                    </div>
                    <!-- /.page-section -->
                </div>



            </div>
            <div class="container">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label9" runat="server" CssClass="lblTxt lblName">User List:</asp:Label>


                                    </div>
                                    <div class="col-md-4 pading5px ">
                                    </div>


                                    <div class="col-md-3 pading5px pull-right">
                                        <div class="msgHandSt">
                                            <asp:Label ID="lblmsg" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>
                                        </div>


                                    </div>
                                </div>

                            </div>
                        </fieldset>
                    </div>

                </div>
            </div>
                        
            <script>
                //toster
                function showContent(msg) {


                    toastr.options = {
                        "closeButton": true,
                        "debug": false,
                        "progressBar": true,
                        "preventDuplicates": false,
                        "positionClass": "toast-top-center",
                        "showDuration": "400",
                        "hideDuration": "1000",
                        "timeOut": "7000",
                        "extendedTimeOut": "1000",
                        "showEasing": "swing",
                        "hideEasing": "linear",
                        "showMethod": "fadeIn",
                        "hideMethod": "fadeOut"
                    }
                    toastr["success"](msg);

                };
                function showContentFail(msg) {

                    toastr.options = {
                        "closeButton": true,
                        "debug": false,
                        "progressBar": true,
                        "preventDuplicates": false,
                        "positionClass": "toast-top-center",
                        "showDuration": "400",
                        "hideDuration": "1000",
                        "timeOut": "7000",
                        "extendedTimeOut": "1000",
                        "showEasing": "swing",
                        "hideEasing": "linear",
                        "showMethod": "fadeIn",
                        "hideMethod": "fadeOut"
                    }
                    toastr["error"](msg);

                };
            </script> 
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


