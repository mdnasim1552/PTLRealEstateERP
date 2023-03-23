<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="ProjectAnalysis.aspx.cs" Inherits="RealERPWEB.F_32_Mis.ProjectAnalysis" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="<%=this.ResolveUrl("~/Scripts/highchartwithmap.js")%>"></script>

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            $('.chzn-select').chosen({ search_contains: true });
        });
        function pageLoaded() {

            try {

                $("input, select").bind("keydown", function (event) {
                    var k1 = new KeyPress();
                    k1.textBoxHandler(event);
                });




                <%--var gvprjanalysis = $('#<%=this.gvprjanalysis.ClientID %>');

                gvprjanalysis.gridviewScroll({
                    width: 2040,
                    height: 420,
                    arrowsize: 30,
                    railsize: 16,
                    barsize: 12,
                    varrowtopimg: "../Image/arrowvt.png",
                    varrowbottomimg: "../Image/arrowvb.png",
                    harrowleftimg: "../Image/arrowhl.png",
                    harrowrightimg: "../Image/arrowhr.png",
                    freezesize: 9
                });--%>

              <%--  $('#<%=this.gvprjanalysis.ClientID%>').tblScrollable();--%>
                $('.chzn-select').chosen({ search_contains: true });
            }

            catch (e) {
                alert(e);
            }

        };
    </script>

    <style>
        .mt20 {
            margin-top: 20px;
        }

        .ml2 {
            margin-left: 3px;
        }

        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 29px !important;
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
            <div class="card card-fluid mt-2 mb-1">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-1.5">
                            <div class="form-group">
                                <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName" Text="Date"></asp:Label>
                                <asp:TextBox ID="txtdate" runat="server" CssClass=" form-control form-control-sm" TabIndex="3"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblgroup" runat="server" CssClass="lblTxt lblName" Text="Group:"></asp:Label>
                                <asp:DropDownList ID="ddlprjgroup" runat="server" CssClass=" chzn-select form-control form-control-sm">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnoK" runat="server" CssClass="btn btn-primary btn-sm mt20 ml2" OnClick="lbtnoK_OnClick">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:HyperLink ID="hlnkprjgrp" runat="server" Target="_blank" NavigateUrl="~/F_22_Sal/ProjectGroup" CssClass="btn btn-success btn-sm mt20">Project Group</asp:HyperLink>
                                <asp:HyperLink ID="hlnkprjgrpcode" runat="server" Target="_blank" NavigateUrl="~/F_22_Sal/SalesCodeBook?Type=Sales" CssClass="btn btn-warning btn-sm mt20 ml2">Project Group Code</asp:HyperLink>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card card-fluid  mt-0">
                <div class="card-body" style="min-height: 500px;">
                    <div class="row">
                        <asp:GridView ID="gvprjanalysis" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-bordered grvContentarea"
                            OnRowDataBound="gvprjanalysis_OnRowDataBound" ShowFooter="True">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Description">
                                    <ItemTemplate>

                                        <asp:Label ID="lblpactcode" runat="server" Visible="False" ForeColor="Black" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                            Width="115px" OnClick="lnkgvprjanalysis_OnClick"></asp:Label>

                                        <asp:LinkButton ID="lnkgvprjanalysis" runat="server" ForeColor="Black" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="115px" OnClick="lnkgvprjanalysis_OnClick"></asp:LinkButton>
                                    </ItemTemplate>

                                    <FooterTemplate>

                                        <asp:HyperLink ID="hlbtntbCdataExel" runat="server"
                                            CssClass="btn  btn-success  btn-xs" ToolTip="Export Excel"><span class="fa  fa-file-excel "></span></asp:HyperLink>
                                        <asp:Label runat="server" Font-Bold="true" Font-Size="13px" >Total Amount</asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" Height="28px" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                

                                <asp:TemplateField HeaderText="Total Sales </br>Target">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkbgdamt" Target="_blank" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tosalval")).ToString("#,##0;-#,##0; ")  %>'
                                            Width="80px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtsaltg" runat="server" Font-Size="11px" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>

                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Sales">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnktsal" Target="_blank" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salamt")).ToString("#,##0;(#,##0); ")  %>'
                                            Width="80px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtsal" runat="server" Font-Size="11px" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>

                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Sales</br> Dues">
                                    <ItemTemplate>
                                        <asp:Label ID="tsaldue" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsaldue")).ToString("#,##0;(#,##0); ")  %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtsaldue" runat="server" Font-Size="11px" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>

                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Collection">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnktcol" runat="server" Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "collamt")).ToString("#,##0;(#,##0); ")  %>'
                                            Width="80px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtsalcol" runat="server" Font-Size="11px" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>

                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total </br>Collection  </br> Dues">
                                    <ItemTemplate>
                                        <asp:Label ID="tcoldues" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcoldue")).ToString("#,##0;(#,##0); ")  %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtcoldue" runat="server" Font-Size="11px" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>

                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Sales </br> & Collec. Dues">
                                    <ItemTemplate>
                                        <asp:Label ID="tsalcoldues" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsalcoldue")).ToString("#,##0;(#,##0); ")  %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtsalcoldue" runat="server" Font-Size="11px" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>

                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Sales </br>(%)</br> from </br>S.Tar.">
                                    <ItemTemplate>
                                        <asp:Label ID="salperstg" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salperstg")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Collec.</br>(%) </br>from</br> Sales">
                                    <ItemTemplate>
                                        <asp:Label ID="colpersal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "colpersal")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="45px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Collec.</br>(%) </br> from </br>S. tar.">
                                    <ItemTemplate>
                                        <asp:Label ID="scolperstg" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "colperstg")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="45px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cons. Budget">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkconsbgd" Target="_blank" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cbgdamt")).ToString("#,##0;(#,##0); ")  %>'
                                            Width="80px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFconbgd" runat="server" Font-Size="11px" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>

                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Budgeted Total">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkbgdtotal" Target="_blank" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acbgdamt")).ToString("#,##0;(#,##0); ")  %>'
                                            Width="80px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFbgdt" runat="server" Font-Size="11px" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>

                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actual Cost">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkcost" Target="_blank" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0;(#,##0); ")  %>'
                                            Width="80px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFactcost" runat="server" Font-Size="11px" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>

                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bgd. Profit">
                                    <ItemTemplate>
                                        <asp:Label ID="bgdprofit" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdprofit")).ToString("#,##0;(#,##0); ")  %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFbgdprft" runat="server" Font-Size="11px" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>

                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Projected </br> Profit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvprojectedprofit" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pronetprofit")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFprojectedprofit" runat="server" Font-Size="11px" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>

                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="Profit</br> %">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvproprofitpercnt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "propercnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFproprofitpercnt" runat="server" Font-Size="11px" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>

                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Cash Flow">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcashflow" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cashflow")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFcashflow" runat="server" Font-Size="11px" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>

                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                            </Columns>

                            <FooterStyle CssClass="grvFooter" />
                                                                <EditRowStyle />
                                                                <AlternatingRowStyle />
                                                                <PagerStyle CssClass="gvPagination" />
                                                                <HeaderStyle CssClass="grvHeader" />
                            <RowStyle CssClass="grvRowsNew" />

                        </asp:GridView>
                    </div>
                    <div class="row" style="margin-top: 50px;">
                        <div id="tab1primary" style="float: left;">
                            <div id="ProjAnabar" style="width: 500px; height: 250px;"></div>
                        </div>
                        <div id="tab2primary" style="float: left; margin-left: 80px;">
                            <div id="ProjAnapie" style="width: 500px; height: 250px;"></div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>



    <script language="javascript" type="text/javascript">
        function PrjAnalysisGraph(data) {
            console.log(JSON.parse(data));
            var sdata = JSON.parse(data);

            Highcharts.setOptions({
                lang: {
                    decimalPoint: '.',
                    thousandsSep: ' '
                }
            });

            $('#ProjAnabar').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Project Analysis (Bar Charts)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }

                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'Amount in Crore'
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
                            format: '{point.y:.1f}Tk'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}%</b> of total<br/>'
                },

                "series": [
                    {
                        "name": "Project Analysis",
                        "colorByPoint": true,
                        "data": [
                            {
                                "name": "Total Sales",
                                "y": sdata[0]["salamt"],

                            },
                            {
                                "name": "Total Sales Dues",
                                "y": sdata[0]["tsaldue"],

                            },
                            {
                                "name": "Total Collection",
                                "y": sdata[0]["collamt"],

                            },
                            {
                                "name": "Total collection Dues",
                                "y": sdata[0]["tcoldue"],

                            },
                            {
                                "name": "Total Sales & Collection Dues",
                                "y": sdata[0]["tsalcoldue"]

                            }


                        ]
                    }
                ]
            });

            $('#ProjAnapie').highcharts({


                chart: {
                    type: 'pie'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Project Analysis (Pie Charts)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }

                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'Amount in Crore'
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
                            format: '{point.y:.1f}Tk'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}%</b> of total<br/>'
                },

                "series": [
                    {
                        "name": "Project Analysis",
                        "colorByPoint": true,
                        "data": [
                            {
                                "name": "Total Sales",
                                "y": sdata[0]["salamt"],

                            },
                            {
                                "name": "Total Sales Dues",
                                "y": sdata[0]["tsaldue"],

                            },
                            {
                                "name": "Total Collection",
                                "y": sdata[0]["collamt"],

                            },
                            {
                                "name": "Total collection Dues",
                                "y": sdata[0]["tcoldue"],

                            },
                            {
                                "name": "Total Sales & Collection Dues",
                                "y": sdata[0]["tsalcoldue"]

                            }


                        ]
                    }
                ]
            });


        }

    </script>
</asp:Content>

