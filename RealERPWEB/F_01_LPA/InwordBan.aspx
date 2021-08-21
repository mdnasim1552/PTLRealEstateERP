<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="InwordBan.aspx.cs" Inherits="RealERPWEB.F_01_LPA.InwordBan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">

        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">
                                <div class="form-group">

                                    <div class="col-md-3 asitCol3 pading5px">

                                         <asp:Label ID="lblinword" runat="server" CssClass="lblTxt lblName" Font-Size="11px" Width="500px"></asp:Label>
                                        <asp:Label ID="Label16" runat="server" CssClass="lblTxt lblName" Font-Size="11px "></asp:Label>

                                        <asp:TextBox ID="txtamt" runat="server" CssClass="inputtextbox" Width="100px"></asp:TextBox>
                                         <div class="col-md-1 asitCol1 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_Click" CssClass="btn btn-primary primaryBtn" BorderStyle="None"
                                            TabIndex="6">In word</asp:LinkButton>
                                        </div>
                                        <%--<asp:LinkButton ID="ibtnFindSubConName" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindSubConName_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>--%>

                                    </div>

                                   


                                </div>
                               




                                </div>
                            </div>
                        </fieldset>


                    </div>
                    
                       



                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

