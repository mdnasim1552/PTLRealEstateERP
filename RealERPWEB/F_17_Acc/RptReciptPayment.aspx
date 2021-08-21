<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptReciptPayment.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptReciptPayment" %>
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
             for (var i = 0; i < mainhead.length; i++)
             {
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
                     text: 'Take In Lac',
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
                                       
                                    </div>

                                </div>

                                


                                

                            </div>
                        </fieldset>




                    </div>


                    <div class="row">
                           <div class="col-md-6" >
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

                                            <asp:LinkButton ID="lnkgvWDescgp" runat="server" ForeColor="Black" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) 
                                                                           %>'
                                                Width="165px" OnClick="lnkgvWDescgp_Click"></asp:LinkButton>



                                        </ItemTemplate>
                                        <FooterTemplate>
                                                <asp:Label ID="lgvFDesc" runat="server" Font-Bold="True" Font-Size="12px" 
                                                     ForeColor="#800000"> Total :</asp:Label>
                                            </FooterTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Payment">
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


                                        <asp:TemplateField HeaderText="Payment %">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpercent" runat="server" Style="text-align: right" 
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                       <FooterTemplate>
                                                <asp:Label ID="lgvFpertoal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="60px" ForeColor="#800000"></asp:Label>
                                            </FooterTemplate>

                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                   <%-- <asp:TemplateField HeaderText="Budget  as of Today">
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
                                    </asp:TemplateField>--%>

                                </Columns>

                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />

                            </asp:GridView>
                            </div>

                    <div class="col-md-6" >
                           
                             <div id="containerGraph" style="width: 580px; height: auto; margin: 0 auto"></div>
                        </div>
                        </div>
                </div>
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->



            


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



