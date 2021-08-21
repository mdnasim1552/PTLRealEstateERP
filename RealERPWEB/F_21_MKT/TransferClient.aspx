<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="TransferClient.aspx.cs" Inherits="RealERPWEB.F_21_MKT.TransferClient" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="CSS/Style.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>
    <script type="text/javascript" language="javascript" src="../Scripts/KeyPress.js"></script>

     <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });

        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });

         <%--  var gvclient = $('#<%=this.gvclient.ClientID %>');--%>

            gvclient.gridviewScroll({
                width: 1160,
                height: 420,
                arrowsize: 30,
                railsize: 16,
                barsize: 8,
                varrowtopimg: "../Image/arrowvt.png",
                varrowbottomimg: "../Image/arrowvb.png",
                harrowleftimg: "../Image/arrowhl.png",
                harrowrightimg: "../Image/arrowhr.png",
                freezesize: 10
            });





            $('.chzn-select').chosen({ search_contains: true });
        }

        function SearchSalesTeam() {

            var srchteam = "%" + $('#<%=this.txtSrchSalesTeam.ClientID%>').val() + "%";
            var userid = $('#<%=this.lbluseid.ClientID%>').text().trim();
            var objsalesteam = new RealERPScript();
            var lst = objsalesteam.GetEmpCode(srchteam, userid);
            var ddlemployee = $('#<%=this.ddlSalesTeam.ClientID %>');
            ddlemployee.children('option').remove();
            $.each(lst, function (index, lst) {
                ddlemployee.append('<option value="' + lst.empid + '">' + lst.empname + '</option>');
            });
        }
     </script>

    <div class="container moduleItemWrpper">
        <div class="contentPart">
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
            <div class="row">
                <fieldset class="scheduler-border fieldset_A">
                    <div class="form-horizontal">
                   
                        <asp:Panel ID="Panel1" runat="server">
                            <div class="form-group">
                                <div class="col-md-2  pading5px">
                                    <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName" Text="Date:"></asp:Label>
                                    <asp:TextBox ID="txtDate" runat="server" Width="80px" CssClass="inputtextbox"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                        Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>
                                    <cc1:ListSearchExtender ID="ListSearchExtender1" runat="server"
                                        QueryPattern="Contains" TargetControlID="ddlSalesTeam">
                                    </cc1:ListSearchExtender>
                                </div>
                       
                     
                                <div class="col-md-4  pading5px">
                                    <asp:Label ID="lblSalesTeam" runat="server" CssClass="smLbl_to" Text="Sales Team:"></asp:Label>
                                    <asp:TextBox ID="txtSrchSalesTeam" runat="server" CssClass="inputtextbox" Visible="false"></asp:TextBox>
                                    <asp:LinkButton ID="imgSearchSalesTeam" runat="server" OnClick="imgSearchSalesTeam_Click" TabIndex="4" CssClass="btn btn-primary srearchBtn" Visible="false" ><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                    <asp:DropDownList ID="ddlSalesTeam" runat="server" CssClass="ddlPage chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlSalesTeam_SelectedIndexChanged"  Width="280px"> </asp:DropDownList>
                                </div>
                    
                                <div class="col-md-4  pading5px">
                                    <asp:Label ID="lblClient" runat="server" CssClass="smLbl_to" Text="Client:"></asp:Label>
                                    <asp:TextBox ID="txtSrchClient" runat="server" CssClass="inputtextbox" Visible="false"></asp:TextBox>
                                    <asp:LinkButton ID="imgSearchClient" runat="server" OnClick="imgSearchClient_Click" Visible="false" TabIndex ="4" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                    <asp:DropDownList ID="ddlClientList" runat="server" CssClass="ddlPage chzn-select" AutoPostBack="True" Width="220px"></asp:DropDownList>
                                       <asp:LinkButton ID="lnkok" CssClass="btn btn-primary primaryBtn" runat="server" OnClick="lnkok_Click" TabIndex="1">Ok</asp:LinkButton>
                                    <asp:Label ID="lblmsg" runat="server" CssClass="btn-danger btn  primaryBtn"></asp:Label>
                                        <asp:Label ID="lbluseid" runat="server" CssClass="lblTxt lblName " Style="display: none;"></asp:Label>
                                </div>
                        </div>
                        </asp:Panel>

                        <asp:Panel ID="Panel2" runat="server" Visible="False">
                            <div class="form-group">
                                <div class="col-md-10  pading5px">
                                    <asp:Label ID="lblSalesTeamNew" runat="server" CssClass="lblTxt lblName" Text="New Sales Team:"></asp:Label>
                                    <asp:TextBox ID="txtSrchSalesTeamNew" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                    <asp:LinkButton ID="imgSearchSalesTeamNew" runat="server" OnClick="imgSearchSalesTeamNew_Click" TabIndex="4" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                    <asp:DropDownList ID="ddlSalesTeamNew" runat="server" CssClass="ddlPage" AutoPostBack="True"   Width="300px"></asp:DropDownList>
                                    <asp:LinkButton ID="Update" CssClass="btn btn-primary primaryBtn" runat="server" OnClick="Update_Click" TabIndex="1">Update</asp:LinkButton>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </fieldset>

            </div>
        </div>
    </div>




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
               <%--   <legend>
                                <asp:Label ID="Label1" runat="server" Text="Old Code Information" Font-Bold="True" Font-Size="18px"
                                    ForeColor="Yellow" Style="vertical-align: middle;"></asp:Label>
                            </legend>--%>
               <%--<asp:Panel ID="Panel1" runat="server">

                                <table style="width: 280%;">
                                    <tr>
                                        <td class="style63">
                                            <asp:Label ID="lblDate" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="White" Height="16px" Style="text-align: left" Text="Date:"
                                                Width="100px"></asp:Label>
                                        </td>
                                        <td class="style62">
                                            <asp:TextBox ID="txtDate" runat="server" CssClass="txtboxformat"
                                                Font-Size="11px" Width="100px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>
                                        </td>
                                        <td class="style66">&nbsp;</td>
                                        <td class="style68">
                                            <cc1:ListSearchExtender ID="ListSearchExtender1" runat="server"
                                                QueryPattern="Contains" TargetControlID="ddlSalesTeam">
                                            </cc1:ListSearchExtender>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style63">
                                            <asp:Label ID="lblSalesTeam" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: left" Text="Sales Team:" Width="100px"
                                                ForeColor="White"></asp:Label>
                                        </td>
                                        <td class="style62">
                                            <asp:TextBox ID="txtSrchSalesTeam" runat="server" BorderStyle="None"
                                                CssClass="txtboxformat" TabIndex="3" Width="100px"></asp:TextBox>
                                        </td>
                                        <td class="style66">
                                            <asp:ImageButton ID="imgSearchSalesTeam" runat="server" Height="16px"
                                                ImageUrl="~/Image/find_images.jpg" OnClick="imgSearchSalesTeam_Click"
                                                Style="margin-left: 0px" TabIndex="4" Width="16px" />
                                        </td>
                                        <td class="style68">
                                            <asp:DropDownList ID="ddlSalesTeam" runat="server" AutoPostBack="True"
                                                Font-Size="11px" OnSelectedIndexChanged="ddlSalesTeam_SelectedIndexChanged"
                                                Width="300px" TabIndex="5">
                                            </asp:DropDownList>

                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkok" runat="server" BackColor="#003399"
                                                BorderColor="#CCFFCC" BorderStyle="Solid" BorderWidth="1px" CssClass="button"
                                                Font-Bold="True" Font-Size="13px" ForeColor="White" Height="16px"
                                                OnClick="lnkok_Click" Width="63px" TabIndex="9">Ok</asp:LinkButton>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="style64">
                                            <asp:Label ID="lblClient" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="White" Style="text-align: left" Text="Client:" Width="100px"></asp:Label>
                                        </td>
                                        <td class="style60">
                                            <asp:TextBox ID="txtSrchClient" runat="server" BorderStyle="None"
                                                CssClass="txtboxformat" TabIndex="6" Width="100px"></asp:TextBox>
                                        </td>
                                        <td class="style67">
                                            <asp:ImageButton ID="imgSearchClient" runat="server" Height="16px"
                                                ImageUrl="~/Image/find_images.jpg" OnClick="imgSearchClient_Click"
                                                TabIndex="7" Width="16px" />
                                        </td>
                                        <td class="style69">
                                            <asp:DropDownList ID="ddlClientList" runat="server" AutoPostBack="True"
                                                Font-Size="11px"
                                                Width="300px" TabIndex="8">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="style59"></td>
                                    </tr>

                                    <tr>
                                        <td class="style17" colspan="2">&nbsp;</td>
                                        <td class="style66">&nbsp;</td>
                                        <td class="style68">&nbsp;</td>
                                        <td>
                                            <asp:Label ID="lblmsg" runat="server" BackColor="Red" Font-Bold="True"
                                                Font-Size="12px" ForeColor="White"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>--%>
               <%--<asp:Panel ID="Panel2" runat="server" BorderColor="Yellow" BorderStyle="Solid"
                        BorderWidth="1px" Visible="False">
                        <table style="width: 280%;">

                            <tr>
                                <td class="style63">
                                    <asp:Label ID="lblSalesTeamNew" runat="server" Font-Bold="True" Font-Size="12px"
                                        Style="text-align: left" Text="New Sales Team:" Width="115px"
                                        ForeColor="White"></asp:Label>
                                </td>
                                <td class="style62">
                                    <asp:TextBox ID="txtSrchSalesTeamNew" runat="server" BorderStyle="None"
                                        CssClass="txtboxformat" TabIndex="3" Width="100px"></asp:TextBox>
                                </td>
                                <td class="style66">
                                    <asp:ImageButton ID="imgSearchSalesTeamNew" runat="server" Height="16px"
                                        ImageUrl="~/Image/find_images.jpg" OnClick="imgSearchSalesTeamNew_Click"
                                        Style="margin-left: 0px" TabIndex="4" Width="16px" />
                                </td>
                                <td class="style68">
                                    <asp:DropDownList ID="ddlSalesTeamNew" runat="server" AutoPostBack="True"
                                        Font-Size="11px" OnSelectedIndexChanged="ddlSalesTeam_SelectedIndexChanged"
                                        Width="300px" TabIndex="5">
                                    </asp:DropDownList>

                                </td>
                                <td>
                                    <asp:LinkButton ID="Update" runat="server" BackColor="#003399"
                                        BorderColor="#CCFFCC" BorderStyle="Solid" BorderWidth="1px" CssClass="button"
                                        Font-Bold="True" Font-Size="13px" ForeColor="White" Height="16px"
                                        OnClick="Update_Click" TabIndex="9" Width="63px">Update</asp:LinkButton>
                                </td>
                            </tr>



                            <tr>
                                <td class="style17" colspan="2">&nbsp;</td>
                                <td class="style66">&nbsp;</td>
                                <td class="style68">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>--%>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

