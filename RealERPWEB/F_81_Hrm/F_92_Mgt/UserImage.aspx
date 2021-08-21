<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="UserImage.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_92_Mgt.UserImage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    
    
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
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label9" runat="server" CssClass="lblTxt lblName">User List:</asp:Label>
                                        <asp:TextBox ID="txtUserSrc" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnUserList" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ibtnUserList_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-4 pading5px ">
                                        <asp:DropDownList ID="ddlUserName" runat="server" CssClass="form-control inputTxt"
                                            >
                                        </asp:DropDownList>

                                    </div>


                                    <div class="col-md-3 pading5px pull-right">
                                        <div class="msgHandSt">
                                            <asp:Label ID="lblmsg" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>
                                        </div>


                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label10" runat="server" CssClass="lblTxt lblName">User Image:</asp:Label>
                                        <asp:Image ID="UserImg" runat="server" />

                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label11" runat="server" Text="User Image:" Width="100px"></asp:Label>

                                    </div>
                                </div>
                                <div class="form-group">
                                <asp:Panel ID="Panel3" runat="server">
                                    <div class="col-md-1"></div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:FileUpload ID="imgFileUpload" runat="server" Height="26px"
                                                    onchange="submitform();" Width="216px" />

                                    </div>
 
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:LinkButton ID="lbtnUpdateImg" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpdateImg_Click"
                                                    >Update</asp:LinkButton>
                                        </div>





                                    
                                </asp:Panel>
                                    </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>


            

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

