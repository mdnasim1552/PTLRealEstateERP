<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="CompImg.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.CompImg" %>

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
                <div class="contentPart contentPartSmall">
                   
                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-6 pading5px">
                                        <asp:Label ID="Label9" runat="server" CssClass="lblTxt lblName">User List:</asp:Label>
                                        <asp:TextBox ID="txtUserSrc" runat="server" CssClass="inputTxt inputName"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnUserList" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnUserList_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        <asp:DropDownList ID="ddlCompName" Width="220" runat="server" CssClass="form-control inputTxt"
                                            >
                                        </asp:DropDownList>

                                    </div>


                                    <div class="col-md-3 pading5px pull-right">
                                        <div class="msgHandSt">
                                            <asp:Label ID="lblmsg" CssClass="btn-danger btn disabled primaryBtn " runat="server" Visible="false"></asp:Label>
                                        </div>


                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="Label10" runat="server" CssClass="lblTxt lblName">User Image:</asp:Label>
                                        
                                         <asp:FileUpload ID="imgFileUpload" runat="server" Height="26px" style="margin-left:10px"
                                                    onchange="submitform();" Width="216px" />
                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                      <asp:Image CssClass="img-thumbnail" Width="100" Height="100" ID="ComImg" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group">
                                <asp:Panel ID="Panel3" runat="server">
                                    <div class="col-md-1"></div>
                                    <div class="col-md-3 pading5px asitCol3">
                                       

                                    </div>
 
                                    <div class="col-md-3 pading5px asitCol3">
                                        <a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=101")%>" class="btn btn-warning primaryBtn colMdbtn2MR"> Close</a>
                                       
                                        <asp:LinkButton ID="lbtnUpdateImg" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpdateImg_Click"
                                                    >Update</asp:LinkButton>
                                        </div>





                                    
                                </asp:Panel>
                                    </div>
                            </div>
                        </fieldset>
                    
                </div>
            </div>


            

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

