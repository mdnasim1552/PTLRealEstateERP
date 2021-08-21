<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptPrjCostPerSFT.aspx.cs" Inherits="RealERPWEB.F_32_Mis.RptPrjCostPerSFT" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

   <script src="<%=this.ResolveUrl("~/Scripts/highchartwithmap.js")%>"></script>
    

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });

        function pageLoaded() {
           <%-- var gv = $('#<%=this.dgvAccRec.ClientID %>');
            gv.Scrollable();--%>

             var dgvAccRec02 = $('#<%=this.gvRCost.ClientID %>');

             dgvAccRec02.gridviewScroll({
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
           
            //  $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

        }


        //function FunBarChart(data)
        //{
        //    var ardate = JSON.parse(data);
        //   // alert = ardate.bgdam;
        //    alert(ardate.bgdam);


        //}



        function FunBarChart() {

            
       


            var bgdam = parseFloat($('#<%=this.txtbgdam.ClientID%>').val());
            var puram = parseFloat($('#<%=this.txtpuram.ClientID%>').val());
            var rbgdam = parseFloat($('#<%=this.txtrbgdam.ClientID%>').val());
            var reqbgd = parseFloat($('#<%=this.txtreqbgd.ClientID%>').val());
            var costincur = parseFloat($('#<%=this.txtcostincur.ClientID%>').val());
            var costsave = parseFloat($('#<%=this.txtcostsave.ClientID%>').val());
            var fuinfla = parseFloat($('#<%=this.txtfuinfla.ClientID%>').val());
            var fusave = parseFloat($('#<%=this.txtfusave.ClientID%>').val());
            var infla = parseFloat($('#<%=this.txtinfla.ClientID%>').val());
            var princur = parseFloat($('#<%=this.txtprincur.ClientID%>').val());
            var fuincur = parseFloat($('#<%=this.txtfuincur.ClientID%>').val());

        

            Highcharts.setOptions({
                lang: {
                    decimalPoint: '.',
                    thousandsSep: ''
                }
            });
            

            $('#inflation').highcharts({




                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: '',
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
                            format: '{point.y:.2f}'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b>'
                },

                "series": [
                    {
                        "name": "",
                        "colorByPoint": true,
                        "data": [
                            {
                                "name": "Budgted Amount",
                                "y": bgdam,
                                "color": 'BlueViolet',

                            },
                            {
                                "name": "Purchse Amount",
                                "y": puram,
                                "color": 'BlueViolet',

                            },
                            {
                                "name": "Budgted Remaining",
                                "y": rbgdam,
                                "color": 'BlueViolet',

                            },
                             {
                                 "name": "Present Inflation",
                                 "y":  princur,
                                 "color": '#a33509',

                             },
                              {
                                  "name": "Forcasted Inflation",
                                  "y": fuincur,
                                  "color": '#a33509',

                              },
                            {
                                "name": "Budget Required",
                                "y": reqbgd,
                                "color": 'BlueViolet',

                            }
                           

                       

                        ]
                    }
                ]
            });



            //$('#inflabreak').highcharts({




            //    chart: {
            //        type: 'column'
            //    },
            //    title: {
            //        text: ''
            //    },
            //    subtitle: {
            //        text: '',
            //        style: {
            //            color: '#44994a',
            //            fontWeight: 'bold'
            //        }

            //    },
            //    xAxis: {
            //        type: 'category'
            //    },
            //    yAxis: {
            //        title: {
            //            text: 'Amount in Crore'
            //        }

            //    },
            //    legend: {
            //        enabled: false
            //    },
            //    plotOptions: {
            //        series: {
            //            borderWidth: 0,
            //            dataLabels: {
            //                enabled: true,
            //                format: '{point.y:.2f}'
            //            }
            //        }
            //    },

            //    tooltip: {
            //        headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
            //        pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
            //    },

            //    "series": [
            //        {
            //            "name": "",
            //            "colorByPoint": true,
            //            "data": [
            //                {
            //                    "name": "Cost Incured",
            //                    "y": costincur,
            //                    "color": '#a33509',

            //                },
            //                {
            //                    "name": "Savings",
            //                    "y": costsave,
            //                    "color": '#a33509',

            //                },
            //                {
            //                    "name": "Future-Incured",
            //                    "y":  fuinfla,
            //                    "color": '#a33509',

            //                },
            //                {
            //                    "name": "Future-Saving",
            //                    "y": fusave,
            //                    "color": '#a33509',

            //                }
                           
                            



            //            ]
            //        }
            //    ]
            //});
            
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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblProjectNme" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                        <asp:LinkButton ID="ImgbtnFindProj" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindProj_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>



                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlAccProject" runat="server" CssClass="chzn-select form-control inputTxt" TabIndex="3" Style="width: 336px;"></asp:DropDownList>

                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lbtnShow" runat="server" CssClass="btn btn-primary okBtn margin5px" OnClick="lbtnShow_Click" TabIndex="11" Style="margin-left: -14px;">Show</asp:LinkButton>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName" Text="Page Size"></asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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
                                    <div class="col-md-6 pading5px">



                                        <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName" Text="Date"></asp:Label>
                                        <asp:TextBox ID="txtdate" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>

                                        <asp:Label ID="lblgroup" runat="server" CssClass=" smLbl_to" Text="Group"></asp:Label>
                                        <asp:DropDownList ID="ddlRptGroup" runat="server" CssClass="ddlPage">
                                            <asp:ListItem>Main</asp:ListItem>
                                            <asp:ListItem>Sub-1</asp:ListItem>
                                            <asp:ListItem>Sub-2</asp:ListItem>
                                            <asp:ListItem>Sub-3</asp:ListItem>
                                            <asp:ListItem Selected="True">Details</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="lblmsg" runat="server" CssClass=" btn btn-danger primaryBtn"></asp:Label>



                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="ViewCostSPerSFT" runat="server">

                                <div class="form-group">
                                    <div class="col-md-3 pading5px">
                                        <asp:Label ID="lblConArea" runat="server" CssClass="btn btn-success primaryBtn" Text="Construction Area:"
                                            Visible="False"></asp:Label>
                                        <asp:Label ID="lbltxtCArea" runat="server" Visible="False"></asp:Label>
                                    </div>
                                </div>
                                <asp:GridView ID="gvCost" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvCost_PageIndexChanging"
                                    ShowFooter="True" Width="902px">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterText="Total" HeaderText="Resource Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvresdesc" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUnit" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                    Width="25px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Budgeted Amt.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbamt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFbamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="White" Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actual Amt.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="White" Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Budgeted Cost/SFT">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbcncst" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bcncst")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFbcncst" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="White" Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actual Cost/SFT">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcncst" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cncst")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFcncst" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="White" Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Budgeted Cost/SFT">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbSlcst" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bslcst")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFbslcst" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="White" Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actual Cost/SFT">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcnslcst" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cnslcst")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFcnslcst" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="White" Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>



                            </asp:View>
                            <asp:View ID="ViewRemainCost" runat="server">
                                <div class="row-fluid">

                                    <div class="col-md-8">
                                        <div id="inflation" style=" height: 250px; margin: 0 auto; border:1px solid #D8D8D8"></div>
                                    </div>

                                   <%-- <div class="col-md-6">
                                        <div id="inflabreak" style=" height: 250px; margin: 0 auto; border:1px solid #D8D8D8"></div>
                                    </div>--%>

                                </div>

                                <div class="row">

                                    <div class="col-md-12">

                                         <asp:Label ID="lblMatCost" runat="server" CssClass="  inputlblVal"   Text="Material Cost" Visible="false" Style="font-size:14px; font-weight:bold;"></asp:Label>
                                        <div class="clearfix"></div>

                                        <asp:GridView ID="gvinfla" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea" AutoGenerateColumns="False" Width="447px">

                                            <Columns>


                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvgrpdesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc"))%>'
                                                            Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText=" Budget">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvBgdam" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Purchase">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvpuram" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "puram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText=" Budget Remaining">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvrbgdam" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rbgdam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText=" Present Inflation ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvprincur" Target="_blank" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "princur")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                                 <asp:TemplateField HeaderText="Forcasted Inflation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvfuincur" Target="_blank" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fuincur")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Budget Required">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvreqbgd" Target="_blank" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqbgd")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle HorizontalAlign="Right" />
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



                                 <%--   <div class="col-md-6">
                                        <asp:Label ID="lblInflation" runat="server" CssClass=" inputlblVal"   Text="Inflation" Visible="false" Style="font-size:14px; font-weight:bold;"></asp:Label>
                                        <div class="clearfix"></div>
                                        <asp:GridView ID="gvinflabreak" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea" AutoGenerateColumns="False">

                                            <Columns>


                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvgrpdescibr" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc"))%>'
                                                            Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText=" Incurred">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvcostincur" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "costincur")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Savings">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvcostsave" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "costsave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Future Incured">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvfuinfla" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fuinfla")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Future Savings">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvfusave" Target="_blank" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fusave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle HorizontalAlign="Right" />
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
                                    </div>--%>


                                </div>


                                <div class="clearfix margin5px"></div>
                                 <asp:Label ID="lblinfladetails" runat="server" CssClass=" inputlblVal"   Text="Details" Visible="false" Style="font-size:14px; font-weight:bold;"></asp:Label>
                                <div class="table table-responsive">
                                    <asp:GridView ID="gvRCost" runat="server"
                                        AutoGenerateColumns="False"
                                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        OnRowDataBound="gvRCost_RowDataBound">
                                        <PagerSettings Position="Top" />
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl </br> (1)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Resource Description </br> (2)">
                                        <HeaderTemplate>
                                            <table style="width: 250px;">
                                                <tr>
                                                    <td class="style58">
                                                        <asp:Label ID="Label4" runat="server"
                                                            Text="Resource Description </br> (2)" Width="180px"></asp:Label>
                                                    </td>
                                                    <td class="style60">&nbsp;</td>
                                                    <td>
                                                        <asp:HyperLink ID="hlbtntbCdataExel" runat="server" 
                                                              CssClass="btn btn-danger btn-xs" ToolTip="Export Excel"><span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:label ID="lblgvresdesc" runat="server" 
                                                Font-Size="12px" Font-Underline="False" Target="_blank"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                Width="250px"></asp:label>
                                        </ItemTemplate>

                                              <FooterTemplate>

                                                    <asp:LinkButton ID="lbtnFinalUpdate" runat="server"
                                                        CssClass="btn btn-danger primaryBtn" OnClick="lbtnFinalUpdate_Click">Update</asp:LinkButton>

                                             </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="left" />
                                        <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    HorizontalAlign="Right" />
                                    </asp:TemplateField>



                                           <%-- <asp:TemplateField HeaderText="Resource Description </br>  (2)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvresdesc" runat="server"
                                                        Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc"))  %>'
                                                        Width="180px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>

                                                    <asp:LinkButton ID="lbtnFinalUpdate" runat="server"
                                                        CssClass="btn btn-danger primaryBtn" OnClick="lbtnFinalUpdate_Click">Update</asp:LinkButton>

                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Unit  </br>  (3)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvUnit0" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                        Width="25px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Budgeted Qty &nbsp&nbsp (4)">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlnkgvbgdqty" runat="server" Style="text-align: right" Target="_blank"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:HyperLink>
                                                </ItemTemplate>
                                                <FooterTemplate>

                                                    <asp:LinkButton ID="lbtntotal" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtntotal_Click">Total</asp:LinkButton>

                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Received Qty &nbsp (5)">
                                                <ItemTemplate>

                                                    <asp:HyperLink ID="hlnkgvrcvqty" runat="server" Style="text-align: right" Target="_blank"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:HyperLink>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bal. Qty &nbsp (6)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvbalqty" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Budgeted Rate  </br>  (7)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgbgdvrate" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Received Rate  </br>  (8)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgbgdrerate" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rerate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Last Pur. Rate &nbsp (9)">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvLRate" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "maxrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                        Style="text-align: right"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="New Pur. Rate &nbsp (10)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvacrate" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                        Style="text-align: right"></asp:TextBox>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Price Increased &nbsp 11=(8-7)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvInRate" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "incrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                        Style="text-align: right"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Present Inflation Loss &nbsp 12=(5*(8-7))">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvBdgPurIn" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "princ")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                        Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <table style="width: 7%; height: 25px;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lgvFbgdPuramt1" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="White" Style="text-align: right" Width="75px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Present Inflation Gain  &nbsp 13=(5*(8-7))">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvBdgPurDe" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pridesc")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                        Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <table style="width: 7%; height: 25px;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lgvFbgdPuramt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="White" Style="text-align: right" Width="75px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            




                                             <asp:TemplateField HeaderText="Forcasted Infaltion Loss  &nbsp (14)">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvfuincured" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fuinfla")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="85px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                        Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <table style="width: 7%; height: 25px;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lgvFfuincured" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="White" Style="text-align: right" Width="85px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Forcasted Infaltion Gain  &nbsp (15)">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvfusaving" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fusave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="85px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                        Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <table style="width: 7%; height: 25px;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lgvFfusaving" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="White" Style="text-align: right" Width="85px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>


                                             <asp:TemplateField HeaderText="Inflation  &nbsp 16=(12-13+14-15)">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvinflation" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "infla")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="85px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                        Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <table style="width: 7%; height: 25px;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lgvFinflation" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="White" Style="text-align: right" Width="85px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>






                                            <asp:TemplateField HeaderText=" Budget Required ">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvbalPur" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdbalqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="85px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                        Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <table style="width: 7%; height: 25px;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lgvFbgdBalamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="White" Style="text-align: right" Width="85px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>




                                            <asp:TemplateField HeaderText="Avg Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvBgdReq" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tbgdreq")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                        Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <table style="width: 7%; height: 25px;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lgvtBgdamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="White" Style="text-align: right" Width="75px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>


                                        </Columns>

                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>



                                     <div style="display: none;">


                                <asp:TextBox ID="txtbgdam" runat="server"></asp:TextBox>
                                <asp:TextBox ID="txtpuram" runat="server"></asp:TextBox>
                                <asp:TextBox ID="txtrbgdam" runat="server"></asp:TextBox>
                                <asp:TextBox ID="txtreqbgd" runat="server"></asp:TextBox>

                                <asp:TextBox ID="txtcostincur" runat="server"></asp:TextBox>
                                <asp:TextBox ID="txtcostsave" runat="server"></asp:TextBox>

                                <asp:TextBox ID="txtfuinfla" runat="server"></asp:TextBox>
                                <asp:TextBox ID="txtfusave" runat="server"></asp:TextBox>
                                <asp:TextBox ID="txtprincur" runat="server"></asp:TextBox>
                                <asp:TextBox ID="txtfuincur" runat="server"></asp:TextBox>
                                <asp:TextBox ID="txtinfla" runat="server"></asp:TextBox>




                            </div>
                                </div>
                                <asp:Panel ID="PanelNote" runat="server" Visible="False">
                                    <%-- <table class="table table-hover table-hover">
                                        <tr>
                                            <td class="style106">
                                                <asp:Label ID="Label1" runat="server" Text="Note:" Width="120px"></asp:Label>
                                            </td>
                                            <td class="style107">&nbsp;</td>
                                            <td class="style108">&nbsp;</td>
                                            <td class="style109">&nbsp;</td>
                                            <td class="style19">&nbsp;</td>
                                            <td class="style110">&nbsp;</td>
                                            <td class="style111">&nbsp;</td>
                                            <td class="style111">
                                                <asp:Label ID="Label14" runat="server"
                                                    Text="Note:" Width="120px"></asp:Label>
                                            </td>
                                            <td class="style112">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style106">
                                                <asp:Label ID="lblstatus" runat="server" Text="Description" Width="120px" CssClass="btn btn-warning primaryBtn"></asp:Label>
                                            </td>
                                            <td class="style107">
                                                <asp:Label ID="lblstatus0" runat="server" Text="Amount" CssClass="btn btn-warning primaryBtn" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style108">
                                                <asp:Label ID="lblstatus1" runat="server" CssClass="btn btn-warning primaryBtn" Text="Const. SFT" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style109">
                                                <asp:Label ID="lblstatus2" runat="server" Text="Cost/SFT" Width="80px" CssClass="btn btn-warning primaryBtn"></asp:Label>
                                            </td>
                                            <td class="style19">
                                                <asp:Label ID="lblstatus3" runat="server" Text="Sales SFT" Width="80px" CssClass="btn btn-warning primaryBtn"></asp:Label>
                                            </td>
                                            <td class="style110">
                                                <asp:Label ID="lblstatus4" runat="server" Text="Sales/SFT" Width="80px" CssClass="btn btn-warning primaryBtn"></asp:Label>
                                            </td>
                                            <td class="style111">
                                                <asp:Label ID="lblstatus5" runat="server" Text="In %" Width="80px" CssClass="btn btn-warning primaryBtn"></asp:Label>
                                            </td>
                                            <td class="style111">
                                                <asp:Label ID="lblstatus6" runat="server" Text="Description" Width="120px" CssClass="btn btn-warning primaryBtn"></asp:Label>
                                            </td>
                                            <td class="style112">
                                                <asp:Label ID="lblstatus7" runat="server" CssClass="btn btn-warning primaryBtn" Text="Amount" Width="80px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblRevCon0" runat="server"
                                                    Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style106">
                                                <asp:Label ID="lblOrgBgd" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#660033" Height="16px" Style="color: #000000"
                                                    Text="A. Orginal Budget" Width="120px"></asp:Label>
                                            </td>
                                            <td class="style107">
                                                <asp:Label ID="lblOrgBgdVal" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#660033" Height="16px" Style="color: #000000; text-align: right;"
                                                    Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style108">
                                                <asp:Label ID="lblOrgBgdCon" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#660033" Height="16px"
                                                    Style="color: #000000; text-align: right;" Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style109">
                                                <asp:Label ID="lblOrgBgdConSFT" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#660033" Height="16px"
                                                    Style="color: #000000; text-align: right;" Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style19">
                                                <asp:Label ID="lblOrgBgdSal" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#660033" Height="16px"
                                                    Style="color: #000000; text-align: right;" Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style110">
                                                <asp:Label ID="lblOrgBgdSFT" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#660033" Height="16px"
                                                    Style="color: #000000; text-align: right;" Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style111">
                                                <asp:Label ID="lblOrgBgdPr" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#660033" Height="16px"
                                                    Style="color: #000000; text-align: right;" Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style111">
                                                <asp:Label ID="lblOrgBgd0" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#660033" Height="16px" Style="color: #000000"
                                                    Text="A. Revised Budget" Width="120px"></asp:Label>
                                            </td>
                                            <td class="style112">
                                                <asp:Label ID="lblNAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#660033" Height="16px" Style="color: #000000; text-align: right;"
                                                    Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style106">
                                                <asp:Label ID="lblRevBgd" runat="server" Font-Bold="True"
                                                    Font-Size="14px" ForeColor="#660033" Height="16px" Style="color: #000000"
                                                    Text="B. Revised Budget" Width="150px"></asp:Label>
                                            </td>
                                            <td class="style107">
                                                <asp:Label ID="lblRevBgdHead" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#660033" Height="16px" Style="color: #000000; text-align: right;"
                                                    Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style108">&nbsp;</td>
                                            <td class="style109">&nbsp;</td>
                                            <td class="style19">&nbsp;</td>
                                            <td class="style110">&nbsp;</td>
                                            <td class="style111">&nbsp;</td>
                                            <td class="style111">
                                                <asp:Label ID="lblRevBgd0" runat="server" Font-Bold="True" Font-Size="14px"
                                                    ForeColor="#660033" Height="16px" Style="color: #000000"
                                                    Text="B. Actual Cost" Width="150px"></asp:Label>
                                            </td>
                                            <td class="style112">
                                                <asp:Label ID="lblNAmt0" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#660033" Height="16px" Style="color: #000000; text-align: right;"
                                                    Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style106">
                                                <asp:Label ID="lblRevBgdAm" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#660033" Height="16px" Style="color: #000000"
                                                    Text="&nbsp;&nbsp;Received As Per Budget" Width="150px"></asp:Label>
                                            </td>
                                            <td class="style107">
                                                <asp:Label ID="lblRevBgdAmVal" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#660033" Height="16px" Style="color: #000000; text-align: right;"
                                                    Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style108">&nbsp;</td>
                                            <td class="style109">&nbsp;</td>
                                            <td class="style19">&nbsp;</td>
                                            <td class="style110">&nbsp;</td>
                                            <td class="style111">&nbsp;</td>
                                            <td class="style111">
                                                <asp:Label ID="lblRevBgdAm0" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#660033" Height="16px" Style="color: #000000"
                                                    Text="&nbsp;&nbsp;Purchase" Width="150px"></asp:Label>
                                            </td>
                                            <td class="style112">
                                                <asp:Label ID="lblNAmt1" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#660033" Height="16px" Style="color: #000000; text-align: right;"
                                                    Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style106">
                                                <asp:Label ID="lblRevPriInc" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#660033" Height="16px" Style="color: #000000"
                                                    Text="&nbsp;&nbsp;Price Increased" Width="150px"></asp:Label>
                                            </td>
                                            <td class="style107">
                                                <asp:Label ID="lblRevPriIncVal" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#660033" Height="16px"
                                                    Style="color: #000000; text-align: right;" Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style108">&nbsp;</td>
                                            <td class="style109">&nbsp;</td>
                                            <td class="style19">&nbsp;</td>
                                            <td class="style110">&nbsp;</td>
                                            <td class="style111">&nbsp;</td>
                                            <td class="style111">
                                                <asp:Label ID="lblRevPriInc0" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#660033" Height="16px" Style="color: #000000"
                                                    Text="&nbsp;&nbsp;General Advance" Width="150px"></asp:Label>
                                            </td>
                                            <td class="style112">
                                                <asp:Label ID="lblNAmt2" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#660033" Height="16px" Style="color: #000000; text-align: right;"
                                                    Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style106">
                                                <asp:Label ID="lblRevPriDec" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#660033" Height="16px" Style="color: #000000"
                                                    Text="&nbsp;&nbsp;PriceDesrees" Width="120px"></asp:Label>
                                            </td>
                                            <td class="style107">
                                                <asp:Label ID="lblRevPriDecVal" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#660033" Height="16px"
                                                    Style="color: #000000; text-align: right;" Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style108">&nbsp;</td>
                                            <td class="style109">&nbsp;</td>
                                            <td class="style19">&nbsp;</td>
                                            <td class="style110">&nbsp;</td>
                                            <td class="style111">&nbsp;</td>
                                            <td class="style111">
                                                <asp:Label ID="lblRevPriDec0" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#660033" Height="16px" Style="color: #000000"
                                                    Text="&nbsp;&nbsp;Sub-Contractor Advance" Width="150px"></asp:Label>
                                            </td>
                                            <td class="style112">
                                                <asp:Label ID="lblNAmt3" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#660033" Height="16px" Style="color: #000000; text-align: right;"
                                                    Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style106">
                                                <asp:Label ID="lblRevRemPur" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#660033" Height="16px" Style="color: #000000"
                                                    Text="&nbsp;&nbsp;  Remaning Purchase" Width="150px"></asp:Label>
                                            </td>
                                            <td class="style107">
                                                <asp:Label ID="lblRevRemPurVal" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#660033" Height="16px"
                                                    Style="color: #000000; text-align: right;" Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style108">&nbsp;</td>
                                            <td class="style109">&nbsp;</td>
                                            <td class="style19">&nbsp;</td>
                                            <td class="style110">&nbsp;</td>
                                            <td class="style111">&nbsp;</td>
                                            <td class="style111">
                                                <asp:Label ID="lblRevRemPur0" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#660033" Height="16px" Style="color: #000000"
                                                    Text="&nbsp;&nbsp;Suplier Advance" Width="150px"></asp:Label>
                                            </td>
                                            <td class="style112">
                                                <asp:Label ID="lblNAmt4" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#660033" Height="16px" Style="color: #000000; text-align: right;"
                                                    Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style106">
                                                <asp:Label ID="lblRevTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#660033" Height="16px" Style="color: #000000"
                                                    Text="&nbsp;&nbsp;Total Amount" Width="150px"></asp:Label>
                                            </td>
                                            <td class="style107">
                                                <asp:Label ID="lblRevTotalVal" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#660033" Height="16px"
                                                    Style="color: #000000; text-align: right;" Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style108">
                                                <asp:Label ID="lblRevCon" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#660033" Height="16px"
                                                    Style="color: #000000; text-align: right;" Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style109">
                                                <asp:Label ID="lblRevConSFT" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#660033" Height="16px"
                                                    Style="color: #000000; text-align: right;" Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style19">
                                                <asp:Label ID="lblRevSal" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#660033" Height="16px"
                                                    Style="color: #000000; text-align: right;" Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style110">
                                                <asp:Label ID="lblRevSalSFT" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#660033" Height="16px"
                                                    Style="color: #000000; text-align: right;" Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style111">
                                                <asp:Label ID="lblRevInPr" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#660033" Height="16px"
                                                    Style="color: #000000; text-align: right;" Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style111">
                                                <asp:Label ID="lblRevTotal0" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#660033" Height="16px" Style="color: #000000"
                                                    Text="&nbsp;&nbsp;Total Actual Cost" Width="150px"></asp:Label>
                                            </td>
                                            <td class="style112">
                                                <asp:Label ID="lblNAmt5" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#660033" Height="16px" Style="color: #000000; text-align: right;"
                                                    Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style106">
                                                <asp:Label ID="lblInc" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#660033" Height="16px" Style="color: #000000"
                                                    Text="C. Increased" Width="150px"></asp:Label>
                                            </td>
                                            <td class="style107">
                                                <asp:Label ID="lblIncVal" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#660033" Height="16px"
                                                    Style="color: #000000; text-align: right;" Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style108">
                                                <asp:Label ID="lblInccon" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#660033" Height="16px"
                                                    Style="color: #000000; text-align: right;" Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style109">
                                                <asp:Label ID="lblIncconSFT" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#660033" Height="16px"
                                                    Style="color: #000000; text-align: right;" Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style19">
                                                <asp:Label ID="lblIncSal" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#660033" Height="16px"
                                                    Style="color: #000000; text-align: right;" Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style110">
                                                <asp:Label ID="lblIncSalSFT" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#660033" Height="16px"
                                                    Style="color: #000000; text-align: right;" Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style111">
                                                <asp:Label ID="lblIncPr" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#660033" Height="16px"
                                                    Style="color: #000000; text-align: right;" Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style111">
                                                <asp:Label ID="lblInc0" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#660033" Height="16px" Style="color: #000000;"
                                                    Text="&nbsp;&nbsp;Remaning Cost" Width="150px"></asp:Label>
                                            </td>
                                            <td class="style112">
                                                <asp:Label ID="lblNAmt6" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#660033" Height="16px" Style="color: #000000; text-align: right;"
                                                    Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style106">&nbsp;</td>
                                            <td class="style107">&nbsp;</td>
                                            <td class="style108">&nbsp;</td>
                                            <td class="style109">&nbsp;</td>
                                            <td class="style19">&nbsp;</td>
                                            <td class="style110">&nbsp;</td>
                                            <td class="style111">&nbsp;</td>
                                            <td class="style111">
                                                <asp:Label ID="lblInc1" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#660033" Height="16px" Style="color: #000000; font-size: 14px;" Text="C. Liabilities"
                                                    Width="150px"></asp:Label>
                                            </td>
                                            <td class="style112">
                                                <asp:Label ID="lblNAmt7" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#660033" Height="16px" Style="color: #000000; text-align: right;"
                                                    Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style106">&nbsp;</td>
                                            <td class="style107">&nbsp;</td>
                                            <td class="style108">&nbsp;</td>
                                            <td class="style109">&nbsp;</td>
                                            <td class="style19">&nbsp;</td>
                                            <td class="style110">&nbsp;</td>
                                            <td class="style111">&nbsp;</td>
                                            <td class="style111">
                                                <asp:Label ID="lblInc2" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#660033" Height="16px" Style="color: #000000;" Text=" &nbsp; &nbsp;General Liabilities"
                                                    Width="150px"></asp:Label>
                                            </td>
                                            <td class="style112">
                                                <asp:Label ID="lblNAmt8" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#660033" Height="16px" Style="color: #000000; text-align: right;"
                                                    Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style106">&nbsp;</td>
                                            <td class="style107">&nbsp;</td>
                                            <td class="style108">&nbsp;</td>
                                            <td class="style109">&nbsp;</td>
                                            <td class="style19">&nbsp;</td>
                                            <td class="style110">&nbsp;</td>
                                            <td class="style111">&nbsp;</td>
                                            <td class="style111">
                                                <asp:Label ID="lblInc3" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#660033" Height="16px" Style="color: #000000;" Text="&nbsp;&nbsp;Sub-Contractor"
                                                    Width="150px"></asp:Label>
                                            </td>
                                            <td class="style112">
                                                <asp:Label ID="lblNAmt9" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#660033" Height="16px" Style="color: #000000; text-align: right;"
                                                    Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style106">&nbsp;</td>
                                            <td class="style107">&nbsp;</td>
                                            <td class="style108">&nbsp;</td>
                                            <td class="style109">&nbsp;</td>
                                            <td class="style19">&nbsp;</td>
                                            <td class="style110">&nbsp;</td>
                                            <td class="style111">&nbsp;</td>
                                            <td class="style111">
                                                <asp:Label ID="lblInc4" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#660033" Height="16px" Style="color: #000000;" Text="&nbsp;&nbsp;Suplier"
                                                    Width="150px"></asp:Label>
                                            </td>
                                            <td class="style112">
                                                <asp:Label ID="lblNAmt10" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#660033" Height="16px" Style="color: #000000; text-align: right;"
                                                    Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style106">&nbsp;</td>
                                            <td class="style107">&nbsp;</td>
                                            <td class="style108">&nbsp;</td>
                                            <td class="style109">&nbsp;</td>
                                            <td class="style19">&nbsp;</td>
                                            <td class="style110">&nbsp;</td>
                                            <td class="style111">&nbsp;</td>
                                            <td class="style111">
                                                <asp:Label ID="lblInc5" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#660033" Height="16px" Style="color: #000000;"
                                                    Text="&nbsp;&nbsp;Total Liabilities" Width="150px"></asp:Label>
                                            </td>
                                            <td class="style112">
                                                <asp:Label ID="lblNAmt11" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#660033" Height="16px" Style="color: #000000; text-align: right;"
                                                    Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style106">&nbsp;</td>
                                            <td class="style107">&nbsp;</td>
                                            <td class="style108">&nbsp;</td>
                                            <td class="style109">&nbsp;</td>
                                            <td class="style19">&nbsp;</td>
                                            <td class="style110">&nbsp;</td>
                                            <td class="style111">&nbsp;</td>
                                            <td class="style111">
                                                <asp:Label ID="lblInc6" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#660033" Height="16px" Style="color: #000000;"
                                                    Text="&nbsp;&nbsp;Remaning Cost with Liabilities" Width="170px"></asp:Label>
                                            </td>
                                            <td class="style112">
                                                <asp:Label ID="lblNAmt12" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#660033" Height="16px" Style="color: #000000; text-align: right;"
                                                    Text="" Width="80px"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>--%>
                                </asp:Panel>


                            </asp:View>
                            <asp:View ID="ProjectGraphChart" runat="server">

                                <table class=" table table-hover table-bordered">
                                    <tr>
                                        <td colspan="2">
                                            <asp:Panel ID="pnlTarVsAchievement" runat="server" Visible="False">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td class="style100">&nbsp;</td>
                                                        <td class="style99">
                                                            <asp:Label ID="lbl01" runat="server" CssClass="style27" Font-Bold="True"
                                                                Font-Size="12px" Font-Underline="False" ForeColor="White" Height="16px"
                                                                Style="font-weight: 700; text-align: right" Text="Project Start Date:"
                                                                Width="110px"></asp:Label>
                                                        </td>
                                                        <td class="style44">
                                                            <asp:Label ID="lblStartDate" runat="server" CssClass="style27" Font-Bold="True"
                                                                Font-Size="12px" Font-Underline="False" ForeColor="White" Height="16px"
                                                                Style="font-weight: 700; text-align: left" Width="110px"></asp:Label>
                                                        </td>
                                                        <td class="style102">
                                                            <asp:Label ID="lbl2" runat="server" CssClass="style27" Font-Bold="True"
                                                                Font-Size="12px" Font-Underline="False" ForeColor="White" Height="16px"
                                                                Style="font-weight: 700; text-align: right" Text="Project End Date:"
                                                                Width="110px"></asp:Label>
                                                        </td>
                                                        <td class="style44">
                                                            <asp:Label ID="lblEndDate" runat="server" CssClass="style27" Font-Bold="True"
                                                                Font-Size="12px" Font-Underline="False" ForeColor="White" Height="16px"
                                                                Style="font-weight: 700; text-align: left" Width="110px"></asp:Label>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style100">&nbsp;</td>
                                                        <td class="style99">
                                                            <asp:Label ID="lbl3" runat="server" CssClass="style27" Font-Bold="True"
                                                                Font-Size="12px" Font-Underline="False" ForeColor="White" Height="16px"
                                                                Style="font-weight: 700; text-align: right" Text="Duration:" Width="110px"></asp:Label>
                                                        </td>
                                                        <td class="style44">
                                                            <asp:Label ID="lblDuration" runat="server" CssClass="style27" Font-Bold="True"
                                                                Font-Size="12px" Font-Underline="False" ForeColor="White" Height="16px"
                                                                Style="font-weight: 700; text-align: left" Width="110px"></asp:Label>
                                                        </td>
                                                        <td class="style101" colspan="2">
                                                            <asp:Label ID="lblProgressInPer" runat="server" CssClass="style27"
                                                                Font-Bold="True" Font-Size="12px" Font-Underline="False" ForeColor="White"
                                                                Height="16px" Style="font-weight: 700; text-align: left"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkGraph" runat="server" BackColor="Blue" BorderColor="White"
                                                                BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="#000" Text="Graph" />
                                                        </td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <asp:GridView ID="gvtarvsachivement" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" Style="margin-right: 0px" Width="323px">
                                                <PagerSettings Position="Top" />
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
                                                    <asp:TemplateField HeaderText="Duration">
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lbtnTotalTarVsAchievement" runat="server" Font-Bold="True"
                                                                Font-Size="12px" ForeColor="White" OnClick="lbtnTotalTarVsAchievement_Click">Total</asp:LinkButton>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvduration" runat="server" Height="16px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "monyear")) %>'
                                                                Width="55px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Target Amt.">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvtaramt" runat="server" BackColor="Transparent"
                                                                BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "taramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFtaramt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Style="text-align: right" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Actual Amt.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvacamt" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFacamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Style="text-align: right" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Completed (%)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvcompleted" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comper")).ToString("#,##0.00;(#,##0.00); ")+(Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comper"))>0?"%":"") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cumulative Target">
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lbtnUpdateTarVsAchievement" runat="server" Font-Bold="True"
                                                                Font-Size="12px" ForeColor="White" OnClick="lbtnUpdateTarVsAchievement_Click">Update</asp:LinkButton>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvcomtarget" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comtamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cumulative Actual">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvcomactual" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comacamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px"></asp:Label>
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
                                        </td>
                                        <td valign="top">
                                            <asp:Chart ID="Chart1" runat="server" Height="264px" Width="663px">
                                                <Series>
                                                    <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="blue"
                                                        MarkerColor="black" MarkerStyle="Circle" Name="Series1" MarkerSize="4">
                                                    </asp:Series>
                                                    <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="#ff3300"
                                                        MarkerColor="black" MarkerStyle="Circle" Name="Series2" MarkerSize="4">
                                                    </asp:Series>
                                                </Series>
                                                <ChartAreas>
                                                    <asp:ChartArea Name="ChartArea1">
                                                        <AxisX MaximumAutoSize="100" Interval="1">
                                                        </AxisX>
                                                    </asp:ChartArea>
                                                </ChartAreas>
                                                <Titles>
                                                    <asp:Title Font="Time New Romans, 16px" Name="Title1"
                                                        Text="Project Target Vs. Achievement">
                                                    </asp:Title>
                                                </Titles>
                                            </asp:Chart>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">&nbsp;</td>
                                    </tr>
                                </table>

                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

