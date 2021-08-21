
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptImpExeStatus.aspx.cs" Inherits="RealERPWEB.F_09_PImp.RptImpExeStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .highcharts-drilldown-axis-label {
            color: maroon !important;
            fill: maroon !important;
            font-weight: normal !important;
            text-decoration: none !important;
        }

        .highcharts-point .highcharts-drilldown-point {
            font-weight: normal !important;
            text-decoration: none !important;
            color: #000000 !important;
        }
        /*tspan {
       
                 color:#000000 !important;
                  fill:#000000 !important;
                   font-weight:normal !important;
        }*/
        rect {
            text-decoration: none !important;
        }


        .blink-one {
            animation: blinker-one 1s linear infinite;
        }
        .inputcontrol{
            width:80px !important;
        }

        @keyframes blinker-one {
            0% {
                opacity: 0;
            }

            
        }
    </style>
    <script language="javascript" type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>

    <script src="../Scripts/highchart2.js"></script>


    <%--<script src="http://github.highcharts.com/highcharts.js"></script>
<script src="http://github.highcharts.com/modules/exporting.js"></script>--%>
    <script src="../Scripts/drilldown.js"></script>

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
            var gv = $('#<%=this.gvImpPlan.ClientID %>');
            var gv1 = $('#<%=this.gvPlanVSEx.ClientID %>');
            var gv2 = $('#<%=this.gvBgdVsEx.ClientID %>');
            var gv3 = $('#<%=this.gvMatEva.ClientID %>');
            var gvmaplnmoplnexe = $('#<%=this.gvmplanvaexe.ClientID %>');
            var DayWiseExecution = $('#<%=this.DayWiseExecution.ClientID %>')
            var gvTvsImp = $('#<%=this.gvTvsImp.ClientID %>')

            gv.Scrollable();
            gv1.Scrollable();
            gv2.Scrollable();
            gv3.Scrollable();
            gvmaplnmoplnexe.Scrollable();
            DayWiseExecution.Scrollable();
            gvTvsImp.Scrollable();

            $('.chzn-select').chosen({ search_contains: true });
        }


        function ExecuteGraph(bgd, alldata, floor) {



            var abgdvsexe = JSON.parse(bgd);
            var alldata = JSON.parse(alldata);
            var floor = JSON.parse(floor);
            
            Highcharts.setOptions({
                lang: {
                    decimalPoint: '.',
                    thousandsSep: ' '
                }
            });
            var aisirdesc = [];
            var budget = [];
            var budget2 = [];
            var actual = [];
            var actual2 = [];
            var drildata = [];
            var arfloor = [];
            for (var i = 0; i < floor.length; i++) {
                arfloor[i] = floor[i]["flrdes"];
            }
            console.log(arfloor);
            for (var key in abgdvsexe) {
                if (abgdvsexe.hasOwnProperty(key)) {
                    aisirdesc.push(abgdvsexe[key].isirdesc);
                    budget.push(abgdvsexe[key].bgdper);
                    budget2.push(
                        { name: abgdvsexe[key].isirdesc, y: abgdvsexe[key].bgdper, drilldown: "B" + abgdvsexe[key].isircode });
                    actual.push(abgdvsexe[key].proper);
                    actual2.push(
                        { name: abgdvsexe[key].isirdesc, y: abgdvsexe[key].proper, drilldown: "E" + abgdvsexe[key].isircode });
                    var bud = [];
                    var exec = [];
                    for (var keyy in alldata) {
                        if (alldata.hasOwnProperty(keyy)) {
                            if (abgdvsexe[key].isircode == alldata[keyy].isircode) {
                                bud.push([alldata[keyy].isirdesc, alldata[keyy].bgdper]);
                                exec.push([alldata[keyy].isirdesc, alldata[keyy].proper]);
                            }


                        }
                    }
                    //   console.log(bud);
                    drildata.push({
                        id: 'B' + abgdvsexe[key].isircode,
                        data: bud,
                        name: 'Budget',
                        color: '#b2e8a0',
                        //    ['East', 4],
                        //    ['West', 2],
                        //    ['North', 1],
                        //    ['South', 4]
                        //]
                    }, {
                            id: 'E' + abgdvsexe[key].isircode,
                            data: exec,
                            name: 'Execution',
                            color: 'maroon',

                            //    ['East', 6],
                            //    ['West', 2],
                            //    ['North', 2],
                            //    ['South', 4]
                            //]
                        });
                }
            }

            console.log(budget2);
            console.log(actual2);
            console.log(drildata);
            //console.log(drildata);
            //    }
            //   }



            //   $('#barchart').highcharts({


            //       chart: {
            //           type: 'bar'
            //       },
            //       title: {
            //           text: 'Budget Vs Actual'
            //       },

            //       labels: {
            //           style: {
            //               color: 'white',
            //               fontSize:'12px',
            //               fontWeight:'regular'
            //           }
            //       },

            //       //subtitle: {
            //       //    text: 'Budget vs Execution graph</a>'
            //       //},
            //       xAxis: {
            //           type:'category',
            //           categories: aisirdesc,
            //           title: {
            //               text: null
            //           }
            //       },
            //       yAxis: {
            //           min: 0,
            //           title: {
            //               text: 'Budget vs Acutal (%)',
            //               align: 'high'
            //           },
            //           labels: {

            //               overflow: 'justify',
            //               fontWeight:'regular'
            //           }
            //       },
            //       tooltip: {
            //           valueSuffix: ' in %'
            //       },
            //       plotOptions: {
            //           bar: {
            //               dataLabels: {
            //                   enabled: true
            //               }
            //           }
            //       },




            //       //legend: {
            //       //    itemStyle: {
            //       //    font: '9pt Trebuchet MS, Verdana, sans-serif',
            //       //        color: 'white',
            //       //        fontWeight: 'bold',
            //       //        fontSize: '20px'
            //       //    },
            //       //    layout: 'vertical',
            //       //    align: 'right',
            //       //    fontWeight:'regular',
            //       //    verticalAlign: 'top',
            //       //    x: -40,
            //       //    y: 80,
            //       //    floating: true,
            //       //    borderWidth: 1,
            //       //    backgroundColor: ((Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'),
            //       //    shadow: true
            //       //},
            //       credits: {
            //           enabled: false
            //       },
            //       series: [{
            //           name: 'Budget',
            //           data: budget,
            //       }, {
            //           name: 'Actual',
            //           data: actual,
            //       }]
            //   });

            //   $('#barchart2').highcharts({

            //       chart: {
            //           type: 'bar'
            //       },
            //       //events: {
            //       //    drilldown: function (e) {
            //       //        //this.xAxis[0].update({
            //       //        //    categories: ['aaa', 'ccc']
            //       //        //});
            //       //        alert("sfsfsf");
            //       //    }
            //       //},
            //       title: {
            //           text: 'Monthly Average Rainfall'
            //       },
            //       subtitle: {
            //           text: 'Source: WorldClimate.com'
            //       },
            //       xAxis: {
            //           categories: aisirdesc,
            //       },
            //       yAxis: {
            //           min: 0,
            //           title: {
            //               text: 'Rainfall (mm)'
            //           }
            //       },
            //       tooltip: {
            //           headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            //           pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
            //               '<td style="padding:0"><b>{point.y:.1f} mm</b></td></tr>',
            //           footerFormat: '</table>',
            //           shared: true,
            //           useHTML: true
            //       },
            //       plotOptions: {
            //           column: {
            //               pointPadding: 0.2,
            //               borderWidth: 0
            //           }
            //       },
            //       series: [{
            //           name: 'Budget',
            //           data: budget,

            //       }, {
            //           name: 'Execution',
            //           data: actual2,

            //       }],
            //       drilldown: {

            //          series: [

            ////               {
            ////               categories: ['Chrome 5.0', 'Chrome 6.0'],
            ////               id: '410100000000',
            ////               data: [{ name: "First Floot", data: [120, 100]}, {name: "Second Floot", data: [120, 100] }],
            ////           },
            //{

            //    id: '410100000000',
            //    data: [
            //        ['Apples', 4],
            //        ['Oranges', 2]
            //    ]

            //}
            //                                ]

            //           //series: [{                       
            //           //    id: '410100000000',
            //           //    name:"Budget",
            //           //    data: budget,


            //           //}],
            //       }

            //   });
            console.log(abgdvsexe);
            
            $('#container').highcharts({
                chart: {
                    type: 'bar'
                },
                title: {
                    text: 'Budget VS Execution'


                },

                xAxis: {
                    type: 'category',
                    labels: {
                        formatter: function () {
                            if ($.inArray(this.value, arfloor) !== -1) {
                                return '<span style="fill: black;">' + this.value + '</span>';
                            } else {
                                return this.value;
                            }
                        },
                        style: {
                            color: '#000',

                        }
                    }
                },

                plotOptions: {
                    series: {
                        stacking: 'normal',
                        borderWidth: 0,
                        dataLabels: {
                            enabled: true
                        }
                    }
                },

                series: [{
                    name: 'Budget',
                    color: '#b2e8a0',                    
                    data: budget2,
                }, {
                    name: 'Execution',
                    color: 'maroon',
                    //data: [{
                    //    name: 'Republican',
                    //    y: 4,
                    //    drilldown: 'republican-2014'
                    //}, {
                    //    name: 'Democrats',
                    //    y: 4,
                    //    drilldown: 'democrats-2014'
                    //}, {
                    //    name: 'Other',
                    //    y: 4,
                    //    drilldown: 'other-2014'
                    //}]
                    data: actual2,

                }],
                drilldown: {
                    series: drildata,

                }
            });

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
            <asp:Label ID="lbljavascript" runat="server"></asp:Label>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Project Name</asp:Label>
                                        <asp:TextBox ID="txtSrcProject" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="chzn-select form-control inputTxt pull-left chzn-select" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                        </asp:DropDownList>



                                    </div>
                                    <div class="pull-left">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                    </div>

                                    <asp:Label ID="lbltk" runat="server" Visible="false" CssClass="lblTxt lblName" Style="font-size: 16px;">Taka in Lac </asp:Label>

                                </div>
                                <div class="form-group">
                                    <div class="col-md-7 pading5px">
                                        <asp:Label ID="lbldatefrm" runat="server" CssClass="lblTxt lblName">Date</asp:Label>
                                        <asp:TextBox ID="txtfrmDate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfrmDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfrmDate"></cc1:CalendarExtender>

                                        <asp:Label ID="lbldateto" runat="server" CssClass=" smLbl_to">To</asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                    </div>

                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="lblprostdate" runat="server" CssClass=" smLbl_to" Visible="false">Project Start Date:</asp:Label>
                                        <asp:Label ID="lblvalprostdate" runat="server" CssClass=" inputtextbox" Visible="false" Style="width: 80px !important"></asp:Label>

                                        <asp:Label ID="lblproendate" runat="server" CssClass=" smLbl_to" Visible="false">End Date:</asp:Label>
                                        <asp:Label ID="lblvalproendate" NavigateUrl="hlnkvalproendate" runat="server" Target="_blank" CssClass=" inputtextbox" Width="80px" Visible="false"></asp:Label>



                                        <asp:Label ID="lblprodelduratin" runat="server" CssClass=" smLbl_to" Visible="false" Style="font-weight: normal; color: maroon">Delay:</asp:Label>



                                        <span class="blink-one">
                                            <asp:HyperLink ID="hlnkvalprodelduratin" NavigateUrl="hlnkvalproendate" runat="server" Target="_blank" CssClass=" inputtextbox" Style="background: blue; color: white; font-weight: bold; font-size: 14px; width:100px; text-align: center;" Visible="false"></asp:HyperLink></span>

                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName">Page Size</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="lblflrlist" runat="server" CssClass=" smLbl_to">Floor</asp:Label>
                                        <asp:DropDownList ID="ddlFloorListRpt" runat="server" AutoPostBack="True" CssClass="ddlPage">
                                        </asp:DropDownList>

                                        <asp:Label ID="lblRptGroup" runat="server" CssClass="smLbl_to">Group</asp:Label>
                                        <asp:DropDownList ID="ddlRptGroup" runat="server" AutoPostBack="True" CssClass="ddlPage">
                                            <asp:ListItem>Main</asp:ListItem>
                                            <asp:ListItem>Sub-1</asp:ListItem>
                                            <asp:ListItem>Sub-2</asp:ListItem>
                                            <asp:ListItem>Sub-3</asp:ListItem>
                                            <asp:ListItem Selected="True">Details</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>


                                    <div class="col-md-5 pading5px">
                                        <asp:CheckBox ID="chkNarration" runat="server" TabIndex="10" Text="With Narration" CssClass="btn btn-primary checkBox" Checked="true" Visible="false" />
                                    </div>

                                </div>

                            </div>
                        </fieldset>
                    </div>

                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="ViewImpPlan" runat="server">
                            <div class="table table-responsive">
                                <asp:GridView ID="gvImpPlan" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" ShowFooter="True" Width="104px" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnPageIndexChanging="gvImpPlan_PageIndexChanging" PageSize="20">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Floor Desc.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvflrdesc" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Work Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcWDesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc1")) %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUnit" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrate" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Previous Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPreqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Previous Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPreAmt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPreAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Current Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCurqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Current Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCurAmt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFCurAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvTqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvTAmt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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

                        <asp:View ID="ViewPlanVSEx" runat="server">
                            <div class="table table-responsive">
                                <asp:GridView ID="gvPlanVSEx" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvPlanVSEx_PageIndexChanging"
                                    ShowFooter="True" PageSize="20">


                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Floor Desc.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvflrdesc0" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Work Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcWDesc0" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc1")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUnit0" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrate0" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pre.Plan Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPrePlanqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ppreqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" Pre.Plan Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPrePlanAmt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ppreamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPrePlanAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" Cur.Plan Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCurPlanqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pcurqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" Cur.Plan Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPlanAmt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pcuramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPlanAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tot.Plan Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvToPlanqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ptoqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" To.Plan Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvToPlanAmt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ptoamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFToPlanAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" Pre.Exe Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPreExqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "epreqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" Pre.Exe Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPreExAmt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "epreamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPreExAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cur.Exe Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvExCurqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ecurqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cur.Exe Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvExCurAmt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ecuramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFCurExAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="To.Exe Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvToExqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "etoqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="To.Exe Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvToExAmt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "etoamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFToExAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Variance(Qty)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvVqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "vtoqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Varinace(Amt)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvVtoAmt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "vtoamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFVtoAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                        <asp:View ID="BgdVsEx" runat="server">
                            <div class="table table-responsive">
                                <asp:GridView ID="gvBgdVsEx" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" ShowFooter="True" Width="831px" PageSize="20"
                                    OnPageIndexChanging="gvBgdVsEx_PageIndexChanging" OnRowDataBound="gvBgdVsEx_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Floor Desc.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvflrdesc1" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Work Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcWDesc1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc1")) %>'
                                                    Width="230px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <table style="width: 250px; height: 31px;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="#000" Text="Total Amt."></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="#000" Text="Percentage(finished)"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUnit1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bgd.Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrate1" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Budget Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbgdqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Budget Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBudgetAmt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <table style="width: 2%; height: 19px;">
                                                    <tr>
                                                        <td class="style32">
                                                            <asp:Label ID="lgvFBgdAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style32">
                                                            <asp:Label ID="lgvFPercent" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="#000" Height="20px" Style="text-align: right" Width="70px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Execution Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvexqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "eqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Execution Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvExAmt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "eamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <table style="width: 9%; height: 38px;">
                                                    <tr>
                                                        <td class="style32">
                                                            <asp:Label ID="lgvFexAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style32">&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Var. Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvvarqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "varqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Var. Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvvaramt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "varamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>

                                                <table style="width: 9%; height: 38px;">
                                                    <tr>
                                                        <td class="style32">

                                                            <asp:Label ID="lgvFvaramt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="#000" Style="text-align: right" Width="75px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style32">&nbsp;</td>
                                                    </tr>
                                                </table>


                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Percent">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvprcent" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prcent")).ToString("#,##0.00;(#,##0.00); ")+"%" %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                        <asp:View ID="MatEva" runat="server">
                            <div class="table table-responsive">
                                <asp:GridView ID="gvMatEva" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvMatEva_PageIndexChanging" PageSize="20"
                                    ShowFooter="True" Width="818px">
                                    <PagerSettings Position="TopAndBottom" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcWDesc2" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc1")) %>'
                                                    Width="350px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUnit2" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Purchase Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvexqty0" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "eqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Bgd.Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrate2" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Actual.Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvacrate" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "erat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Var.Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvvarrate" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "varrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Var. Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvvaramtm" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "varamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFvaramtm" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                        <asp:View ID="MaPlanVsPlanVsEx" runat="server">
                            <%--OnRowCreated="gvmplanvaexe_RowCreated" OnRowDataBound="gvmplanvaexe_RowDataBound"--%>
                            <div class="row">
                                <asp:GridView ID="gvmplanvaexe" runat="server" AllowPaging="True"
                                    CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" ShowFooter="True" Width="831px" PageSize="20"
                                    OnPageIndexChanging="gvmplanvaexe_PageIndexChanging">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo10" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Floor Desc.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvflrdesc10" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Work Description">
                                            <FooterTemplate>
                                                <table style="width: 16%; height: 33px;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label6emxe" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="#000" Text="Total Amt."></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label7emxe" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="#000" Text="Execution % Based On Master Plan" Width="190px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgcWDesc10" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                    Width="230px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUnit10" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bgd.Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrate10" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="M. Plan Qty.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvmplanqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mapqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Monthly Qty.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBudgetAmtimexe" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <%--<table style="width:2%; height: 19px;">
                                                                <tr>
                                                                    <td class="style32">
                                                                        <asp:Label ID="lgvFBgdAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                            ForeColor="#000" style="text-align: right" Width="70px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style32">
                                                                        <asp:Label ID="lgvFPercent" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                            ForeColor="#000" Height="20px" style="text-align: right" Width="70px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>--%>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Execution Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvexqtyimexe" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "eqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="M.Plan Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvmpamtimexe" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mpamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>

                                                <table style="width: 10%; height: 33px;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lgvFmpamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>

                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Monthly Plan Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvmonthamtimexe" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>

                                                <table style="width: 10%; height: 33px;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lgvFmonthamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="#000" Style="text-align: right" Width="75px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>




                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Execution. Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvexeamt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "examt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>


                                                <table style="width: 10%; height: 33px;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lgvFexeamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="#000" Style="text-align: right" Width="75px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>

                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Percent">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvprcent" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "experc")).ToString("#,##0.00;(#,##0.00); ")+(Convert.ToDouble((DataBinder.Eval(Container.DataItem, "experc")))>0?"%":"") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>


                                                <table style="width: 10%; height: 33px;">
                                                    <tr>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lgvFexepercentage" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="#000" Style="text-align: right" Width="75px"></asp:Label></td>
                                                    </tr>
                                                </table>

                                            </FooterTemplate>



                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />

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
                        <asp:View ID="DayWiseExecution" runat="server">
                            <div class="row">

                                <fieldset class="scheduler-border fieldset_B">

                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-3 pading5px">
                                                <asp:Label ID="lblCatagory" runat="server" CssClass="lblTxt lblName" Text="Catagory"></asp:Label>
                                                <asp:DropDownList ID="ddlcatagory" runat="server" CssClass="ddlPage chzn-select  inputTxtinputTxt" Width="185px" TabIndex="12" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </div>






                                        </div>


                                    </div>
                            </div>
                    </fieldset>
                </div>


                <div class="table table-responsive">
                    <asp:GridView ID="gvExecution" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        AutoGenerateColumns="False" OnPageIndexChanging="gvExecution_PageIndexChanging"
                        PageSize="20" ShowFooter="True" Width="104px"
                        OnRowDataBound="gvExecution_RowDataBound">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo11" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="isircode" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvisircode" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isircode")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle ForeColor="#000" HorizontalAlign="Left" Font-Size="12" Font-Bold="true" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Project" Visible="false">

                                <ItemTemplate>
                                    <asp:Label ID="lblgvproject" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                        Width="110px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle ForeColor="#000" HorizontalAlign="Left" Font-Size="12" Font-Bold="true" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Issue #">
                                <FooterTemplate>
                                    <asp:Label ID="lblFTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                        Text="Grand Total"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvisuno" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isuno1")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle ForeColor="#000" HorizontalAlign="Left" Font-Size="12" Font-Bold="true" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvisudate" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isudat")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Ref. No">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvrefno" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isuref")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Floor Desc.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvflrdescexe" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Work Description">
                                <ItemTemplate>
                                    <asp:Label ID="lgcWDescexe" runat="server" CssClass="textwrap"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc")) %>'
                                        Width="320px"></asp:Label>

                                    <asp:TextBox ID="lgcWDescexenar" runat="server" TextMode="MultiLine"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc")) %>'
                                        Width="320px"></asp:TextBox>
                                </ItemTemplate>



                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit ">
                                <ItemTemplate>
                                    <asp:Label ID="lgvUnitexe" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirunit")) %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Work Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lgvisuqty" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Rate">
                                <ItemTemplate>
                                    <asp:Label ID="lgvrateexe" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isurate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText=" Work Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvissueAmt" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFIssueAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                <asp:View ID="ViewTvsPlan" runat="server">
                    <div class="table table-responsive">
                        <asp:GridView ID="gvTvsImp" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" OnPageIndexChanging="gvTvsImp_PageIndexChanging"
                            PageSize="20" ShowFooter="True" Width="104px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo12" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Work Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcWDesctvp" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc1")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Floor Desc.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvflrdesctvp" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvUnittvp" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvratetvp" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="System Generated Work Target Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtrqtytvp" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" System Generated Work Target Amt">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtramttvp" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tramt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtramttvp" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="75px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actual Target Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvImpqtytvp" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "impqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actual Target Amt">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvimpAmttvp" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "impamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFimpAmttvp" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="75px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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

                <asp:View ID="ViewBgdVsExe02" runat="server">

                    <div class="row">
                        <div class="col-md-6">
                            <asp:GridView ID="gvbgdvsexegp" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" ShowFooter="True" Width="475px"
                                OnRowDataBound="gvBgdVsExgp_RowDataBound" OnRowCreated="gvbgdvsexegp_RowCreated">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo1gp" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>

                                            <asp:LinkButton ID="lnkgvWDescgp" runat="server" ForeColor="Black" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc")) 
                                                                           %>'
                                                Width="130" OnClick="lnkgvWDescgp_Click"></asp:LinkButton>



                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Budget">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvBudgetAmtgp" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Budget  as of Today">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvBudgetatAmtgp" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdatam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Actual">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvExAmtgp" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "exeam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Budgeted">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvbgdpro" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdper")).ToString("#,##0.00;(#,##0.00); ")+ (Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdper"))>0?"%":"") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Actual">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvacpro" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proper")).ToString("#,##0.00;(#,##0.00); ")+ (Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proper"))>0?"%":"") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Budgeted">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvbgddur" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgddur")).ToString("#,##0;(#,##0); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Actual">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlnkgvexedur" runat="server" Style="text-align: right" Target="_blank"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "exedur")).ToString("#,##0;(#,##0); ") %>'
                                                Width="40px"></asp:HyperLink>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                </Columns>

                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />

                            </asp:GridView>



                        </div>


                        <div class="col-md-6" style="border: 1px solid #D8D8D8">


                            <%--<asp:Chart ID="Chart1" runat="server" Width="500px" Height="730px">
                                        <Series>
                                            <asp:Series ChartArea="ChartArea1" ChartType="Bar" Color="BlueViolet"
                                                MarkerColor="black" MarkerStyle="None" Name="Series1" MarkerSize="4">
                                            </asp:Series>

                                            <asp:Series ChartArea="ChartArea1" ChartType="Bar" Color="YellowGreen"
                                                MarkerColor="black" MarkerStyle="None" Name="Series2" MarkerSize="4">
                                            </asp:Series>




                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1">
                                                <AxisX MaximumAutoSize="100" Interval="1" LineColor="YellowGreen">
                                                    <MajorGrid Enabled="false" />

                                                </AxisX>
                                                <%--  <Area3DStyle Enable3D="True" />

                                               
                                            </asp:ChartArea>
                                        </ChartAreas>

                                        <Legends>
                                            <asp:Legend Docking="Top" Alignment="Center"></asp:Legend>
                                        </Legends>
                                    </asp:Chart> --%>
                            <%--  <div id="barchart" style="width: 700px; height: 730px; margin: 0 auto"></div>--%>
                            <div id="container" style="width: 620px; height: 730px; margin: 0 auto"></div>
                        </div>




                    </div>

                </asp:View>
                </asp:MultiView>
            </div>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

