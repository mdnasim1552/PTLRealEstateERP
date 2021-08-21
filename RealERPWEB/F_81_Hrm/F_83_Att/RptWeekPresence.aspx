<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptWeekPresence.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_83_Att.RptWeekPresence" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        #charts {
            width: 600px;
            height: 500px;
            position: relative;
            margin: 0 auto;
            font-size: 12px;
        }

        .chartdiv {
            width: 600px;
            height: 500px;
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
                color: #046971;
            }

            .legnd span:nth-child(6) {
                color: #E56B8D;
            }

            .legnd span:nth-child(7) {
                color: #336699;
            }


            .lblrangeup{
                background: rgba(0, 0, 0, 0) url("../../Images/ArrowUp.png") no-repeat scroll center top;
    float: right;
    height: 60px;
    width: 27px;
            }
             .lblrangedwn{
                background: rgba(0, 0, 0, 0) url("../../Images/ArrowDown.png") no-repeat scroll center bottom;
    float: right;
    height: 60px;
    width: 27px;
            }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
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
                            <div class="col-md-12 pading5px">
                                <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Company</asp:Label>

                                <asp:DropDownList ID="ddlCompany" runat="server" Width="233" CssClass="form-control inputTxt pull-left" AutoPostBack="true" TabIndex="2" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                </asp:DropDownList>

                                <asp:Label ID="lblfrmdate" runat="server" CssClass="smLbl_to">From</asp:Label>
                                <asp:TextBox ID="txtFdate" runat="server" CssClass=" inputDateBox " AutoPostBack="true" OnTextChanged="txtFdate_TextChanged"></asp:TextBox>

                                <cc1:CalendarExtender ID="txtFdate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtFdate"
                                    PopupButtonID="Image2"></cc1:CalendarExtender>

                                <asp:Label ID="Label1" runat="server" CssClass="smLbl_to">To</asp:Label>

                                <asp:TextBox ID="txtTdate" runat="server"  CssClass="inputDateBox "></asp:TextBox>

                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtTdate"
                                    PopupButtonID="Image2"></cc1:CalendarExtender>

                                 <asp:RadioButtonList ID="rpttype" runat="server" AutoPostBack="True" 
                                            BackColor="#DFF0D8" BorderColor="#000" CssClass="rbtnList1 margin5px pull-left"
                                            Font-Bold="True" Font-Size="15px" ForeColor="Black" RepeatDirection="Horizontal">
                           
                                    <asp:ListItem Value="0" Selected="True">All Machine Staff</asp:ListItem>
                                    <asp:ListItem Value="1">All Staff</asp:ListItem>
                                </asp:RadioButtonList>

                              
                                    <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary okBtn pull-left margin5px" OnClick="lnkbtnShow_Click">Ok</asp:LinkButton>

                            </div>
                        </div>


                    </div>
                </fieldset>

                <div class="table-responsive">
                    <div class="col-md-5 col-sm-5 col-lg-5">
                        <div class="row">
                            <asp:GridView ID="gvWeekPresence" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                ShowFooter="false" CssClass="table-striped table-hover table-bordered grvContentarea">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvymonday" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "ymonddesc")).ToString("dd-MMM-yyyy") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Staff">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvStaff" Style="text-align: right" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "staff"))==0.00 ? "0" :Convert.ToDouble(DataBinder.Eval(Container.DataItem, "staff")).ToString("#,##0;(#,##0); ")%>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Present">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvPresent" Style="text-align: right" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "present"))==0.00 ? "0" :Convert.ToDouble(DataBinder.Eval(Container.DataItem, "present")).ToString("#,##0;(#,##0); ")%>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Absent">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvAbsent" Style="text-align: right" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "absnt"))==0.00 ? "0" :Convert.ToDouble(DataBinder.Eval(Container.DataItem, "absnt")).ToString("#,##0;(#,##0); ")%>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                 <%--    <asp:TemplateField HeaderText="On leave">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvonleave" Style="text-align: right" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "onleave"))==0.00 ? "0" :Convert.ToDouble(DataBinder.Eval(Container.DataItem, "absnt")).ToString("#,##0;(#,##0); ")%>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>--%>


                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>



                        <div class="row">
                            <asp:Panel ID="pnlgraph" Visible="false" runat="server">
                                <div style="margin: 10px auto 0;">
                                    <div style="width: 65%; float: left; text-align: center;">
                                        <button class="btn btn-success btn-sm" style="font-size: 25px; cursor:none; margin: 5px 0; font-weight: bold; height: 30px; line-height: 30px; padding: 0; width: 100%;">
                                            Last 7 Days Presence</button>
                                        <asp:Label ID="lbltodaystaff" BorderWidth="0" CssClass="btn btn-primary btn-sm" runat="server"></asp:Label>
                                        <asp:Label ID="lbltodayprs" BorderWidth="0" CssClass="btn btn-primary btn-sm" runat="server"></asp:Label>
                                    </div>
                                    <div style="float: right; font-size: 25px; margin: 5px 0 5px 5px; width: 28%;">
                                        <asp:Label ID="lblresult" CssClass="btn btn-danger btn-sm" Style="border: medium none; cursor:none; color: White; font-size: 40px; padding: 0 5px;"
                                            runat="server"></asp:Label>

                                        <asp:Label ID="lblrangeup" CssClass="lblrangeup" Visible="false" runat="server"></asp:Label>
                                        <asp:Label ID="lblrangedwn" CssClass="lblrangedwn"  Visible="false" runat="server"></asp:Label>

                                    </div>






                                    <div class="clearfix"></div>
                                </div>


                                <asp:Chart ID="chartWeekly" runat="server" Height="250px" Width="500px">
                                    <Series>
                                        <asp:Series ChartArea="ChartArea1" BorderWidth="3" IsValueShownAsLabel="true" Color="#5B9BD5"
                                            MarkerColor="black" MarkerStyle="Circle" Name="Series1" MarkerSize="4" YValuesPerPoint="6" ChartType="Line">
                                        </asp:Series>
                                        <asp:Series ChartArea="ChartArea1" Color="#EE853E" BorderWidth="3" IsValueShownAsLabel="true"
                                            MarkerColor="black" MarkerStyle="Circle" MarkerBorderWidth="30" Name="Series2" MarkerSize="4" YValuesPerPoint="6" ChartType="Line">
                                        </asp:Series>
                                    </Series>
                                    <ChartAreas>



                                        <asp:ChartArea Name="ChartArea1">
                                            <AxisX MaximumAutoSize="100" Interval="1">
                                                <MajorGrid Enabled="false" />


                                            </AxisX>
                                            <AxisY>
                                                <MajorGrid Enabled="False" />
                                            </AxisY>

                                        </asp:ChartArea>


                                    </ChartAreas>
                                    <%-- <Titles>
                                <asp:Title Font="Cambria, 25px" ForeColor="#2E75B6" Name="Title1"
                                    Text="Last 7 Days Presence">
                                </asp:Title>

                            </Titles>--%>
                                </asp:Chart>

                            </asp:Panel>
                        </div>

                    </div>
                    <div class="col-md-7 col-sm-7 col-lg-7">
                        <asp:Panel ID="onDayGraph" runat="server" Visible="false">
                            <script src="../../Scripts/Mchart.js"></script>
                            <script src="../../Scripts/Mchartpie.js"></script>
                            <%--<script src="http://www.amcharts.com/lib/3/themes/light.js"></script>--%>



                            <div id="charts">
                                <div class="btn btn-primary btn-sm legnd">

                                    <span>Total Present</span>
                                    <span>Total Absent</span>
                                    <span>In Time</span>
                                    <span>Late</span>
                                    <span>Early Leave</span>
                                    <span>Absent</span>
                                    <span>On Leave</span>

                                </div>
                                <div id="chart1" class="chartdiv"></div>
                                <div id="chart2" class="chartdiv"></div>
                                <div id="chart3" class="chartdiv"></div>
                                <div id="chart4" class="chartdiv"></div>
                            </div>

                        </asp:Panel>


                    </div>
                </div>
            </div>

            <div class="row">




                <div style="display: none">

                    <asp:TextBox ID="txtcomp1" runat="server"></asp:TextBox>


                    <asp:TextBox ID="txtpresent" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtlate" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtearlylev" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtonleave" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtabsent" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txttostaff" runat="server"></asp:TextBox>


                </div>


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




            var pres = this.parseFloat($("#<%=this.txtpresent.ClientID %>").val());
            var el = this.parseFloat($("#<%=this.txtearlylev.ClientID %>").val());
            var ol = this.parseFloat($("#<%=this.txtonleave.ClientID %>").val());
            var late = this.parseFloat($("#<%=this.txtlate.ClientID %>").val());
            var abs = this.parseFloat($("#<%=this.txtabsent.ClientID %>").val());

            var totalp = Math.round((pres + late + el), 2);
            
            var totalabs = Math.round((abs + ol),2);

            var ttlstaff = this.parseFloat($("#<%=this.txttostaff.ClientID %>").val());
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
                    "title": "Pr(" + pres + ")",
                    "value": pres,
                    "color": "#C3DA57"
                }, {
                    "title":  "L(" + late + ")",
                    "value": late,
                    "color": "#88C760"
                },
                {
                    "title": "EL (" + el + ")",
                    "value": el,
                    "color": "#046971"
                },
                 {
                     "title": "Ab (" + abs + ")",
                     "value": abs,
                     "color": "#E56B8D"
                 },
                {
                    "title": "Ol(" + ol + ")",
                    "value": ol,
                    "color": "#336699"
                }],
                "startDuration": 1,
                "pullOutRadius": 0,
                "color": "#fff",
                "fontSize": 9,
                "titleField": "title",
                "valueField": "value",
                "colorField": "color",
                "labelRadius": -27,
                "labelColor": "#fff",
                "radius": 150,
                "innerRadius": 40,
                "outlineAlpha": 1,
                "outlineThickness": 1,
                "labelText": "[[title]]%",
                "balloonText": "[[title]]: [[value]]"
            });

            AmCharts.makeChart("chart3", {
                "type": "pie",
                "theme": "light",
                
                "bringToFront": true,
                "dataProvider": [{
                   
                    "title":"TPr("+ totalp+")",
                    "value": totalp,
                    "color": "#0FAD5E"

                },

                {
                    "title":"TAb("+totalabs+")",
                    "margin": "0 50px 0 0",
                    "value": totalabs,
                    "color": "#F15922"
                }],
                "startDuration": 1,
                "pullOutRadius": 0,
                "color": "#fff",
                "fontSize": 12,

                "text-align": "center",
                "titleField": "title",
                "valueField": "value",
                "colorField": "color",
                "labelRadius": -27,
                "labelColor": "#fff",
                "radius": 200,
                "innerRadius": 150,
                "outlineAlpha": 1,

   

                "outlineThickness": 1,
                "labelText": "[[title]]%",
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
                    "text": "One Day Presence",

                    "bold": true,
                    "size": 20,
                    "color": "#404040",
                    "x": 0,

                    "align": "center",
                    "y": 20

                }]
            });
        </script>
        <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
    </div>
</asp:Content>
