<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AllEmpList.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_92_Mgt.AllEmpList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $('.chzn-select').chosen({ search_contains: true });

        };
    </script>

    <style>
        #charts {
            width: 600px;
            height: 400px;
            position: relative;
            margin: 0 auto;
            font-size: 12px;
        }

        .chartdiv {
            width: 600px;
            height: 400px;
            position: absolute;
            top: 10px;
            left: 0;
        }

        .legnd {
            position: absolute;
            top: 0;
            left: 0;
            z-index: 500;
            background: #ffffff;
        }

            .legnd span:nth-child(1) {
                color: #0FAD5E;
            }

            .legnd span:nth-child(2) {
                color: #F15922;
            }

            .legnd span:nth-child(3) {
                color: #C3DA57;
            }

            .legnd span:nth-child(4) {
                color: #88C760;
            }

            .legnd span:nth-child(5) {
                color: #F59AB8;
            }

            .legnd span:nth-child(6) {
                color: #E56B8D;
            }

            .legnd span:nth-child(7) {
                color: #336699;
            }


        .lblrangeup {
            background: rgba(0, 0, 0, 0) url("../../Images/ArrowUp.png") no-repeat scroll center top;
            float: right;
            height: 60px;
            width: 27px;
        }

        .lblrangedwn {
            background: rgba(0, 0, 0, 0) url("../../Images/ArrowDown.png") no-repeat scroll center bottom;
            float: right;
            height: 60px;
            width: 27px;
        }
    </style>
    <style>
        .well h4 {
            margin: 0;
            font-size: 15px;
            font-weight: bold;
        }

        .well p {
            margin: 0;
        }
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
    <div class="container moduleItemWrpper">
        <div class="contentPart">
            <fieldset class="scheduler-border fieldset_A" id="cmplist" runat="server" visible="False">
                <div class="form-horizontal">
                    <div class="form-group">
                        <div class="col-md-4">
                            <h4 style="text-align: left; padding-left: 12px">List Of Companies</h4>
                            <asp:DropDownList ID="ddlComName" OnSelectedIndexChanged="ddlComName_OnSelectedIndexChanged" AutoPostBack="True" class="ComName form-control ClCompAndMod" runat="server" TabIndex="2">
                            </asp:DropDownList>
                            <%--<ul id="leftul" class="hover-item"  clientidmode="Static">
                    </ul>--%>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>

            </fieldset>
            <button type="button" class="btn btn-primary btn-lg" data-toggle="modal" style="background: #fff; float: right; margin-top: -32px; padding: 0 12px; position: absolute; right: 299px; z-index: 555555 !important;"
                data-target="#myModal">
                Graph
            </button>

            <!-- Modal -->
            <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title" id="myModalLabel">Staff Graph</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-lg-12">
                                    <asp:Panel ID="onDayGraph" runat="server">
                                        <script src="../../Scripts/Mchart.js"></script>
                                        <script src="../../Scripts/Mchartpie.js"></script>
                                        <%--<script src="http://www.amcharts.com/lib/3/themes/light.js"></script>--%>



                                        <div id="charts">
                                            <div class="btn btn-primary btn-sm legnd" style="background: #fff; width: 50%; border: none !important; height: 31px;">

                                                <span></span>
                                                <span></span>
                                                <span></span>
                                                <span></span>
                                                <span></span>
                                                <span></span>
                                                <span></span>

                                            </div>
                                            <div id="chart1" class="chartdiv"></div>
                                            <div id="chart2" class="chartdiv"></div>

                                        </div>

                                    </asp:Panel>
                                    <div>
                                    </div>


                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>

                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <fieldset class="scheduler-border fieldset_A">
                    <div class="form-horizontal">

                        <div class="form-group">
                            <div class="col-md-4">
                                <asp:Label ID="lblSection" runat="server" CssClass="lblTxt lblName">Section :</asp:Label>
                                <asp:DropDownList ID="ddlsection" runat="server" Width="250" CssClass="chzn-select form-control inputTxt pull-left" TabIndex="2">
                                </asp:DropDownList>


                            </div>
                            <div class="col-md-1">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                            <div class="col-md-7">
                                <asp:HyperLink ID="hlnktoEmployee" CssClass="pull-right" Style="font-weight: bold; font-size: 14px; margin-right: 30px;" runat="server" Target="_blank" NavigateUrl="~/F_81_Hrm/F_97_MIS/RptMgtInterface.aspx">Total Employee</asp:HyperLink>
                            </div>

                        </div>
                    </div>
                </fieldset>
            </div>
            <%--<div class="row">--%>




            <div class="row" id="datashow" runat="server">

            </div>
            <%-- </div>--%>
            <div style="display: none">

                <%-- <asp:TextBox ID="txtpresent" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtlate" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtearlylev" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtonleave" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtabsent" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txttostaff" runat="server"></asp:TextBox>--%>
                <asp:TextBox ID="txtTtlStaff" runat="server"></asp:TextBox>

                <asp:TextBox ID="txtdpt1" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtdpt2" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtdpt3" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtdpt4" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtdpt5" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtdpt6" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtdpt7" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtdpt8" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtdpt9" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtdpt10" runat="server"></asp:TextBox>

                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>


            </div>
        </div>
        <script>
            /**
    * Plugin: Manipulate z-index of the chart
    */
            AmCharts.addInitHandler(function (chart) {
                // init holder for nested charts
                if (AmCharts.nestedChartHolder === undefined)
                    AmCharts.nestedChartHolder = {};

                if (chart.bringToFront === true) {
                    chart.addListener("init", function (event) {
                        // chart inited
                        var chart = event.chart;
                        var div = chart.div;
                        var parent = div.parentNode;

                        // add to holder
                        if (AmCharts.nestedChartHolder[parent] === undefined)
                            AmCharts.nestedChartHolder[parent] = [];
                        AmCharts.nestedChartHolder[parent].push(chart);

                        // add mouse mouve event
                        chart.div.addEventListener('mousemove', function () {

                            // calculate current radius
                            var x = Math.abs(chart.mouseX - (chart.realWidth / 2));
                            var y = Math.abs(chart.mouseY - (chart.realHeight / 2));
                            var r = Math.sqrt(x * x + y * y);

                            // check which chart smallest chart still matches this radius
                            var smallChart;
                            var smallRadius;
                            for (var i = 0; i < AmCharts.nestedChartHolder[parent].length; i++) {
                                var checkChart = AmCharts.nestedChartHolder[parent][i];

                                if ((checkChart.radiusReal < r) || (smallRadius < checkChart.radiusReal)) {
                                    checkChart.div.style.zIndex = 1;
                                }
                                else {
                                    if (smallChart !== undefined)
                                        smallChart.div.style.zIndex = 1;
                                    checkChart.div.style.zIndex = 2;
                                    smallChart = checkChart;
                                    smallRadius = checkChart.radiusReal;
                                }

                            }
                        }, false);
                    });
                }

            }, ["pie"]);


    <%--    var pres = this.parseFloat($("#<%=this.txtpresent.ClientID %>").val());
        var el = this.parseFloat($("#<%=this.txtearlylev.ClientID %>").val());
        var ol = this.parseFloat($("#<%=this.txtonleave.ClientID %>").val());
        var late = this.parseFloat($("#<%=this.txtlate.ClientID %>").val());
        var abs = this.parseFloat($("#<%=this.txtabsent.ClientID %>").val());--%>


            var dpt1 = this.parseFloat($("#<%=this.txtdpt1.ClientID %>").val());
            var dpt2 = this.parseFloat($("#<%=this.txtdpt2.ClientID %>").val());
            var dpt3 = this.parseFloat($("#<%=this.txtdpt3.ClientID %>").val());
            var dpt4 = this.parseFloat($("#<%=this.txtdpt4.ClientID %>").val());
            var dpt5 = this.parseFloat($("#<%=this.txtdpt5.ClientID %>").val());
            var dpt6 = this.parseFloat($("#<%=this.txtdpt6.ClientID %>").val());
            var dpt7 = this.parseFloat($("#<%=this.txtdpt7.ClientID %>").val());
            var dpt8 = this.parseFloat($("#<%=this.txtdpt8.ClientID %>").val());
            var dpt9 = this.parseFloat($("#<%=this.txtdpt9.ClientID %>").val());
            var dpt10 = this.parseFloat($("#<%=this.txtdpt10.ClientID %>").val());


            //Titel
            var dptname1 = this.String($("#<%=this.TextBox1.ClientID %>").val());
            var dptname2 = this.String($("#<%=this.TextBox2.ClientID %>").val());
            var dptname3 = this.String($("#<%=this.TextBox3.ClientID %>").val());
            var dptname4 = this.String($("#<%=this.TextBox4.ClientID %>").val());
            var dptname5 = this.String($("#<%=this.TextBox5.ClientID %>").val());
            var dptname6 = this.String($("#<%=this.TextBox6.ClientID %>").val());
            var dptname7 = this.String($("#<%=this.TextBox7.ClientID %>").val());
            var dptname8 = this.String($("#<%=this.TextBox8.ClientID %>").val());
            var dptname9 = this.String($("#<%=this.TextBox9.ClientID %>").val());
            var dptname10 = this.String($("#<%=this.TextBox10.ClientID %>").val());


            /// var ttlstaff = dpt1 + dpt2 + dpt3 + dpt4 + dpt5 + dpt6 + dpt7 + dpt8 + dpt9 + dpt10;


            var ttlstaff = this.parseFloat($("#<%=this.txtTtlStaff.ClientID %>").val());
            //var abs = 4;

            /**
             * Create the charts
             */
            AmCharts.makeChart("chart1", {

                "type": "pie",
                "theme": "light",
                "bringToFront": true,
                "dataProvider": [{
                    "title": 'Staff' + '(' + ttlstaff + ')',
                    "value": ttlstaff,
                    "color": "#fff"
                }],
                "startDuration": 0,
                "pullOutRadius": 0,
                "color": "#000",
                "fontSize": 10,
                "titleField": "title",
                "valueField": "value",
                "colorField": "color",
                "labelRadius": -50,
                "labelColor": "#fff",
                "radius": 50,
                "innerRadius": 0,
                "labelText": "[[title]]",
                "balloonText": "[[title]]: [[value]]"
            });

            AmCharts.makeChart("chart2", {
                "type": "pie",
                "theme": "light",
                "bringToFront": true,
                "dataProvider": [{
                    "title": dptname1,
                    "value": dpt1,
                    "color": "#3366CC"
                }, {
                    "title": dptname2,
                    "value": dpt2,
                    "color": "#DC3912"
                },
                {
                    "title": dptname3,
                    "value": dpt3,
                    "color": "#FF9900"
                },
                {
                    "title": dptname4,
                    "value": dpt4,
                    "color": "#109618"
                },
                {
                    "title": dptname5,
                    "value": dpt5,
                    "color": "#990099"
                },
                {
                    "title": dptname6,
                    "value": dpt6,
                    "color": "#0099C6"
                },
                {
                    "title": dptname7,
                    "value": dpt7,
                    "color": "#DD4477"
                },
                {
                    "title": dptname8,
                    "value": dpt8,
                    "color": "#66AA00"
                },
                {
                    "title": dptname9,
                    "value": dpt9,
                    "color": "#B82E2E"
                },
                {
                    "title": dptname10,
                    "value": dpt10,
                    "color": "#316395"
                }],
                "startDuration": 1,
                "pullOutRadius": 0,
                "color": "#fff",
                "fontSize": 8,
                "titleField": "title",
                "valueField": "value",
                "colorField": "color",
                "labelRadius": -70,
                "labelColor": "#fff",
                "radius": 150,
                "innerRadius": 40,
                "outlineAlpha": 1,
                "outlineThickness": 1,
                "labelText": "[[title]]",
                "balloonText": "[[title]]: [[value]]",
                //});

                //AmCharts.makeChart("chart4", {
                //    "type": "pie",
                //    "bringToFront": true,
                //    "dataProvider": [{
                //        "title": "Present",
                //        "value": 6,
                //        "color": "#BA3233"
                //    },   {
                //        "title": "Absent",
                //        "value": 4,
                //        "color": "#6179C0"
                //    }],
                //    "startDuration": 1,
                //    "pullOutRadius": 0,
                //    "color": "#fff",
                //    "fontSize": 8,
                //    "titleField": "title",
                //    "valueField": "value",
                //    "colorField": "color",
                //    "labelRadius": -27,
                //    "labelColor": "#fff",
                //    "radius": 190,
                //    "innerRadius": 137,
                //    "outlineAlpha": 1,
                //    "outlineThickness": 4,
                //    "labelText": "[[title]]",
                //    "balloonText": "[[title]]: [[value]]",

                "allLabels": [{
                    "text": "Company Total Staff",

                    "bold": true,
                    "size": 20,
                    "color": "#404040",
                    "x": 0,

                    "align": "center",
                    "y": 20

                }]
            });
        </script>
</asp:Content>

