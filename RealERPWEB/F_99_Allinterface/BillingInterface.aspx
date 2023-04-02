
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="BillingInterface.aspx.cs" Inherits="RealERPWEB.F_99_Allinterface.BillingInterface" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
                /*border-bottom: 0;*/
            }

                ul.sidebarMenu li:last-child {
                    /*border-bottom: 1px solid #DFF0D8;*/
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
            font-family: Calibri, Arial;
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





        .modalPopup {
            top: 25px !important;
            min-height: 400px;
            overflow: scroll;
        }

        ul.sidebarMenu li {
            display: block;
            height: 36px;
            list-style: none;
            border: 1px solid #9752A2;
            /*border-bottom: 0;*/
        }

            ul.sidebarMenu li a {
                text-align: left;
                display: block;
                line-height: 30px;
                font-size: 14px;
                font-family: Calibri;
                cursor: pointer;
                /*/* width: 130px; */
                /*background: #32CD32;*/
                background: #CCE5FF;
                border-radius: 5px;
                color: #000;
                font-weight: bold;
                padding: 0 0 0 4px;
                /* line-height: 30px; */
                margin: 1px 2px;
                display: block;
                text-align: left;
                border-bottom: 1px;
            }

                ul.sidebarMenu li a:hover {
                    background: #006633;
                    color: white !important;
                }
    </style>



    <script src="../Scripts/waypoints.min.js"></script>
    <script src="../Scripts/jquery.counterup.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            try {

                var comcod =<%=this.GetCompCode()%>;


                switch (comcod) {
                   
                    case 3369:
                        $('#<%=this.hplMonRec.ClientID%>').attr("href", "../F_23_CR/CustOthMoneyReceipt?Type=CustCare");
                        break;

                    default:
                        $('#<%=this.hplMonRec.ClientID%>').attr("href", "../F_23_CR/CustOthMoneyReceipt?Type=Billing");
                        break;


                }

                $('.chzn-select').chosen({ search_contains: true });

                $("input, select").bind("keydown", function (event) {
                    var k1 = new KeyPress();
                    k1.textBoxHandler(event);
                });

                $('#<%=this.gvDayWSale.ClientID %>').tblScrollable();
                


            } catch (e) {
                alert(e);
            }



        };

    </script>








    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>

   
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

            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-1">
                            <div class="form-group">
                                <div class="form-group">
                                    <label class="control-label   lblmargin-top9px" for="lblfrmdate">Date</label>

                                </div>


                            </div>

                        </div>

                        <div class="col-md-2">
                            <div class="form-group">

                                <asp:TextBox ID="txtdate" runat="server" CssClass=" form-control   fa fa-pencil-ruler" AutoPostBack="true" OnTextChanged="txtdate_TextChanged"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>



                            </div>

                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnok" runat="server" CssClass="btn btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-2 pading5px ">


                            <ul class="sidebarMenu">


                                <li style="background-color: #FFA07A!important; text-align: center !important; padding: 10px !important">
                                    <asp:Label ID="Label2" runat="server" Font-Size="15px" Font-Bold="true" Style="text-align: center; margin-top: 3px !important;" Font-Names="Cambria" ForeColor="Black">Entry</asp:Label>

                                </li>

                               

                                <li>
                                    <asp:HyperLink ID="hplMonRec" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_23_CR/CustOthMoneyReceipt?Type=Billing">Money Receipt Create</asp:HyperLink>
                                </li>
                                 
                               

                                <li style="background-color: #FFA07A!important; text-align: center !important; padding: 10px !important">
                                    <asp:Label ID="lbl" runat="server" Font-Size="15px" Font-Bold="true" Style="text-align: center;" Font-Names="Cambria" ForeColor="Black">Report</asp:Label>

                                </li>
                                   <li>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_23_CR/RptMRTopSheet">Money Receipt 360</asp:HyperLink>

                                </li>
                                  <li>
                                    <asp:HyperLink ID="HyperLink6" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_22_Sal/RptMktMoneyReceipt?Type=Billing">Money Receipt Print</asp:HyperLink>

                                </li>
                                 <li>
                                    <asp:HyperLink ID="HyperLink11" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_17_Acc/RptAccCollVsClearance?Type=MonSales&comcod=">Month Wise Sales</asp:HyperLink>
                                </li>
                                <li>
                                    <asp:HyperLink ID="HyperLink12" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_17_Acc/RptAccCollVsClearance?Type=MonAR&comcod=">Month Wise Collection</asp:HyperLink>
                                </li>
                              

                            </ul>

                        </div>
                        <div id="slSt" class=" col-md-9  pading5px" style="margin-left: 30px;">
                            <div class="panel with-nav-tabs panel-primary">
                                <fieldset class="tabMenu">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="tbMenuWrp nav nav-tabs rptPurInt">
                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0"></asp:ListItem>
                                                    <asp:ListItem Value="1"></asp:ListItem>
                                                    <asp:ListItem Value="2"></asp:ListItem>
                                                 
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>

                                <div>

                                    <asp:Panel ID="pnlgvDayWSale" runat="server" Visible="false">

                                        <div class="table-responsive col-lg-12">

                                            <asp:GridView ID="gvDayWSale" runat="server" AutoGenerateColumns="False"
                                                ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" AllowPaging="False"
                                                OnRowDataBound="gvDayWSale_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Particular Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDPactdesc" runat="server" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    
                                                    
                                                    <asp:TemplateField HeaderText="Bill/Invoice No">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lgbillno" runat="server" 
                                                                Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "billno"))
                                                                         %>'
                                                                Width="90px"></asp:Label>


                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bill/Invoice Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvbilldat" runat="server"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "billdat")).ToString("dd-MMM-yyyy") %>'
                                                                Width="65px" Style="text-align: left"></asp:Label>
                                                        </ItemTemplate>


                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bill/Invoice Ref.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvbillrefno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billrefno")) %>'
                                                                Width="80px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bill/Invoice </Br>Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvbillamt" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="80px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                                <asp:Label ID="lgvFbillamt" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="User Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvUserNamemr" runat="server" Style="text-align: left" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Entry </br> User">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvpostdate" runat="server" Style="text-align: left" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postdate")) %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                </Columns>
                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                                <RowStyle CssClass="grvRows" />
                                            </asp:GridView>

                                        </div>
                                    </asp:Panel>


                                    <asp:Panel ID="pnlinprocess" Visible="false" runat="server">


                                        <div class="col-md-12" style="display: none;">

                                            <asp:RadioButtonList ID="rbtnList1" runat="server" BackColor="#BBBB99" CssClass="rbtnList1"
                                                RepeatColumns="6"
                                                RepeatDirection="Horizontal" Style="text-align: left" Width="369px">
                                                <asp:ListItem>With Post Dated</asp:ListItem>
                                                <asp:ListItem>Current Dated</asp:ListItem>
                                                <asp:ListItem>Actual Dated</asp:ListItem>
                                            </asp:RadioButtonList>


                                        </div>

                                        <div class="table-responsive col-lg-12">
                                            <asp:GridView ID="grvTrnDatWise" runat="server" AllowPaging="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                AutoGenerateColumns="False"
                                                ShowFooter="True"
                                                >
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNomr" runat="server" Font-Bold="True"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    
                                                    <asp:TemplateField HeaderText="Project Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgcProDescmr" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="MR No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgcMRNomr" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                   <%-- <asp:TemplateField HeaderText="Voucher  No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgcVoucherNomr" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>--%>


                                                    <asp:TemplateField HeaderText="MR Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgcMRDatmr" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrdate1")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Collection From" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvCollFrmmr" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "collfrm")) %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                      <asp:TemplateField HeaderText="Received Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgcrecptypemr" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rectype")) %>'
                                                                Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                          <FooterTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lgvFCDTotal" runat="server" Font-Bold="True" Font-Size="10px"
                                                                            ForeColor="Black" Style="text-align: right" Width="80px">Total:</asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lgvFCDNetTotal" runat="server" Font-Bold="True" Font-Size="10px"
                                                                            ForeColor="Black" Style="text-align: right" Width="80px">Net Total:</asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                                                                     


                                                    <asp:TemplateField HeaderText="Cheque No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvChNomr" runat="server" Style="text-align: left" Font-Size="10px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cash Amt">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvCaAmtmr" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cashamt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lgvFCashamt" runat="server" Font-Bold="True"
                                                                            ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label1" runat="server" Font-Bold="True"
                                                                            ForeColor="Black" Style="text-align: right" Width="70px"> </asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cheque Amt">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvChAmtmr" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chqamt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>


                                                        <FooterTemplate>

                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lgvFChqamt" runat="server" Font-Bold="True"
                                                                            ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lgvCDNetTotal" runat="server" Font-Bold="True"
                                                                            ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </FooterTemplate>


                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cheque Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvChDatmr" runat="server" Style="text-align: left" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqdate")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bank Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvBaNomr" runat="server" Style="text-align: left" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Reconciliation Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvRecDat1mr" runat="server" Style="text-align: left" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recndt")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="User Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvUserNamemr" runat="server" Style="text-align: left" Font-Size="8px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    

                                                </Columns>
                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                                <RowStyle CssClass="grvRows" />
                                            </asp:GridView>
                                        </div>


                                    </asp:Panel>
                                    
                                    <asp:Panel ID="PnlDues" runat="server" Visible="false">

                                        <div class="table-responsive col-lg-12">

                                            <asp:GridView ID="grvNetDues" runat="server" AutoGenerateColumns="False"
                                                ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" AllowPaging="False"
                                              >
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Particular Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDPactdesc" runat="server" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    
                                                    
                                                    <asp:TemplateField HeaderText="Bill/Invoice No">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lgbillno" runat="server" 
                                                                Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "billno"))
                                                                         %>'
                                                                Width="90px"></asp:Label>


                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bill/Invoice Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvbilldat" runat="server"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "billdat")).ToString("dd-MMM-yyyy") %>'
                                                                Width="65px" Style="text-align: left"></asp:Label>
                                                        </ItemTemplate>


                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bill/Invoice Ref.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvbillrefno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billrefno")) %>'
                                                                Width="80px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bill/Invoice </Br>Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvbillamt" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="80px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                                <asp:Label ID="lgvFbillamt" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Collection </Br>Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvpaidamt" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="80px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                                <asp:Label ID="lgvFpaidamt" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Dues </Br>Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvdueamt" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="80px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>
                                                         <FooterTemplate>
                                                                <asp:Label ID="lgvFdueamt" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="User Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvUserNamemr" runat="server" Style="text-align: left" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Entry </br> User"> 
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvpostdate" runat="server" Style="text-align: left" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "posteddat")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                </Columns>
                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                                <RowStyle CssClass="grvRows" />
                                            </asp:GridView>

                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>



    </asp:UpdatePanel>


</asp:Content>


