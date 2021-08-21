<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="TransectionPrint2.aspx.cs" Inherits="RealERPWEB.F_17_Acc.TransectionPrint2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
       .customradio table tr td input[type="checkbox"],.customradio input[type="radio"] {
    display: inline !important;
}
        .customradio label{
            display: inline !important;
        }
        ul.tbMenuWrp {
            margin: 0;
            padding: 0;
            border: 0;
            background: none !important;
        }

            ul.tbMenuWrp li {
                width: 140px;
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
            background: #472AC6 !important;
            color: #fff;
        }


        .tbMenuWrp table tr td {
            /*height: 50px;*/
            padding: 0 0;
            float: left;
            list-style: none;
            margin: 0 3px;
            color: #fff;
            text-align: center;
            /*border: 2px solid #D1D735;*/
            /*-webkit-border-radius: 30px;
            -moz-border-radius: 30px;
            border-radius: 30px;*/
            cursor: pointer;
            background: #fff;
            position: relative;
        }






        table.grvContentarea tr td span.glyphicon {
            margin: 0 4px;
        }

        .tbMenuWrp table tr td label {
            color: #000;
            cursor: pointer;
            font-weight: bold;
            height: 35px;
            margin: 1px 0;
            /*padding: 2px;*/
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
            background: #fff;
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
            background: #667DE8;
            color: #000000;
        }

        .lblactive label tr td {
            background: #667DE8 !important;
            color: #000 !important;
        }

        .blink_me {
            animation: blinker 5s linear infinite;
        }

        @keyframes blinker {
            50% {
                opacity: 0;
            }
        }

        .grvContentarea tr td:last-child {
            /*width: 120px;*/
        }


        .fan:nth-child(1) {
            background-color: #e6b0e1;
            color: #fff;
            height: 100%;
            line-height: 32px;
        }


        .fan {
            border-radius: 0;
            px display: inline-block;
            float: left;
            font-size: 18px;
            padding: 8px;
        }

            .fan:nth-child(1) {
                background-color: #817E24;
                border-bottom: 2px solid red;
                /* border-top: 2px solid red; */
                /* border-left: 3px solid #4800ff; */
                color: #fff;
                height: 35px;
                line-height: 14px;
            }

            .fan:nth-child(2) {
            }

            .fan:nth-child(3) {
            }

            .fan:nth-child(4) {
            }

            .fan:nth-child(5) {
            }

            .fan:nth-child(6) {
            }

            .fan:nth-child(7) {
            }
        /* for interface*/

        .circle-tile {
            margin-bottom: 15px;
            text-align: center;
            width: 125px;
            font-size: 10px;
            
        }
         .circle-tile_type {
            margin-bottom: 15px;
            text-align: center;
            width: 125px;
            font-size: 10px;
            
        }

        .circle-tile-heading {
            border: 3px solid rgba(255, 255, 255, 0.3);
            border-radius: 100%;
            color: #FFFFFF;
            font-size: 15px;
            height: 39px;
            margin: -2px auto -22px;
            padding: 4px 4px;
            position: relative;
            text-align: center;
            transition: all 0.3s ease-in-out 0s;
            width: 42px;
        }

            .circle-tile-heading .fa {
                line-height: 80px;
            }

        .circle-tile-content {
            padding-top: 8px;
            padding-bottom: 6px;
            /*border-radius: 0px 15px;*/
        }

        .circle-tile-number {
            font-size: 26px;
            font-weight: 700;
            line-height: 1;
            padding: 5px 0 15px;
        }

        .circle-tile-description {
            text-transform: capitalize;
        }

        .circle-tile-footer {
            background-color: rgba(0, 0, 0, 0.1);
            color: rgba(255, 255, 255, 0.5);
            display: block;
            padding: 5px;
            transition: all 0.3s ease-in-out 0s;
        }

            .circle-tile-footer:hover {
                background-color: rgba(0, 0, 0, 0.2);
                color: rgba(255, 255, 255, 0.5);
                text-decoration: none;
            }

        .circle-tile-heading.dark-blue:hover {
            background-color: #2E4154;
        }

        .circle-tile-heading.green:hover {
            background-color: #138F77;
        }

        .circle-tile-heading.orange:hover {
            background-color: #DA8C10;
        }

        .circle-tile-heading.blue:hover {
            background-color: #2473A6;
        }

        .circle-tile-heading.red:hover {
            background-color: #CF4435;
        }

        .circle-tile-heading.purple:hover {
            background-color: #7F3D9B;
        }

        .tile-img {
            text-shadow: 2px 2px 3px rgba(0, 0, 0, 0.9);
        }

        .dark-blue {
            background-color: #34495E;
        }

        .green {
            background-color: #16A085;
        }

        .blue {
            background-color: #2980B9;
        }

        .orange {
            background-color: #F39C12;
        }

        .red {
            background-color: #E74C3C;
        }

        .purple {
            background-color: #8E44AD;
        }

        .dark-gray {
            background-color: #7F8C8D;
        }

        .gray {
            background-color: #95A5A6;
        }

        .light-gray {
            background-color: #BDC3C7;
        }

        .yellow {
            background-color: #F1C40F;
        }

        .text-dark-blue {
            color: #34495E;
        }

        .text-green {
            color: #16A085;
        }

        .text-blue {
            color: #2980B9;
        }

        .text-orange {
            color: #F39C12;
        }

        .text-red {
            color: #E74C3C;
        }

        .text-purple {
            color: #8E44AD;
        }

        .text-faded {
            /*color: rgba(255, 255, 255, 0.7);*/
            color: #000;
        }

        .text-faded_type {
            /*color: rgba(255, 255, 255, 0.7);*/
            color: #FFFFFF;
        }

        .common_color {
            background-color: #faf5f5;
        }

        .common_color_type {
            background-color: #155273;
        }
    </style>

    

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
                        <div class="col-md-3 pading5px">
                            <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName">From</asp:Label>
                            <asp:TextBox ID="txtfromdate" runat="server" CssClass="inputTxt inputName inpPixedWidth" ToolTip="(dd.mmm.yyyy)"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>

                            <asp:Label ID="Label6" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                            <asp:TextBox ID="txttodate" runat="server" CssClass="inputTxt inputName inpPixedWidth"
                                TabIndex="1"></asp:TextBox>
                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="txttodate" TodaysDateFormat=""></cc1:CalendarExtender>
                            <div class="colMdbtn pading5px">
                                <asp:LinkButton ID="lnkbtnVouOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkbtnVouOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-5 pading5px">
                            <fieldset class="tabMenu">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="tbMenuWrp nav nav-tabs">
                                            <asp:RadioButtonList ID="RadioButtonList2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="AccVoucher"><div class='circle-tile'><div class='circle-tile-content common_color'><div class='circle-tile-description text-faded'>Voucher Print</div></div></div></asp:ListItem>
                                                <asp:ListItem Value="AccCheque"><div class='circle-tile'><div class='circle-tile-content common_color'><div class='circle-tile-description text-faded'>Cheque Print</div></div></div></asp:ListItem>
                                                <asp:ListItem Value="AccPostDatChq"><div class='circle-tile'><div class='circle-tile-content common_color'><div class='circle-tile-description text-faded'>Post Dated Cheque Print</div></div></div></asp:ListItem>
                                                <%--<asp:ListItem Value="AccVoucherCan"><div class='circle-tile'><div class='circle-tile-content common_color'><div class='circle-tile-description text-faded'>Voucher Cancellation</div></div></div></asp:ListItem>--%>
                                                <asp:ListItem Value="Payslip"><div class='circle-tile'><div class='circle-tile-content common_color'><div class='circle-tile-description text-faded'>Pay Slip</div></div></div></asp:ListItem>

                                            </asp:RadioButtonList>

                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                            </fieldset>
                        </div>

                        <div class="col-md-4 pading5px" runat="server" id="chqno" visible="false">
                            <asp:CheckBox ID="Chkpayment" runat="server" TabIndex="10" Text="Payment" CssClass="btn btn-primary checkBox" />
                            <asp:CheckBox ID="chktopsheet" runat="server" TabIndex="11" Text="Top Sheet" CssClass="btn btn-primary checkBox" />
                            <asp:Label ID="Label9" runat="server" CssClass="lblTxt lblName">Cheque No</asp:Label>
                            <asp:TextBox ID="txtSearchChequeno" runat="server" CssClass=" inputTxt inputName inpPixedWidth"> </asp:TextBox>
                             

                        </div>
                        <div class="col-md-1 pading5px" runat="server" id="paye" visible="false">
                            <asp:CheckBox ID="ChboxPayee" runat="server" CssClass="pull-right" Text="A/C Payee"  />
                            </div>
                       

                    </div>


                    <asp:MultiView ID="MultiView1" runat="server">

                        <asp:View ID="AccVoucher" runat="server">
                            <div class="row">

                                <asp:Panel ID="Panel1" runat="server">
                                    <fieldset class="scheduler-border">

                                        <div class="form-horizontal">
                                            
                                            <div class="row">
                                                <div class="col-md-12">
                                                <fieldset class="tabMenu">
                                                    <div class="form-horizontal">
                                                        <div class="form-group">
                                                            <div class="nav nav-tabs customradio">
                                                                <asp:RadioButtonList ID="rbtnList1" runat="server"  
                                                                    RepeatColumns="6" RepeatDirection="Horizontal" >
                                                                    <asp:ListItem>Bank Voucher</asp:ListItem>
                                                                    <asp:ListItem>Cash Voucher</asp:ListItem>
                                                                    <asp:ListItem>Journal Voucher</asp:ListItem>
                                                                    <asp:ListItem>Post Dated Cheque</asp:ListItem>
                                                                    <asp:ListItem>All Voucher</asp:ListItem>
                                                                    <asp:ListItem>Cancellation Voucher</asp:ListItem>


                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </fieldset>
                                                    </div>
                                            </div>

                                          


                                            <div class="form-group">

                                                <asp:ListBox ID="lstVouname" runat="server" CssClass="col-sm-4 form-control" Font-Bold="True"
                                                    Font-Size="12px" Height="297px" SelectionMode="Multiple" Width="350px"></asp:ListBox>
                                                <asp:LinkButton ID="lnkbtnDelVoucher" runat="server" CssClass="btn-danger" OnClick="lnkbtnDelVoucher_Click"
                                                    Visible="False" Width="60px">Delete</asp:LinkButton>
                                               
                                            </div>
                                        </div>
                                    </fieldset>
                                </asp:Panel>
                            </div>
                        </asp:View>
                        <asp:View ID="AccCheque" runat="server">
                            <div class="row">
                                <asp:Panel ID="Panel2" runat="server">

                                    <fieldset class="scheduler-border">

                                        <div class="form-horizontal">
                                            <%--<div class="form-group">
                                                <div class="col-md-4 pading5px">
                                                    <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName">From</asp:Label>
                                                    <asp:TextBox ID="txtfromdatec" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtfromdatec_CalendarExtender" runat="server"
                                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdatec"></cc1:CalendarExtender>

                                                    <asp:Label ID="Label8" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                                    <asp:TextBox ID="txttodatec" runat="server" CssClass="inputTxt inputName inpPixedWidth"
                                                        TabIndex="1"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txttodatec_CalendarExtender" runat="server"
                                                        Format="dd-MMM-yyyy" TargetControlID="txttodatec" TodaysDateFormat=""></cc1:CalendarExtender>

                                                   

                                                    <div class="colMdbtn pading5px">
                                                        <asp:LinkButton ID="lnkbtnChkOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkbtnChkOk_Click">Ok</asp:LinkButton>

                                                    </div>

                                                </div>


                                               
                                            </div>--%>
                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:Label ID="lblVoucher" runat="server" CssClass="lblTxt lblName">Voucher No</asp:Label>
                                                    <asp:TextBox ID="txtSearchCheqno" runat="server" CssClass=" inputTxt inputName inpPixedWidth"> </asp:TextBox>
                                                    <asp:LinkButton ID="imgbtnSearchChq" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="imgbtnSearchChq_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                </div>
                                                <div class="col-md-4 pading5px ">
                                                    <asp:DropDownList ID="ddlChkVouNo" runat="server" CssClass="form-control inputTxt">
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-md-4 pading5px ">
                                                    <asp:RadioButtonList ID="rbtCprintList" runat="server" RepeatColumns="6" RepeatDirection="Horizontal"
                                                        Visible="False">
                                                        <asp:ListItem>All Company</asp:ListItem>
                                                        <asp:ListItem>Rp Land</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>

                                </asp:Panel>
                                <asp:GridView ID="gvCheque" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Width="577px" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnPageIndexChanging="gvCheque_PageIndexChanging" PageSize="15">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Voucher #">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkbntUpPayto" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lnkbntUpPayto_Click">Final Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvvounum" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum2")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Voucher Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvVouDat" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Cheque Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvChequeDat" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chequedat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAmount" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0;(#,##0);  ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Pay To">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvPayto" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Style="text-align: left" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                                    Width="150px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server"
                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chk"))=="True" %>'
                                                    Width="20px"
                                                    Enabled='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chk"))=="False" %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>

                            </div>

                        </asp:View>
                        <asp:View ID="PostDatedCheque" runat="server">
                            <div class="row">
                                <asp:Panel ID="Panel4" runat="server">
                                    <fieldset class="scheduler-border">

                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-md-4 pading5px">
                                                    <%--<asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">From</asp:Label>
                                                    <asp:TextBox ID="txtfromdatec1" runat="server" CssClass="inputTxt inputName inpPixedWidth" ToolTip="(dd.mmm.yyyy)"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                        Enabled="True" Format="dd.MMM.yyyy" TargetControlID="txtfromdatec1"></cc1:CalendarExtender>

                                                    <asp:Label ID="Label2" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                                    <asp:TextBox ID="txttodatec1" runat="server" CssClass="inputTxt inputName inpPixedWidth"
                                                        TabIndex="1"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server"
                                                        Format="dd-MMM-yyyy" TargetControlID="txttodatec1" TodaysDateFormat=""></cc1:CalendarExtender>--%>

                                                  <%--  <asp:CheckBox ID="chkpayee" runat="server" CssClass="pull-right" Text="A/C Payee" />--%>

                                                    <%--<div class="colMdbtn pading5px">
                                                        <asp:LinkButton ID="btnPostDatChqOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="btnPostDatChqOk_Click">Ok</asp:LinkButton>

                                                    </div>--%>

                                                </div>


                                               <%-- <div class="col-md-3 pading5px pull-right">
                                                    <div class="msgHandSt">

                                                        <asp:Label ID="lmsg02" CssClass="btn-danger btn disabled" runat="server"></asp:Label>
                                                    </div>


                                                </div>--%>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:Label ID="Label10" runat="server" CssClass="lblTxt lblName">Voucher No</asp:Label>
                                                    <asp:TextBox ID="txtSearchPCheqno" runat="server" CssClass=" inputTxt inputName inpPixedWidth"> </asp:TextBox>
                                                    <asp:LinkButton ID="imgbtnSearchPChq" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="imgbtnSearchPChq_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                </div>
                                                <div class="col-md-4 pading5px ">
                                                    <asp:DropDownList ID="ddlPostDatedCheque" runat="server" CssClass="form-control inputTxt">
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-md-4 pading5px ">
                                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatColumns="6" RepeatDirection="Horizontal"
                                                        Visible="False">
                                                        <asp:ListItem>All Company</asp:ListItem>
                                                        <asp:ListItem>Rp Land</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>

                                    </fieldset>
                                </asp:Panel>

                                <asp:GridView ID="gvPostDatCheq" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Width="577px" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnPageIndexChanging="gvPostDatCheq_PageIndexChanging" PageSize="15">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Voucher #">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkbntUpPayto1" runat="server" Font-Bold="True"
                                                    CssClass="btn btn-danger primaryBtn" OnClick="lnkbntUpPayto1_Click">Final Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvvounum1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Voucher Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvVouDat1" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAmount1" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0;(#,##0);  ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cheque No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvChqNo" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cheque Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvChqDat1" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chequedat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pay To">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvPayto1" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Style="text-align: left" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                                    Width="150px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server"
                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chk"))=="True" %>'
                                                    Width="20px"
                                                    Enabled='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chk"))=="False" %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>
                            </div>
                        </asp:View>

                    </asp:MultiView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


