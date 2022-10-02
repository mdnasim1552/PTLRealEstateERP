<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="BudgetInterface.aspx.cs" Inherits="RealERPWEB.F_99_Allinterface.BudgetInterface" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #AsyncFileUpload1 input {
            position: absolute;
            top: 0;
            right: 0;
            margin: 0;
            border: solid transparent;
            border-width: 0 0 100px 200px;
            opacity: 0.0;
            filter: alpha(opacity=0);
            -o-transform: translate(250px, -50px) scale(1);
            -moz-transform: translate(-300px, 0) scale(4);
            direction: ltr;
            cursor: pointer;
        }
    </style>


    <script type="text/javascript">






        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function openModal() {
            //    $('#myModal').modal('show');
            $('#contact').modal('toggle');
        }

        function CloseModal() {

            $('#contact').modal('hide');
        }





        function pageLoaded() {

            hideOptions();


            try {
                $("input, select").bind("keydown", function (event) {
                    var k1 = new KeyPress();
                    k1.textBoxHandler(event);

                });

                $('.chzn-select').chosen({ search_contains: true });

                $('#<%=this.gvPrjInfo.ClientID%>').tblScrollable();

           <%-- var gvclient = $('#<%=this.gvPrjInfo.ClientID %>');

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
            });--%>

            }

            catch (e) {


                alert(e.message);
            }

        }



        function Search_Gridview(strKey, cellNr, gvname) {
            try {



                // var txtvalu = document.getElementById('<%=((TextBox)gvPrjInfo.HeaderRow.FindControl("txtSearchpro")).ClientID %>').value;

                var tblData

                switch (gvname) {

                    case 'gvPrjInfo':
                        tblData = document.getElementById("<%=this.gvPrjInfo.ClientID %>");
                        break;


                    case 'gvCodeBook':
                        tblData = document.getElementById("<%=this.gvCodeBook.ClientID %>");
                        break;


                }

                var strData = strKey.value.toLowerCase().split(" ");


                var rowData;
                for (var i = 0; i < tblData.rows.length; i++) {
                    rowData = tblData.rows[i].cells[cellNr].innerHTML;
                    var styleDisplay = 'none';
                    for (var j = 0; j < strData.length; j++) {
                        if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                            styleDisplay = '';
                        else {
                            styleDisplay = 'none';
                            break;
                        }
                    }
                    tblData.rows[i].style.display = styleDisplay;
                }


                // document.getElementById('<%=((TextBox)gvPrjInfo.HeaderRow.FindControl("txtSearchpro")).ClientID %>').value = "";
            }

            catch (e) {
                alert(e.message);

            }

        }

        function hideOptions() {
            $(".tbMenuWrp table tr td:nth-child(1)").hide();
            $(".tbMenuWrp table tr td:nth-child(3)").hide();
            //$(".tbMenuWrp table tr td:nth-child(8)").hide();
            //$(".tbMenuWrp table tr td:nth-child(9)").hide();
        }


    </script>
    <style type="text/css">
        .modal-dialog {
            margin: 44px auto;
            width: 50%;
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

            /*.tbMenuWrp table tr td:nth-child(8) {
                background: #4BCF9E;
            }

            .tbMenuWrp table tr td:nth-child(9) {
                background: #4BCF9E;
            }*/

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
            padding: 1px;
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
                border-radius: 5;
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

        .pad2px {
            padding-top: 2px;
            padding-bottom: 2px;
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



            <div class="card card-fluid">
                <div class="card-body">

                    <div class="row">

                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label lblmargin-top9px" for="FromDate">Date</label>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_txtdate" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>

                            </div>

                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label lblmargin-top9px">Total Proj.</label>



                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:HyperLink ID="ttlcustomer" Target="_blank" NavigateUrl="~/F_32_Mis/RptMisMasterBgd?Type=InvPlan" runat="server" CssClass="btn btn-sm btn-warning pad2px">1000</asp:HyperLink>

                                <%--<asp:Label ID="lblbill" runat="server" CssClass=" form-control "></asp:Label>--%>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label lblmargin-top9px">Project Type</label>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlProjectType" CssClass="inputTxt ddlPage chzn-select" Width="150px" TabIndex="13" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectType_SelectedIndexChanged" >
                                    <asp:ListItem Value="00000">Please Select</asp:ListItem>

                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="btn-group" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-danger">Operations</button>
                                <div class="btn-group" role="group">
                                    <button id="btnGroupDrop4" type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                        <div class="dropdown-arrow"></div>

                                        <asp:HyperLink ID="HyperLink3" Target="_blank" NavigateUrl="~/F_17_Acc/AccSubCodeBook?InputType=Res" runat="server" CssClass="dropdown-item">Resource Code</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink1" Target="_blank" NavigateUrl="~/F_17_Acc/AccSubCodeBook?InputType=Wrkschedule" runat="server" CssClass="dropdown-item">Work Schedule</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink4" Target="_blank" NavigateUrl="~/F_04_Bgd/BgdStdAna" runat="server" CssClass="dropdown-item">  Standard Analysis</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink2" Target="_blank" NavigateUrl="~/F_34_Mgt/AccProjectCode" runat="server" CssClass="dropdown-item"> Add Project</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink5" Target="_blank" NavigateUrl="~/F_04_Bgd/AddBudget?Type=Entry&prjcode=" runat="server" CssClass="dropdown-item"> Additional Budget</asp:HyperLink>



                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="btn-group" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-danger">Reports</button>
                                <div class="btn-group" role="group">
                                    <button id="btnGroupDrop4" type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                        <div class="dropdown-arrow"></div>

                                        <asp:HyperLink ID="HyperLink6" Target="_blank" NavigateUrl="~" runat="server" CssClass="dropdown-item">Budget Status</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink7" Target="_blank" NavigateUrl="~/F_32_Mis/ProjectAnalysis?Type=Report&comcod=" runat="server" CssClass="dropdown-item">Multi Project</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink8" Target="_blank" NavigateUrl="~/F_32_Mis/RptMisMasterBgd.aspx?Type=InvPlan&comcod=" runat="server" CssClass="dropdown-item">  Investment Plan Summary</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink9" Target="_blank" NavigateUrl="~/F_32_Mis/RptConstruProgress" runat="server" CssClass="dropdown-item">Category Wise Construction Progress</asp:HyperLink>



                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div id="slSt" class=" col-md-12  pading5px">
                            <div class="panel with-nav-tabs panel-deafult">
                                <fieldset class="tabMenu">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="tbMenuWrp nav nav-tabs rptPurInt">
                                                <asp:RadioButtonList ID="RadioButtonList1" ClientIDMode="Static" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0"></asp:ListItem>
                                                    <asp:ListItem Value="1" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="2"></asp:ListItem>
                                                    <asp:ListItem Value="3"></asp:ListItem>
                                                    <asp:ListItem Value="4"></asp:ListItem>
                                                    <asp:ListItem Value="5"></asp:ListItem>
                                                    <asp:ListItem Value="6"></asp:ListItem>
                                                  <%--  <asp:ListItem Value="7" style="display:none;"></asp:ListItem>
                                                    <asp:ListItem Value="8" style="display:none;"></asp:ListItem>--%>

                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                    </div>

                    <asp:Panel ID="pnlBgd" runat="server" Visible="false">

                        <div class="col-md-12 table-responsive">
                            <asp:GridView ID="gvPrjInfo" runat="server"
                                AutoGenerateColumns="False" PageSize="15" ShowFooter="true" OnRowDataBound="gvPrjInfo_RowDataBound"
                                CssClass="table-striped table-hover table-bordered grvContentarea">
                                <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                    Mode="NumericFirstLast" />

                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsn" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Project Name">

                                        <HeaderTemplate>
                                            <asp:TextBox ID="txtSearchpro" SortExpression="actdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Project Name" onkeyup="Search_Gridview(this,1, 'gvPrjInfo')"></asp:TextBox><br />

                                        </HeaderTemplate>

                                       <%-- Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))--%>

                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyplProjectS" runat="server" Target="_blank" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "pdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "pdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                           "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "") %>'
                                                Width="150px"></asp:HyperLink>
                                            <asp:Label ID="lgbActcode" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="Construction <br> Area">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvConstruction" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "consarea")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Saleable <br> area">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpronamechS" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salearea")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Project <br> start date">
                                        <ItemTemplate>
                                            <asp:Label ID="lgbStartDate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "prjsdate")).ToString("dd-MMM-yyyy") %>'
                                                Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "prjsdate")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'
                                                Width="85px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Project <br> completion date">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcomDate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "prjenddate")).ToString("dd-MMM-yyyy") %>'
                                                Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "prjenddate")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'
                                                Width="85px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Area">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvbarea" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "location")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Construction Cost">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvBgdConstruction" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Total Cost">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvBgdamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdtamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cost Per SFT">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvBgdCosper" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conspersft")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Actual Cost">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvacamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cost Per SFT(Actual)">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvaccostperscf" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "accpersft")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>



                                  <%--  <asp:TemplateField HeaderText="Plan Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpBgdamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "plnamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Excution Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpexBgdamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "examt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="less Excution Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvplesexBgdamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lesexamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Target Work in %" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpsdlesexBgdamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "targwrkper")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actual Work in %" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpsdlesfgdamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "actwrkper")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Less Progress in %" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvlessrogress" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lessprog")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink4" Target="_blank" runat="server" CssClass="btn btn-sm btn-success pad2px"> <span class="glyphicon glyphicon-eye-open"></span> View</asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>

                                            <asp:HyperLink ID="lnkapp" Visible="false" Target="_blank" runat="server" ToolTip="Approved" CssClass="btn btn-default btn-xs"><span style="color:green" class=" fa fa-check"></span> </asp:HyperLink>
                                            <asp:HyperLink ID="lnkBgdLock" Visible="false" Target="_blank" runat="server" ToolTip="Budget Lock" CssClass="btn btn-default btn-xs"><span style="color:green" class=" fa fa-lock"></span> </asp:HyperLink>

                                        </ItemTemplate>

                                        <ItemStyle Width="40px" />
                                        <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#F5F5F5" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <RowStyle CssClass="grvRows" />
                            </asp:GridView>

                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlPrjLink" runat="server" Visible="false">
                        <asp:GridView ID="gvCodeBook" runat="server"
                            AutoGenerateColumns="False" OnPageIndexChanging="gvCodeBook_PageIndexChanging"
                            OnRowCancelingEdit="gvCodeBook_RowCancelingEdit" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            OnRowEditing="gvCodeBook_RowEditing" OnRowUpdating="gvCodeBook_RowUpdating"
                            Width="500px" AllowPaging="True" PageSize="30">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CompCode" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvComCod" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")) %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GroupCode" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvInfGrp" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infgrp")) %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inf Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvInfCod" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>'
                                            Width="49px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="True">
                                    <ItemStyle Font-Bold="True" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvInfCod1" runat="server" BorderColor="Blue"
                                            BorderStyle="Solid" BorderWidth="1px" Font-Size="11px" MaxLength="16"
                                            Style="font-weight: 700; text-align: left;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod1")) %>'
                                            Width="100px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvInfCod1" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod1")) %>'
                                            Width="100px" Style="text-align: left; font-weight: 700"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Details Description">

                                    <%--  <HeaderTemplate>
                                            <asp:TextBox ID="txtSearchinfdesc" SortExpression="infdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="180px" placeholder="Details Description" onkeyup="Search_Gridview(this,3, 'gvCodeBook')"></asp:TextBox>

                                        </HeaderTemplate>--%>

                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvInfDesc" runat="server" BorderColor="Blue"
                                            BorderStyle="Solid" BorderWidth="1px" Font-Size="11px"
                                            Style="font-weight: 700; text-align: left;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>'
                                            Width="250px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvInfDesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Short Description">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvInfDes2" runat="server" BorderColor="Blue"
                                            BorderStyle="Solid" BorderWidth="1px" Font-Size="11px"
                                            Style="font-weight: 700; text-align: left;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc2")) %>'
                                            Width="177px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvInfDes2" runat="server" Height="20px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc2")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvUnitFPS" runat="server" BorderColor="Blue"
                                            BorderStyle="Solid" BorderWidth="1px" Font-Size="11px" MaxLength="12"
                                            Style="font-weight: 700; text-align: left;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unitfps")) %>'
                                            Width="55px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvUnitFPS" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unitfps")) %>'
                                            Width="58px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Rate">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvStdQtyF" runat="server" BorderColor="Blue"
                                            BorderStyle="Solid" BorderWidth="1px" Font-Size="11px" MaxLength="12"
                                            Style="font-weight: 700; text-align: right;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stdqtyf")).ToString("###0.0000;(###0.00); ") %>'
                                            Width="100px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvStdQtyF" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stdqtyf")).ToString("#,##0.0000;(#,##0.00); ") %>'
                                            Width="100px" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Const. Area" Visible="false">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvcarea" runat="server" BorderColor="Blue"
                                            BorderStyle="Solid" BorderWidth="1px" Font-Size="11px" MaxLength="12"
                                            Style="font-weight: 700; text-align: right;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conarea")).ToString("###0.00;(###0.00); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcarea" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conarea")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Saleable Area" Visible="false">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvsarea" runat="server" BorderColor="Blue"
                                            BorderStyle="Solid" BorderWidth="1px" Font-Size="11px" MaxLength="12"
                                            Style="font-weight: 700; text-align: right;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salarea")).ToString("###0.00;(###0.00); ") %>'
                                            Width="80px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvgvsarea" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salarea")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Project Name">
                                    <EditItemTemplate>
                                        <asp:Panel ID="Panel2" runat="server">
                                            <table style="width: 250px;">
                                                <tr>

                                                    <td>
                                                        <asp:LinkButton ID="ibtnSrchProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnSrchProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlProName" runat="server" CssClass="ddlistPull" TabIndex="6">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvProNames" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                            <RowStyle CssClass="grvRows" />
                        </asp:GridView>
                    </asp:Panel>

                    <asp:Panel ID="pnlDissAppo" runat="server" Visible="false">
                    </asp:Panel>


                    <asp:Panel ID="pnlDissNew" runat="server" Visible="false">
                    </asp:Panel>
                    <asp:Panel ID="pnlDataBank" runat="server" Visible="false">
                    </asp:Panel>
                    <asp:Panel ID="pnlNextApponi" runat="server" Visible="false">
                    </asp:Panel>

                    <asp:Panel ID="pnlbirthday" runat="server" Visible="false">
                    </asp:Panel>


                    <asp:Panel ID="PnlMarrDay" runat="server" Visible="false">
                    </asp:Panel>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

