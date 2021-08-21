<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="InterfaceAtt.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_92_Mgt.InterfaceAtt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .modal-dialog {
            margin: 44px auto;
            width: 100%;
        }

        .InBox {
            color: red !important;
        }

        .ServProdInfo .panel-body {
            padding: 0 5px 2px;
        }

        .ServProdInfo label {
            margin-bottom: 0;
        }

        .ServProdInfo .panel {
            margin-bottom: 5px;
        }

        .ServProdInfo .panel-heading {
            padding: 1px 15px;
            font-weight: bold;
            font-size: 16px;
        }

        .modal-title {
            font-weight: bold;
            color: #000;
        }

            .modal-title span {
                color: red;
            }

        .wrntLbl {
            float: right;
            width: 60%;
            background: #DFF0D8;
            border: 1px solid #DFF0D8;
        }

        .contentPart .ServProdInfo .form-group {
            overflow: hidden;
        }

        .OverAll {
            /*animation-name: example;
            animation-duration: 4s;
            animation-iteration-count: 5;*/
            /*font-size: 18px;*/
            color: black;
            font-size: 14px;
            text-align: center !important;
            margin-top: 0px;
        }


        ul.sidebarMenu {
            margin: 0;
            padding: 0;
            width: 115%;
        }

            ul.sidebarMenu li {
                display: block;
                height: 30px;
                list-style: none;
                border: 1px solid #DFF0D8;
                border-bottom: 0;
            }

                ul.sidebarMenu li:last-child {
                    border-bottom: 1px solid #DFF0D8;
                }

                ul.sidebarMenu li a {
                    text-align: left;
                    display: block;
                    line-height: 30px;
                    font-size: 14px;
                    font-family: Calibri;
                }

                ul.sidebarMenu li h4 {
                    line-height: 50px;
                    text-align: center;
                    display: block;
                }

                ul.sidebarMenu li a:hover {
                    background: #D7E6D1;
                    color: black;
                }

        ul.tbMenuWrp {
            margin: 0;
            padding: 0;
            border: 0;
            background: none !important;
        }

            ul.tbMenuWrp li {
                height: 50px;
                width: 155px;
                padding: 0px 0;
                float: left;
                list-style: none;
                margin: 0 2px;
                color: #fff;
                background: #5F5F5F;
                -webkit-border-radius: 4px;
                -moz-border-radius: 4px;
                border-radius: 4px;
            }

                ul.tbMenuWrp li a {
                    padding: 0 0;
                    height: 50px;
                    background: #5F5F5F;
                    -webkit-border-radius: 4px;
                    -moz-border-radius: 4px;
                    border-radius: 4px;
                    display: block;
                    color: #fff;
                    padding: 0px 0 0 0;
                    vertical-align: middle;
                    border: none !important;
                }

                    ul.tbMenuWrp li a:hover {
                        background: #12A5A6;
                    }

                    ul.tbMenuWrp li a:focus {
                        outline: none;
                        outline-offset: 0;
                    }

                    ul.tbMenuWrp li a label {
                        color: #fff;
                        background: none;
                        border: none;
                        text-align: center;
                        font-weight: bold;
                        font-size: 16px;
                        display: block;
                        cursor: pointer;
                        width: 100%;
                    }

        .tbMenuWrp > li.active > a, .tbMenuWrp > li.active > a:focus, .tbMenuWrp > li.active > a:hover {
            background: #199698;
            color: #fff;
        }


        .tbMenuWrp table tr td {
            background: #fff none repeat scroll 0 0;
            border: 2px solid #d1d735;
            border-radius: 7px;
            color: #fff;
            cursor: pointer;
            float: left;
            height: 65px;
            list-style: outside none none;
            margin: 0 7px;
            padding: 0;
            position: relative;
            text-align: center;
            width: 195px;
        }

            .tbMenuWrp table tr td:nth-child(1) {
                background: #3BA8E0;
            }

            .tbMenuWrp table tr td:nth-child(2) {
                background: #5EB75B;
            }

            .tbMenuWrp table tr td:nth-child(3) {
                background: #EFAD4D;
            }

            .tbMenuWrp table tr td:nth-child(4) {
                background: #D95350;
            }

            .tbMenuWrp table tr td:nth-child(5) {
                background: #76C9B5;
            }

            .tbMenuWrp table tr td:nth-child(6) {
               background: #769bf4 none repeat scroll 0 0;
width: 72px !important;
            }

            .tbMenuWrp table tr td:nth-child(7) {
                background: #00CBF3;
            }

            .tbMenuWrp table tr td:nth-child(8) {
                background: #4BCF9E;
            }

        /*.tbMenuWrp table tr td:nth-child(7) {
                width: 115px;
                padding: 0 3px;
            }

            .tbMenuWrp table tr td:nth-child(8) {
                width: 115px;
                padding: 0 3px;
            }

            .tbMenuWrp table tr td:nth-child(9) {
                width: 115px;
                padding: 0 3px;
            }*/


        table.grvContentarea tr td span.glyphicon {
            margin: 0 4px;
        }

        .tbMenuWrp table tr td label {
            color: #000;
            cursor: pointer;
            font-weight: bold;
            height: 100%;
            margin: 1px 0;
            padding: 2px;
            width: 100%;
        }

            .tbMenuWrp table tr td label.active > a, .tbMenuWrp table tr td label.active > .tbMenuWrp table tr td label:focus, .tbMenuWrp table tr td label.active > a:hover {
                /*background: #12A5A6;*/
                /*color: #fff;*/
            }


        .tbMenuWrp table tr td input[type="checkbox"], input[type="radio"] {
            display: none;
        }

        .tabMenu a {
            display: block;
            line-height: 15px;
            font-size: 14px;
            color: #000;
            text-align: center;
            background-color: #00ff21;
        }

        .tbMenuWrp table tr td label span.lbldata {
            border: 2px solid #fff;
            border-radius: 50%;
            color: #fff;
            display: inline-block;
            float: left;
            font-size: 17px;
            font-weight: bold;
            padding: 2px;
            position: absolute;
            right: 4px;
            top: 7px;
        }

        .rptPurInt span.lbldata2 {
            background: #e5dcdd none repeat scroll 0 0;
            border: 1px solid #3ba8e0;
            display: block;
            font-size: 12px;
            line-height: 22px;
            margin: 14px 0 0;
            padding: 0;
            text-align: center;
        }

        .tbMenuWrp table tr td label .lblactive {
            /*background: #12A5A6;
            color: #fff;*/
        }

        .grvContentarea tr td:last-child {
            /*width: 120px;*/
        }

       .nav-tabs {
  border-bottom: none !important;
}

        .fan {
            border: 2px solid #f3b728;
            border-radius: 50%;
            display: inline-block;
            float: left;
            font-size: 18px;
            margin-top: 4px;
            padding: 2px;
        }

            .fan:nth-child(1) {
                background: #FF9C40 !important;
                color: #fff;
                border: 2px solid #E4DDDD;
            }

            .fan:nth-child(2) {
                color: #E49015;
                background-color: #5EB75A !important;
            }

            .fan:nth-child(3) {
                color: #fff;
                background: #085407 !important;
            }

            .fan:nth-child(4) {
                color: #fff;
                background: #DA3F40 !important;
                border: 2px solid #E4DDDD;
            }

            .fan:nth-child(5) {
                color: #fff;
                background: #009BFF !important;
                border: 2px solid #E4DDDD;
            }

            .fan:nth-child(6) {
                color: #E4DDDD;
                background: #539250 !important;
                border: 2px solid #E4DDDD;
            }

            .fan:nth-child(1) {
                color: #E4DDDD;
                background: #E79956 !important;
                border: 2px solid #E4DDDD;
            }

            .fan:nth-child(1) {
                color: #fff;
                background: #459A42 !important;
                border: 2px solid #E4DDDD;
            }




        /*.modalPopup{
            top:185px !important;
        }*/
    </style>
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

    <div class="container moduleItemWrpper ServProdInfo">
        <div class="contentPart">
            <div class="row">
                <fieldset class="scheduler-border fieldset_A ">
                    <div class="form-group">
                        <div class="col-md-3 pading5px">
                            <asp:Label ID="Label1" runat="server" CssClass=" smLbl_to">Date</asp:Label>

                            <asp:TextBox ID="txtFdate" runat="server" CssClass="inputTxt inputName inPixedWidth120 " AutoPostBack="true"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1_txtFdate" runat="server" Enabled="True"
                                Format="dd-MMM-yyyy" TargetControlID="txtFdate"></cc1:CalendarExtender>

                            <asp:LinkButton ID="lnkbtnok" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>


                        </div>
                    </div>
                    <div class="clearfix"></div>
                </fieldset>
            </div>
            <div class="row">
                <asp:Panel ID="pnlInterf" runat="server">
                    <div id="slSt" class="col-md-12 ServProdInfo">
                        <div class="panel with-nav-tabs panel-primary">
                            <fieldset class="tabMenu">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="tbMenuWrp nav nav-tabs rptPurInt">
                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0"></asp:ListItem>
                                                <asp:ListItem Value="1"></asp:ListItem>
                                                <asp:ListItem Value="2"></asp:ListItem>
                                                <asp:ListItem Value="3"></asp:ListItem>
                                                <asp:ListItem Value="4"></asp:ListItem>
                                                <asp:ListItem Value="5"></asp:ListItem>

                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <div style="margin-top:5px;">
                                <asp:Panel ID="pnlTStaff" runat="server" >


                                    <div class="table-responsive">

                                        <asp:Repeater ID="rplellaabsemp" runat="server">
                                            <HeaderTemplate>
                                                <table id="tblrplellaabsemp" class="table-striped table-hover table-bordered grvContentarea">
                                                    <tr>
                                                        <th rowspan="2">SL</th>
                                                        <th colspan="2" style="background-color: #109618; font-size: 14px; color: #fff;">
                                                            <asp:Label ID="Label4" runat="server" Text="On-Time Present"></asp:Label></th>

                                                        <th colspan="2" style="background-color: #DC3912; font-size: 14px; color: #fff;">
                                                            <asp:Label ID="lblrphamt01" runat="server" Text="Late"></asp:Label></th>
                                                        <th colspan="2" style="background-color: #DC3912; font-size: 14px; color: #fff;">
                                                            <asp:Label ID="lblrphamt02" runat="server" Text="Last Day"></asp:Label></th>
                                                        <th colspan="2" style="background-color: #FF9900; font-size: 14px; color: #fff;">


                                                            <asp:Label ID="lblrphamt03" runat="server" Text="Early Leave"></asp:Label></th>
                                                        <th colspan="2" style="background-color: #990099; font-size: 14px; color: #fff;">
                                                            <asp:Label ID="lblrphamt04" runat="server" Text="On Leave"></asp:Label></th>
                                                        <th colspan="2" style="background-color: #8D1517; font-size: 14px; color: #fff;">
                                                            <asp:Label ID="lblrphamt05" runat="server" Text="Absent"></asp:Label></th>



                                                    </tr>

                                                    <tr>

                                                        <th style="width: 120px;">Name</th>
                                                        <th style="width: 50px;">Comments</th>
                                                        <th style="width: 120px;">Name</th>
                                                        <th style="width: 50px;">Comments</th>
                                                        <th style="width: 100px;">Out Time</th>
                                                        <th style="width: 80px;">Reason</th>
                                                        <th style="width: 130px;">Name</th>
                                                        <th style="width: 60px;">Comments</th>
                                                        <th style="width: 130px;">Name</th>
                                                        <th style="width: 80px;">Comments</th>
                                                        <th style="width: 130px;">Name</th>
                                                        <th style="width: 40px;">Comments</th>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right" Text='<%# Convert.ToString(Container.ItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "pempname"))  %>'
                                                            Width="100px"></asp:Label>
                                                    </td>

                                                    <td>
                                                        <asp:Label ID="Label3" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "pcomm"))  %>'
                                                            Width="50px"></asp:Label>
                                                    </td>

                                                    <td>
                                                        <asp:Label ID="lrplempname" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "lempname"))  %>'
                                                            Width="110px"></asp:Label>
                                                    </td>

                                                    <td>
                                                        <asp:Label ID="lrplcomm" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "lcomm"))  %>'
                                                            Width="50px"></asp:Label>
                                                    </td>


                                                    <td>
                                                        <asp:Label ID="lrpllastdayst" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "llastdayst"))  %>'
                                                            Width="52px"></asp:Label>
                                                    </td>

                                                    <td>
                                                        <asp:Label ID="lrplresaon" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "lresaon"))  %>'
                                                            Width="80px"></asp:Label>
                                                    </td>


                                                    <td>
                                                        <asp:Label ID="lrpelempname" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "elempname"))  %>'
                                                            Width="100px"></asp:Label>
                                                    </td>

                                                    <td>
                                                        <asp:Label ID="lrpelcomm" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "elcomm"))  %>'
                                                            Width="50px"></asp:Label>
                                                    </td>

                                                    <td>
                                                        <asp:Label ID="lrpolempname" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "olempname"))  %>'
                                                            Width="100px"></asp:Label>
                                                    </td>

                                                    <td>
                                                        <asp:Label ID="lrpolcomm" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "olcomm"))  %>'
                                                            Width="80px"></asp:Label>
                                                    </td>

                                                    <td>
                                                        <asp:Label ID="lrpaempname" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "aempname"))  %>'
                                                            Width="100px"></asp:Label>
                                                    </td>

                                                    <td>
                                                        <asp:Label ID="lrpacomm" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "acomm"))  %>'
                                                            Width="40px"></asp:Label>
                                                    </td>

                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>


                                        </asp:Repeater>


                                    </div>


                                </asp:Panel>
                                <asp:Panel ID="pnlPresent" Visible="false" runat="server">
                                    <h3>pnlPresent</h3>

                                </asp:Panel>

                                <asp:Panel ID="pnlLate" Visible="false" runat="server">
                                    <h3>pnlLate</h3>

                                </asp:Panel>
                                <asp:Panel ID="pnlErLeave" Visible="false" runat="server">
                                    <h3>pnlErLeave</h3>

                                </asp:Panel> 
                                <asp:Panel ID="pnlLeave" Visible="false" runat="server">
                                    <h3>pnlLeave</h3>


                                </asp:Panel>

                                <asp:Panel ID="pnlAbsnt" Visible="false" runat="server">
                                    <h3>pnlAbsnt</h3>


                                </asp:Panel>


                            </div>
                        </div>
                    </div>
                    <asp:Label ID="lblprintstkl" runat="server"></asp:Label>

                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>

