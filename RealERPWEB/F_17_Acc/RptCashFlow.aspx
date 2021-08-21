<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptCashFlow.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptCashFlow" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
         $(document).ready(function () {
             Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
         });
         function pageLoaded() {
           
          
        }



         function ExecuteGraph(bgd) {



             var bgddata = JSON.parse(bgd);
           
             //var alldata = JSON.parse(alldata);
             //var payam = JSON.parse(payam);
             //console.log(bgd);
             console.log(bgddata);
            

             //Acc Legend
             
             var armainhead = [];
             for (var i = 0; i < bgddata.length; i++) {
                 armainhead[i] = bgddata[i]["head"];
             }

             Highcharts.chart('GraphCashFlow', {
                 chart: {
                     type: 'bar'
                 },
                 title: {
                     text: ''
                 },
                 subtitle: {
                     text: 'Trnsactions',
                     style: {
                         color: '#44994a',
                         fontWeight: 'bold'
                     }
                 },


                 xAxis: {
                     type: 'category',
                     labels:
                         {
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
                         "data":
                         (function () {
                             // generate an array of random data
                             var data = [],

                                 i;

                             for (var key in bgddata) {
                                 if (bgddata.hasOwnProperty(key)) {
                                     data.push([bgddata[key].head,
                                    bgddata[key].amount, false
                                     ]);
                                 }
                             }
                             return data;
                         }())
                     }
                 ]
             });

             //Highcharts.chart('GraphCashFlow', {
             //    chart: {
             //        plotBackgroundColor: null,
             //        plotBorderWidth: null,
             //        plotShadow: false,
             //        type: 'bar'
             //    },
             //    title: {
             //        text: 'Transactions'
             //    },
             //    tooltip: {                   
             //        headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
             //pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
             //    '<td style="padding:0"><b>{point.percentage:.2f}</b></td></tr>',
             //footerFormat: '</table>',
             //shared: true,
             //useHTML: true,
             //    },
             //    plotOptions: {
             //        pie: {
             //            allowPointSelect: true,
             //            cursor: 'pointer',
             //            dataLabels: {
             //                enabled: true,
             //                format: '<b>{point.name}</b>', //: {point.percentage:.2f}
             //                style: {
             //                    color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
             //                }
             //            }
             //        }
             //    },
             //    series: [{
             //        name: 'Transection',
             //        colorByPoint: true,                    
             //        allowPointSelect: true,
             //        keys: ['name', 'y', 'selected', 'sliced'],
             //        data: (function () {
             //            // generate an array of random data
             //            var data = [];
                         
             //            var amt;
             //            for (var key in bgddata) {
             //                if (bgddata.hasOwnProperty(key)) {                                 
             //                    data.push([bgddata[key].head+':'+'    '+ (bgddata[key].amount).toFixed(2),
             //                       Math.abs(bgddata[key].amount), false
             //                    ]);
             //                }
             //            }
             //            console.log(data);
             //            return data;
             //        }()),
             //        showInLegend: true
             //    }]
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

                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-12 pading5px">
                                        <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                        <asp:TextBox ID="txtDateFrom" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDateFrom" Enabled="true"></cc1:CalendarExtender>


                                        <asp:Label ID="lbltoDate" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                        <asp:TextBox ID="txtDateto" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" ToolTip="(dd-MM-yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateto_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDateto" Enabled="true"></cc1:CalendarExtender>
             

                                        <div class="colMdbtn">


                                            <asp:LinkButton ID="btnok" runat="server" CssClass="btn btn-primary okBtn" OnClick="btnok_Click">Ok</asp:LinkButton>
                                            

                                        </div>
                                       
                                          
                                       
                                    </div>

                                </div>

                                


                                

                            </div>
                        </fieldset>




                    </div>


                    <div class="row">
                           <div class="col-md-6" >
                    <asp:GridView ID="gvcashflow" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" ShowFooter="True" OnRowDataBound="gvcashflow_RowDataBound">
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

                                    <asp:TemplateField HeaderText="Head">
                                       
                                         <ItemTemplate>
                                        <asp:HyperLink ID="HLgvDesc" runat="server" Font-Underline="false" Target="_blank" ForeColor="blue"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Head")) %>'
                                            Width="70px"></asp:HyperLink>
                                    </ItemTemplate>

                                     <%--     <ItemTemplate>
                                        <asp:Label ID="lblgvResdesc" runat="server" Width="70px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Head")) %>'></asp:Label>                                

                                        </ItemTemplate>--%>
                                        
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />

                                    </asp:TemplateField>


                                    

                                   <%-- <asp:TemplateField HeaderText="Grp" Visible="false">
                                        

                                        <ItemTemplate>
                                        
                                         <asp:Label ID="lblgrp" runat="server"  Width="70px"
                                            Text='<%# Convert.ToSingle(DataBinder.Eval(Container.DataItem, "grp")) %>' ></asp:Label>
                                    </ItemTemplate>




                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>--%>


                                    <asp:TemplateField HeaderText="Amount">
                                        

                                        <ItemTemplate>
                                        
                                         <asp:Label ID="Label1" runat="server"  Width="70px"
                                           Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;(#,##0.00); ") %>' ></asp:Label>
                                    </ItemTemplate>




                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                        <asp:TemplateField HeaderText="%">                                       
                                            
                                           <ItemTemplate>
                                        
                                         <asp:Label ID="lblperamt" runat="server" Width="70px" 
                                           Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peramt")).ToString("#,##0.00;(#,##0.00); ") %>' ></asp:Label>
                                    </ItemTemplate>                                   

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

                    <div class="col-md-6" >                           
                             <div id="GraphCashFlow" style="width: 580px; height: 300px; margin: 0 auto"></div>
                        </div>
                        </div>
                </div>
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->



            


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

