
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptMarketingInterface.aspx.cs" Inherits="RealERPWEB.F_99_Allinterface.RptMarketingInterface" %>

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
            /*height: 50px;*/
            height: 65px;
            width: 120px;
            padding: 0 0;
            float: left;
            list-style: none;
            margin: 0 1px;
            color: #fff;
            text-align: center;
            border: 2px solid #D1D735;
            /*-webkit-border-radius: 30px;
            -moz-border-radius: 30px;
            border-radius: 30px;*/
            cursor: pointer;
            background: #fff;
            position: relative;
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
                background: #769BF4;
            }

            .tbMenuWrp table tr td:nth-child(7) {
                background: #00CBF3;
            }

            .tbMenuWrp table tr td:nth-child(8) {
                background: #4BCF9E;
            }
                .tbMenuWrp table tr td:nth-child(9) {
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




      
           .modalPopup{
            top:25px !important;
            min-height:400px;
            overflow:scroll;
       
        }
    </style>

      <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {
           <%-- var gv = $('#<%=this.dgvAccRec.ClientID %>');
            gv.Scrollable();--%>
            // $('.chzn-select').chosen({ search_contains: true });
        
            var gvtodayapp = $('#<%=this.gvtodayapp.ClientID %>');

        


            gvtodayapp.gridviewScroll({
                width: 1160,
                height: 420,
                arrowsize: 30,
                railsize: 16,
                barsize: 8,
                varrowtopimg: "../Image/arrowvt.png",
                varrowbottomimg: "../Image/arrowvb.png",
                harrowleftimg: "../Image/arrowhl.png",
                harrowrightimg: "../Image/arrowhr.png",
                freezesize: 6
            });


        }
      </script>
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
                                <div class="col-md-10  pading5px">
                                     <div class="col-md-4 pading5px">
                                    <asp:Label ID="lblfrmdate" runat="server" CssClass=" smLbl_to">Date</asp:Label>                                    
                                  <asp:TextBox ID="txtfrmdate" runat="server" CssClass=" inputDateBox" AutoPostBack="true"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender_txtfrmdate" runat="server" Enabled="True"
                                        Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                                    <asp:LinkButton ID="lnkbtnok" runat="server" CssClass="btn btn-primary okBtn"  OnClick="lnkbtnok_Click">Ok</asp:LinkButton>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </fieldset>
                <asp:Panel ID="pnlInterf" runat="server">
                            <div id="slSt" class=" col-md-12 ServProdInfo">
                                <div class="panel with-nav-tabs panel-primary">
                                    <fieldset class="tabMenu">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="tbMenuWrp nav nav-tabs rptPurInt">
                                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0"></asp:ListItem>
                                                        <asp:ListItem Value="1"></asp:ListItem>
                                                        <asp:ListItem Value="2"></asp:ListItem>
                                                        <asp:ListItem Selected="True" Value="3"></asp:ListItem>
                                                        <asp:ListItem Value="4"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                   <asp:MultiView ID="MkIntView" runat="server">
                                    <asp:View ID="ViewClntBrthDay" runat="server">
                                       <div class="table table-responsive">
                                          <asp:GridView ID="gvClientBrthDay" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                Width="186px" ShowFooter="True">

                                                <Columns>

                                                    <asp:TemplateField HeaderText="Sl No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvslno" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                                Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Client Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvClientName" runat="server" Font-Bold="True"
                                                                Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name")) %>' Width="160px"
                                                                ForeColor="Black"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Nick Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvClientnNameb" runat="server"
                                                                Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nickname")) %>' Width="120px"
                                                                ForeColor="Black"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Address">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvpredAddress" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preaddress")) %>'
                                                                Width="160px" ForeColor="Black"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Home Phone">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvhomephoneb" runat="server" Width="80px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "homephone")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" />

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Mobile">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvMobNo" runat="server" Width="100px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobile")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Office Phone">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvoffPhoneb" runat="server" Width="80px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "offphone")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="btnSendMail" runat="server" Font-Bold="True" CssClass="btn btn-warning"
                                                                Style="text-decaration: none;" Font-Size="12px" ForeColor="Black">Send Mail</asp:LinkButton>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Email">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvemailb" runat="server" Width="140px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="btnSendSms" runat="server" Font-Bold="True" CssClass="btn btn-primary "
                                                                Style="text-decaration: none;" Font-Size="12px" ForeColor="Black">Send SMS</asp:LinkButton>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>
                                       </div>
                                    </asp:View>

                                    <asp:View ID="ViewClientMrrgDay" runat="server">
                                       <div class="table table-responsive">
                                          <asp:GridView ID="gvClientMrgDay" runat="server" AutoGenerateColumns="False"
                                            CssClass=" table-striped table-hover table-bordered grvContentarea"
                                            Width="186px" ShowFooter="True">
                                            <Columns>

                                                <asp:TemplateField HeaderText="Sl No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvslno" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                            Width="40px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Client Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvClientName" runat="server" Font-Bold="True"
                                                            Style="text-align: left"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name")) %>' Width="160px"
                                                            ForeColor="Black"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Nick Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvClientnNamem" runat="server"
                                                            Style="text-align: left"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nickname")) %>' Width="120px"
                                                            ForeColor="Black"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Address">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvpredAddress" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preaddress")) %>'
                                                            Width="160px" ForeColor="Black"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Home Phone">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvconNo" runat="server" Width="80px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "homephone")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvMobNo" runat="server" Width="100px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobile")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="btnSendMail" runat="server" Font-Bold="True" CssClass="btn btn-success"
                                                            Style="text-decaration: none;" Font-Size="12px" ForeColor="Black">Send Mail</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Office Phone">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvoffPhonem" runat="server" Width="80px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "offphone")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="btnSendSms" runat="server" Font-Bold="True" CssClass="btn btn-success "
                                                            Style="text-decaration: none;" Font-Size="12px" ForeColor="Black">Send SMS</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvemailm" runat="server" Width="140px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
                                       </div>
                                    </asp:View>

                                    <asp:View ID="todaysApp" runat="server">
                                      <div class="table-responsive">
                                     <asp:GridView ID="gvtodayapp" runat="server" AllowPaging="True" 
                                        AutoGenerateColumns="False" PageSize="15" ShowFooter="True"  
                                        CssClass="table-striped table-hover table-bordered grvContentarea">
                                       
                                        <Columns>
                                            <asp:CommandField ShowEditButton="True" />
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNotd" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Appointment">
                                                <ItemTemplate>
                                                    <asp:Label ID="nappdattd" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "napnt")).ToString("dd-MMM-yyyy HH:mm:tt") %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtgvdVal" runat="server" BackColor="Transparent" BorderStyle="None" BorderWidth="0"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "napnt")) %>'
                                                                Width="120px"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="txtgvdVal_CalendarExtender" runat="server"
                                                                Enabled="True" Format="dd'/'MMM'/'yyyy HH':'mm':'ss" TargetControlID="txtgvdVal">
                                                            </cc1:CalendarExtender>



                                                </EditItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Booking Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvMeetingdattd" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                               

                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Client Name">
                                                <ItemTemplate>

                                                   <asp:HyperLink ID="hlnkgvwrkdesc" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc"))%>' Width="200px">
                                    </asp:HyperLink>
                                                     <asp:Label ID="lgClNameCode" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "proscod")) %>'
                                                        Width="120px"></asp:Label>
                                                   
                                                </ItemTemplate>                                              

                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Phone No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvPhonetd" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <EditItemTemplate>
                                                   <asp:Label ID="lgvPhonetd2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                        Width="70px"></asp:Label>
                                                </EditItemTemplate>

                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Project Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvpronametd" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                  <EditItemTemplate>
                                                     <asp:Label ID="lgvpronametd4" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </EditItemTemplate>

                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvunitnametd" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                        Width="110px"></asp:Label>
                                                </ItemTemplate>

                                                  <EditItemTemplate>
                                                    <asp:Label ID="lgvunitnametd1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                        Width="110px"></asp:Label>
                                                </EditItemTemplate>

                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Size">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvunitsizetd" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>

                                                 <EditItemTemplate>
                                                     <asp:Label ID="lgvunitsizetd1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </EditItemTemplate>

                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Actual Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvacpricetd" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrate")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>

                                                   <EditItemTemplate>
                                                      <asp:Label ID="lgvacpricetd1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrate")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </EditItemTemplate>

                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Parking">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvacparkingtd" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>

                                                 <EditItemTemplate>
                                                   <asp:Label ID="lgvacparkingtd2" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </EditItemTemplate>

                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Other">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvacothertd" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>

                                                  <EditItemTemplate>
                                                   <asp:Label ID="lgvacothertd1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </EditItemTemplate>

                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Offered Price">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvactoamttd" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tuamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>

                                                  <EditItemTemplate>
                                                   <asp:Label ID="lgvactoamttd1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tuamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </EditItemTemplate>

                                                <ItemStyle HorizontalAlign="right" BackColor="#69AEE7" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Offered Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvofratetd" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>

                                                  <EditItemTemplate>
                                                   <asp:Label ID="lgvofratetd2" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </EditItemTemplate>

                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Parking">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvofparkingtd" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ofpamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>

                                                 <EditItemTemplate>
                                                    <asp:Label ID="lgvofparkingtd3" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ofpamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </EditItemTemplate>

                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Other">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgofothertd" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ofothamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>

                                                  <EditItemTemplate>
                                                     <asp:Label ID="lgofothertd2" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ofothamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="55px"></asp:Label>
                                                </EditItemTemplate>

                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Asking Price">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvoftoamttd" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "oftuamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                
                                                  <EditItemTemplate>
                                                     <asp:Label ID="lgvoftoamttd1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "oftuamt")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </EditItemTemplate>

                                                <ItemStyle HorizontalAlign="right" BackColor="#69AEE7" />

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Gap %">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvoftogggamttd" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gap")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                     <EditItemTemplate>
                                                     <asp:Label ID="lgvoftogggamttd1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gap")).ToString("#,##0; (#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </EditItemTemplate>

                                                <ItemStyle HorizontalAlign="right" BackColor="#69AEE7" />

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Discussion">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDiscussiontd" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "discus")) %>'
                                                        Width="320px"></asp:Label>
                                                </ItemTemplate>

                                                  <EditItemTemplate>
                                                    <asp:Label ID="lgvDiscussiontd1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "discus")) %>'
                                                        Width="320px"></asp:Label>
                                                </EditItemTemplate>


                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:CommandField />
                                        </Columns>
                                        <FooterStyle BackColor="#F5F5F5" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                     </div>
                                   </asp:View>

                                 </asp:MultiView>
                                </div>
                            </div>
                 </asp:Panel>
            </div>
        </div>
    </div>



</asp:Content>


