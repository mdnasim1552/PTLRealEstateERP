
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="TransferClient.aspx.cs" Inherits="RealERPWEB.F_39_MyPage.TransferClient" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">

                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName">Date</asp:Label>
                                        <asp:TextBox ID="txtDate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDate">
                                        </cc1:CalendarExtender>
                                    </div>

                                    <div class="col-md-3 pading5px">
                                        <div class="msgHandSt">
                                            <asp:Label ID="lblmsg" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>
                                        </div>


                                    </div>



                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblSalesTeam" runat="server" CssClass="lblTxt lblName">Sales Team</asp:Label>
                                        <asp:TextBox ID="txtSrchSalesTeam" runat="server" CssClass=" inputtextbox" TabIndex="4"></asp:TextBox>



                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="lbtnSearchSalesTeam" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="lbtnSearchSalesTeam_Click" TabIndex="5"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>

                                    </div>

                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlSalesTeam" runat="server" CssClass="form-control inputTxt" OnSelectedIndexChanged="ddlSalesTeam_SelectedIndexChanged" AutoPostBack="true" TabIndex="6">
                                        </asp:DropDownList>

                                    </div>



                                    <div class="col-md-3 pading5px asitCol3">
                                        <div class="colMdbtn pading5px">
                                            <asp:LinkButton ID="lnkok" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkok_Click">Ok</asp:LinkButton>

                                        </div>




                                    </div>



                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblClient" runat="server" CssClass="lblTxt lblName">Client</asp:Label>
                                        <asp:TextBox ID="txtSrchClient" runat="server" CssClass=" inputtextbox" TabIndex="1"></asp:TextBox>



                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="lbtnSearchClient" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="lbtnSearchClient_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>

                                    </div>

                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlClientList" runat="server" CssClass="form-control inputTxt"  TabIndex="3">
                                        </asp:DropDownList>

                                    </div>



                                    <div class="col-md-3 pading5px asitCol3">
                                    </div>



                                </div>
                            </div>




                        </fieldset>


                        <asp:Panel ID="Panel2" runat="server" Visible="False">
                            <fieldset class="scheduler-border fieldset_B">

                                <div class="form-horizontal">


                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblSalesTeamNew" runat="server" CssClass="lblTxt lblName">New Sales Team:</asp:Label>
                                            <asp:TextBox ID="txtSrchSalesTeamNew" runat="server" CssClass=" inputtextbox" TabIndex="4"></asp:TextBox>



                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="lbtnSearchSalesTeamNew" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="lbtnSearchSalesTeamNew_Click" TabIndex="5"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                            </div>

                                        </div>

                                        <div class="col-md-4 pading5px">
                                            <asp:DropDownList ID="ddlSalesTeamNew" runat="server" CssClass="form-control inputTxt" TabIndex="6">
                                            </asp:DropDownList>

                                        </div>



                                        <div class="col-md-3 pading5px asitCol3">
                                            <div class="colMdbtn pading5px">
                                                <asp:LinkButton ID="Update" runat="server" CssClass="btn btn-danger  primaryBtn" OnClick="Update_Click">Update</asp:LinkButton>

                                            </div>




                                        </div>



                                    </div>

                                </div>




                            </fieldset>
                        </asp:Panel>
                    </div>
                </div>
            </div>


        </ContentTemplate>
            </asp:UpdatePanel>
</asp:Content>


