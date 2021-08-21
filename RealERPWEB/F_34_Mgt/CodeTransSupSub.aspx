<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="CodeTransSupSub.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.CodeTransSupSub" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <script type="text/javascript"  language="javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" language="javascript" src="../Scripts/KeyPress.js"></script>--%>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });
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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">
                                <div class="row">
                                    <fieldset class="scheduler-border fieldset_A">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="lblTxt lblName" Style="font-size: 11px;" Text="Trans. From Code"></asp:Label>
                                                    <asp:TextBox ID="txtserceacc" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>


                                                    <asp:LinkButton ID="imgbtnFindAccount" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindAccount_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                </div>
                                                <div class="col-md-4 pading5px ">
                                                    <asp:DropDownList ID="ddlHeadFrom" runat="server" CssClass="form-control inputTxt" AutoPostBack="true" OnSelectedIndexChanged="ddlAccHead_SelectedIndexChanged" TabIndex="3">
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:Label ID="lbltxtdetailsCode" runat="server" CssClass="lblTxt lblName" Style="font-size: 11px;" Text="Trans. To Code"></asp:Label>
                                                    <asp:TextBox ID="txtserDetailsCode" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>


                                                    <asp:LinkButton ID="imgbtnFindDetailsCode0" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindDetailsCode_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                </div>
                                                <div class="col-md-4 pading5px ">
                                                    <asp:DropDownList ID="ddlHeadTo" runat="server" CssClass="form-control inputTxt" TabIndex="3">
                                                    </asp:DropDownList>

                                                </div>
                                                <asp:LinkButton ID="lnkFinalUpdate" CssClass="btn btn-danger primaryBtn" runat="server" OnClick="lnkFinalUpdate_Click" TabIndex="2">Final Update</asp:LinkButton>


                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                               
                            </asp:View>
                            <asp:View ID="ViewGeneral" runat="server">
                                <div class="row">
                                    <fieldset class="scheduler-border fieldset_A">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:Label ID="lblfrmcodegen" runat="server" CssClass="lblTxt lblName" Style="font-size: 11px;" Text="Trans. From Code"></asp:Label>
                                                    <asp:TextBox ID="txtfrmcodegen" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>


                                                    <asp:LinkButton ID="imgbtnfrmcodegen" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnfrmcodegen_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                </div>
                                                <div class="col-md-4 pading5px ">
                                                    <asp:DropDownList ID="ddlHeadFromgen" runat="server" CssClass="form-control inputTxt" OnSelectedIndexChanged="ddlHeadFromgen_SelectedIndexChanged" TabIndex="3">
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:Label ID="lbltocodegen" runat="server" CssClass="lblTxt lblName" Style="font-size: 11px;" Text="Trans. To Code"></asp:Label>
                                                    <asp:TextBox ID="txttocodegen" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>


                                                    <asp:LinkButton ID="imgbtntocodegen" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtntocodegen_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                </div>
                                                <div class="col-md-4 pading5px ">
                                                    <asp:DropDownList ID="ddlHeadTogen" runat="server" CssClass="form-control inputTxt" TabIndex="3" >
                                                    </asp:DropDownList>

                                                </div>
                                                <asp:LinkButton ID="lnkFinalUpdateGen" CssClass="btn btn-danger primaryBtn" runat="server" OnClick="lnkFinalUpdateGen_Click" TabIndex="2">Final Update</asp:LinkButton>


                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </asp:View>
                        </asp:MultiView>
                </div>
            </div>







            

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

