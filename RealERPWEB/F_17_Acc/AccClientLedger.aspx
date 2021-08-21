<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccClientLedger.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccClientLedger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });
           
          <%--  var dgv2 = $('#<%=this.dgv2.ClientID %>');
            dgv2.Scrollable();--%>
            


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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-group">
                                <div class="col-md-3 pading5px asitCol3">
                                </div>
                                <div class="col-md-3 pading5px pull-right">
                                    <div class="msgHandSt">
                                        <asp:Label ID="lblmsg" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                            </div>

                            <div class="form-group">
                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="lblTxt lblName" Text="Get Acc. Heads"></asp:Label>
                                    <asp:TextBox ID="txtAccSearch" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                    <div class="colMdbtn">
                                        <asp:LinkButton ID="lbtnSearchAcc" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="lbtnSearchAcc_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="col-md-4 pading5px ">
                                    <asp:DropDownList ID="ddlConAccHead" runat="server" CssClass="form-control inputTxt chzn-select">
                                    </asp:DropDownList>

                                </div>
                                <div class="col-md-3 pading5px asitCol3">
                                    <div class="colMdbtn pading5px">
                                        <asp:LinkButton ID="lnkShowLedger" runat="server" CssClass="btn btn-primary primaryBtn">Show</asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>

                            <div class="form-group">
                                <div class="col-md-3 pading5px asitCol3"></div>
                                <div class="col-md-4 pading5px">
                                    <asp:DropDownList ID="ddlCusHead" runat="server" CssClass="form-control inputTxt chzn-select">

                                    </asp:DropDownList>
                                </div>
                            </div>

                        </fieldset>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


