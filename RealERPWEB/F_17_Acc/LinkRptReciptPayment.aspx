<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkRptReciptPayment.aspx.cs" Inherits="RealERPWEB.F_17_Acc.LinkRptReciptPayment" %>

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

        @keyframes blinker-one {
            0% {
                opacity: 0;
            }
        }
    </style>


    <script src="../Scripts/highchart2.js"></script>
    
    <%--    <script src="../Scripts/highchart2.js"></script>
     
    <script src="../Scripts/drilldown.js"></script>--%>

    <script language="javascript" type="text/javascript">
        var date1,date2;
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            var comcod = <%=this.GetCompCode()%>;
            date1=$("#<%=this.txtDateFrom.ClientID%>").text().trim();
            date2=$("#<%=this.txtDateto.ClientID%>").text().trim();
            $("#hlnkdtlg").attr("href", "RptAccDTransaction.aspx?Type=Accounts&TrMod=DTran&comcod="+comcod+"&Date1="+date1+"&Date2="+date2);
            $("#hlnkdt").attr("href", "RptAccDTransaction.aspx?Type=Accounts&TrMod=RecPay&comcod="+comcod+"&Date1="+date1+"&Date2="+date2);
            $("#hlnkdtres").attr("href", "RptAccDTransaction.aspx?Type=Accounts&TrMod=RecPay02&comcod="+comcod+"&Date1="+date1+"&Date2="+date2);
        }

        

        function ExecuteGraph(bgd, alldata, mainhead) {



            var bgddata = JSON.parse(bgd);
            var mainhead = JSON.parse(mainhead);
            //var alldata = JSON.parse(alldata);
            //var payam = JSON.parse(payam);
            //console.log(bgd);
            console.log(bgddata);

            Highcharts.setOptions({
                lang: {
                    decimalPoint: '.',
                    thousandsSep: ' '
                }
            });
            var iactdesc = [];

            var payam2 = [];
            var actual = [];
            var actual2 = [];

            var payment = [];
            var armainhead = [];
            for (var i = 0; i < mainhead.length; i++) {
                armainhead[i] = mainhead[i]["actdesc"];
            }

            //console.log(payment);
            //for (var key in bgddata) {
            //    if (bgddata.hasOwnProperty(key)) {
            //        iactdesc.push(abgdvsexe[key].actdesc);
            //        payam.push(abgdvsexe[key].payam);
            //        payam2.push(
            //            { name: abgdvsexe[key].actdesc, y: abgdvsexe[key].payam, drilldown: "B" + abgdvsexe[key].mpaycode });
            //        //actual.push(abgdvsexe[key].proper);
            //        //actual2.push(
            //        //    { name: abgdvsexe[key].actdesc, y: abgdvsexe[key].proper, drilldown: "E" + abgdvsexe[key].mpaycode });
            //        var bud = [];
            //        var exec = [];
            //        for (var keyy in alldata) {
            //            if (alldata.hasOwnProperty(keyy)) {
            //                if (abgdvsexe[key].isircode == alldata[keyy].mpaycode) {
            //                    bud.push([alldata[keyy].actdesc, alldata[keyy].bgdper]);
            //                    //exec.push([alldata[keyy].actdesc, alldata[keyy].proper]);
            //                }


            //            }
            //        }
            //        //   console.log(bud);

            //    }
            //}


            $('#containerGraph').highcharts({
                chart: {
                    type: 'bar'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Accounts Details',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },


                xAxis: {
                    type: 'category',
                    labels: {
                        formatter: function () {
                            if ($.inArray(this.value, armainhead) !== -1) {
                                return '<span style="fill: maroon;">' + this.value + '</span>';
                            } else {
                                return this.value;
                            }
                        },
                        style: {
                            color: '#000',

                        }
                    }
                },


                yAxis: {
                    title: {
                        text: 'Taka In Lac'
                    }

                },
                legend: {
                    enabled: false
                },
                plotOptions: {
                    series: {
                        borderWidth: 0,
                        dataLabels: {
                            enabled: true,
                            format: '{point.y:.2f}'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                },

                "series": [
                    {
                        "name": "",
                        "colorByPoint": true,
                        "data":// [
                        //    {
                        //        "name": bgddata[0]['actdesc'],
                        //        "y": bgddata[0]['payam'],
                        //        "color": "#4286f4",


                        //    },
                        //    {
                        //        "name": bgddata[1]['actdesc'],
                        //        "y": bgddata[1]['payam'],
                        //        "color": "#f4a641",

                        //    },
                        //    {
                        //        "name": bgddata[2]['actdesc'],
                        //        "y": bgddata[2]['payam'],
                        //        "color": "#f44141",

                        //    }
                        //]
                        (function () {
                            // generate an array of random data
                            var data = [],

                                i;

                            for (var key in bgddata) {
                                if (bgddata.hasOwnProperty(key)) {
                                    data.push([bgddata[key].actdesc,
                                   bgddata[key].payam, false
                                    ]);
                                }
                            }
                            return data;
                        }())
                    }
                ]
            });

        }



    </script>



    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                                        <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                        <asp:Label ID="txtDateFrom" ClientIDMode="Static" runat="server" CssClass="lblTxt lblName"></asp:Label>
                                        <%--<cc1:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDateFrom" Enabled="true"></cc1:CalendarExtender>--%>


                                        <asp:Label ID="lbltoDate" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                        <asp:Label ID="txtDateto" ClientIDMode="Static" runat="server" AutoCompleteType="Disabled" CssClass="lblTxt lblName"></asp:Label>
                                        <%-- <cc1:CalendarExtender ID="txtDateto_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDateto" Enabled="true"></cc1:CalendarExtender>--%>


                                        <asp:Label ID="lblPage" runat="server" CssClass="smLbl_to">Page</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" Width="50px"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass="ddlPage">
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





                                        <div class="colMdbtn">


                                            <asp:LinkButton ID="btnok" runat="server" CssClass="btn btn-primary okBtn" OnClick="btnok_Click">Ok</asp:LinkButton>


                                        </div>
                                        <div class="pull-right col-md-6" runat="server" id="panellnk">

                                           
                                            <asp:HyperLink ID="hlnkdt" runat="server" Target="_blank" CssClass="btn btn-outline-light" ClientIDMode="Static">Details</asp:HyperLink>

                                            <asp:HyperLink ID="hlnkdtres" runat="server" Target="_blank" CssClass="btn btn-outline-light" ClientIDMode="Static">Details (Resource)</asp:HyperLink>
                                             <asp:HyperLink ID="hlnkdtlg" runat="server" Target="_blank" CssClass="btn btn-outline-light" ClientIDMode="Static">Details(Transaction)</asp:HyperLink>
                                            <%--<asp:CheckBox ID="chkpaymentdt" runat="server" TabIndex="10" Text="Payment Details" CssClass="btn btn-outline-light" BorderStyle="Solid" />--%>
                                        </div>

                                    </div>





                                </div>
                        </fieldset>




                    </div>


                    <div class="row">
                        <div class="col-md-6">
                            <div class="col-md-12">
                                <asp:GridView ID="gvbgdvsexegp" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" ShowFooter="True" Width="475px"
                                    OnRowDataBound="gvBgdVsExgp_RowDataBound">
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

                                                <asp:LinkButton ID="lnkgvWDescgp" runat="server" ForeColor="Black" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))%>'
                                                    Width="165px" OnClick="lnkgvWDescgp_Click"></asp:LinkButton>



                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFDesc" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#800000"> Total :</asp:Label>
                                            </FooterTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />

                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBudgetAmtgp" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtoal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="60px" ForeColor="#800000"></asp:Label>
                                            </FooterTemplate>

                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Percentage(%)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpercent" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lgvFpertoal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="40px" ForeColor="#800000"></asp:Label>
                                            </FooterTemplate>

                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
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
                            <div runat="server" id="abppanel" class="col-md-12" style="margin-top: 50px;" visible="False">
                                <asp:HyperLink CssClass="col-md-4 col-md-offset-2 btn btn-sm btn-success" Style="float: left" Font-Size="13" NavigateUrl="~/F_05_Busi/YearlyPlanningSt.aspx?Type=CBudget" Target="_blank" runat="server">ABP Amount</asp:HyperLink>
                                <asp:Label runat="server" ID="abpamt" Style="color: maroon; margin-left: 20px; margin-top: 10px; font-weight: bold; float: left" Font-Size="13"></asp:Label>
                                <asp:Label runat="server" ID="abpper" Style="color: maroon; margin-left: 45px; margin-top: 10px; font-weight: bold; float: left" Font-Size="13"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-6">

                            <div id="containerGraph" style="width: 580px; height: 500px; margin: 0 auto"></div>
                        </div>
                    </div>
                </div>
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->






      <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>



