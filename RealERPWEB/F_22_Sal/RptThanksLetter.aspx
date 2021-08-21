<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptThanksLetter.aspx.cs" Inherits="RealERPWEB.F_22_Sal.RptThanksLetter" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {
            
            $('.chzn-select').chosen({ search_contains: true });

        }

      </script>

    

    <table style="width: 100%;">
        <tr>
            <td>
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
                                                <div class=" col-md-4  pading5px asitCol4">

                                                    <asp:Label ID="Label2" CssClass="lblTxt lblName" runat="server" Text="Form Date"></asp:Label>

                                                    <asp:TextBox ID="txtfrmdate" runat="server" CssClass=" inputtextbox"
                                                        TabIndex="1"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server"
                                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>

                                                    <asp:Label ID="lblDate" CssClass="smLbl_to" runat="server" Text="Date"></asp:Label>
                                                    <asp:TextBox ID="txtfromdate" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class=" col-md-3  pading5px asitCol3">

                                                    <asp:Label ID="Label5" CssClass="lblTxt lblName" runat="server" Text="Project Name:"></asp:Label>
                                                    <asp:TextBox ID="txtSrcPro" runat="server" CssClass=" inputtextbox"></asp:TextBox>


                                                    <asp:LinkButton ID="ibtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindProject_Click" TabIndex="12"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                                </div>


                                                <div class="col-md-4 pading5px">
                                                    <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="chzn-select form-control inputTxt" TabIndex="13" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    
                                                </div>


                                            </div>
                                            <div class="form-group">
                                                <div class=" col-md-3  pading5px asitCol3">

                                                    <asp:Label ID="Label1" CssClass="lblTxt lblName" runat="server" Text="Customer Name"></asp:Label>
                                                    <asp:TextBox ID="TextBox1" runat="server" CssClass=" inputtextbox"></asp:TextBox>


                                                    

                                                </div>


                                                <div class="col-md-4 pading5px">
                                                    <asp:DropDownList ID="ddlCustName" runat="server" CssClass="form-control inputTxt" TabIndex="13" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    
                                                </div>


                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                        </div>




                        <%--<table style="width: 100%;">
                            <tr>
                                <td colspan="11">
                                    <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid"
                                        BorderWidth="1px">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="style29" width="100px">&nbsp;</td>
                                                <td class="style57">
                                                    <asp:TextBox ID="txtfrmdate" runat="server" AutoCompleteType="Disabled"
                                                        BorderStyle="None" Width="80px"></asp:TextBox>

                                                </td>
                                                <td class="style58">
                                                    <asp:Label ID="lblDate" runat="server" Font-Bold="True"
                                                        Style="text-align: right" Text="Date:" CssClass="style34" Font-Size="12px"></asp:Label>
                                                </td>
                                                <td align="left" class="style32">
                                                    <asp:TextBox ID="txtfromdate" runat="server" CssClass="txtboxformat"
                                                        Font-Bold="True" Width="100px" BorderStyle="None"></asp:TextBox>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="style29" width="100px">
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Project Name:"
                                                        Style="text-align: right" Width="80px" CssClass="style34" Font-Size="12px"></asp:Label>
                                                </td>
                                                <td class="style57">
                                                    <asp:TextBox ID="txtSrcPro" runat="server" CssClass="txtboxformat"
                                                        Width="100px" BorderStyle="None"></asp:TextBox>
                                                </td>
                                                <td class="style58">
                                                    <asp:ImageButton ID="ibtnFindProject" runat="server" Height="18px"
                                                        ImageUrl="~/Image/find_images.jpg" OnClick="ibtnFindProject_Click"
                                                        Style="width: 18px" />
                                                </td>
                                                <td valign="top" class="style32">
                                                    <asp:DropDownList ID="ddlProjectName" runat="server"
                                                        Font-Bold="True" Font-Size="12px" Width="350px" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="style29" width="100px">&nbsp;</td>
                                                <td align="right" class="style27" colspan="2">
                                                    <asp:Label ID="Label6" runat="server" CssClass="style34" Font-Bold="True"
                                                        Style="text-align: right; margin-right: 0px;" Text="Customer Name:"
                                                        Width="121px" Font-Size="12px"></asp:Label>
                                                </td>
                                                <td class="style32" valign="top">
                                                    <asp:DropDownList ID="ddlCustName" runat="server" Font-Bold="True"
                                                        Font-Size="12px" Width="350px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="11">&nbsp;</td>
                            </tr>
                            <tr>
                                <td></td>
                                <td class="style50"></td>
                                <td class="style15"></td>
                                <td class="style20">
                                    
                                </td>
                                <td></td>
                                <td></td>
                                <td class="style53"></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td class="style20"></td>
                                <td class="style15"></td>
                                <td class="style20">
                                    <cc1:CalendarExtender ID="csefdate" runat="server" Format="dd-MMM-yyyy ddd"
                                        TargetControlID="txtfromdate"></cc1:CalendarExtender>
                                </td>
                                <td></td>
                                <td></td>
                                <td class="style56"></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td class="style50"></td>
                                <td class="style15"></td>
                                <td class="style20"></td>
                                <td></td>
                                <td></td>
                                <td class="style53"></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td class="style50"></td>
                                <td class="style15"></td>
                                <td class="style20"></td>
                                <td></td>
                                <td></td>
                                <td class="style53"></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td class="style50"></td>
                                <td class="style15"></td>
                                <td class="style20"></td>
                                </td>
                                <td></td>
                                <td></td>
                                <td class="style53"></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>--%>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>



