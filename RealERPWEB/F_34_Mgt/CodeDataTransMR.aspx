<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="CodeDataTransMR.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.CodeDataTransMR" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            try
            {

                $("input, select").bind("keydown", function (event) {
                    var k1 = new KeyPress();
                    k1.textBoxHandler(event);

                });

                $('.chzn-select').chosen({ search_contains: true });

             <%--   var Type=(<%=this.Request.QueryString["Type"].ToString()%>);

                alert(Type);--%>
            }

            catch(e)
            {
            
                alert(e.message);
            
            }
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
                            <div class="form-horizontal">
                                <asp:Panel ID="Panel1" runat="server">

                                    <div class="form-group">
                                        <div class="col-md-10  pading5px  asitCol10">

                                            <asp:Label ID="Label12" runat="server" CssClass=" lblName lblTxt" Text="Present Code:"></asp:Label>

                                        </div>
                                    </div>

                                    <div class="form-group">

                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblcontrolAccHead" runat="server" CssClass=" lblName lblTxt" Text="Accounts Code:"></asp:Label>

                                            <asp:TextBox ID="txtserceacc" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                            <asp:LinkButton ID="imgbtnFindAccount" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindAccount_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                        </div>

                                        <div class="col-md-5  pading5px  asitCol5">

                                            

                                            <asp:DropDownList ID="ddlAccHead" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlAccHead_SelectedIndexChanged"
                                                Width="400px" CssClass="chzn-select ddlPage">
                                            </asp:DropDownList>

                                            <asp:Label ID="lblAccCodedesc" runat="server" Visible="False" Width="400px" CssClass="inputtextbox"></asp:Label>

                                          


                                        </div>

                                        <div class="col-md-1 pading5px">
                                              
                                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                            

                                        </div>

                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-10  pading5px  asitCol10">

                                            <asp:Label ID="lbltxtUnitCode" runat="server" CssClass=" lblName lblTxt" Text="Unit Code:"></asp:Label>

                                            <asp:TextBox ID="txtserDetailsCode" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                            <asp:LinkButton ID="imgbtnFindDetailsCode" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="imgbtnFindDetailsCode_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                            <asp:DropDownList ID="ddlUnitcode" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlUnitcode_SelectedIndexChanged" Width="400px" CssClass=" chzn-select ddlPage">
                                            </asp:DropDownList>

                                            <asp:Label ID="lblUnitCodedesc" runat="server" Visible="False" Width="400px" CssClass="inputtextbox"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-10  pading5px  asitCol10">

                                            <asp:Label ID="lbltxtMr" runat="server" CssClass=" lblName lblTxt" Text="Money Receipt:"></asp:Label>

                                            <asp:TextBox ID="txtserMr" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                            <asp:LinkButton ID="imgbtnFindMr" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="imgbtnFindMr_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                            <asp:DropDownList ID="ddlMR" runat="server" Width="400px" CssClass="chzn-select ddlPage"></asp:DropDownList>

                                            <asp:Label ID="lblMRdesc" runat="server" Visible="False" Width="400px" CssClass="inputtextbox"></asp:Label>
                                        </div>
                                    </div>

                                </asp:Panel>
                                <asp:Panel ID="PnlNewCode" runat="server" Visible="False">

                                    <div class="form-group">
                                        <div class="col-md-10  pading5px  asitCol10">

                                            <asp:Label ID="Label13" runat="server" CssClass=" lblName lblTxt" Text="New Code:"></asp:Label>
                                            <asp:Label ID="lblmsg1" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>

                                        </div>
                                    </div>

                                    <div class="form-group">
                                        
                                          <div class="col-md-3 pading5px asitCol3">
                                              <asp:Label ID="lblcontrolAccHeadN" runat="server" CssClass=" lblName lblTxt" Text="Accounts Code:"></asp:Label>

                                            <asp:TextBox ID="txtserceaccN" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                            <asp:LinkButton ID="imgbtnFindAccountN" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindAccountN_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                              
                                               </div>
                                        
                                        <div class="col-md-5  pading5px  asitCol5">

                                           

                                            <asp:DropDownList ID="ddlAccHeadN" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAccHeadN_SelectedIndexChanged"
                                                Width="400px" CssClass=" chzn-select ddlPage">
                                            </asp:DropDownList>

                                            <asp:Label ID="Label2" runat="server" Visible="False" Width="400px" CssClass="inputtextbox"></asp:Label>

                                         
                                        </div>

                                        <div class="col-md-1 pading5px">

                                               <asp:LinkButton ID="lnkFinalUpdate" runat="server" CssClass="btn btn-primary  primaryBtn" OnClick="lnkFinalUpdate_Click">Final Update</asp:LinkButton>

                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-10  pading5px  asitCol10">

                                            <asp:Label ID="lbltxtUnitCodeN" runat="server" CssClass=" lblName lblTxt" Text="Unit Code:"></asp:Label>

                                            <asp:TextBox ID="txtserDetailsCodeN" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                            <asp:LinkButton ID="imgbtnFindDetailsCodeN" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="imgbtnFindDetailsCodeN_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                            <asp:DropDownList ID="ddlMrN" runat="server"
                                                Visible="False" Width="400px" CssClass="chzn-select ddlPage">
                                            </asp:DropDownList>


                                        </div>
                                    </div>


                                    <div class="form-group">
                                        <div class="col-md-10  pading5px  asitCol10">

                                            <asp:Label ID="lbltransferamt" Visible="false" runat="server" CssClass=" lblName lblTxt" Text="Transfer Amount:"></asp:Label>

                                            <asp:TextBox ID="txttransferamt"  Visible="false"  runat="server" CssClass="inputtextbox txtAlgRight" Style="text-align:right"></asp:TextBox>


                                          


                                        </div>
                                    </div>

                                </asp:Panel>
                            </div>
                        </fieldset>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

