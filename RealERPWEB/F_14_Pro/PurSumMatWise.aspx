<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="PurSumMatWise.aspx.cs" Inherits="RealERPWEB.F_14_Pro.PurSumMatWise" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Scripts/highchart2.js"></script>




    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            var comcod = <%=this.GetComeCode()%>;
            var date1 = $('#<%=this.txtFDate.ClientID%>').val();
            var date2 = $('#<%=this.txttodate.ClientID%>').val();
            //alert(date1);

            //$("#hlnkprjgrpcode").attr("href", "RptPurchaseStatus.aspx?Type=Purchase&Rpt=PurSum&comcod="+comcod+"&Date1="+date1+"&Date2="+date2+"");
              //$("#hlnkprjgrpcode").attr("href", "RptPurchaseStatus.aspx?Type=Purchase&Rpt=PurSum&comcod="+comcod"&Date1="+$('#<%=this.txtFDate.ClientID%>').val()+"&Date2="+$('#<%=this.txttodate.ClientID%>').val());
            $('#ddlReport').on('change', function () {
                //var ddlvalue = $(this).value;
                var selectedValue = $("#ddlReport").val();
                //alert(selectedValue);
                switch (selectedValue) {
                    case "1":
                        window.open("RptPurchaseStatus.aspx?Type=Purchase&Rpt=PurSum&comcod=" + comcod + "&Date1=" + date1 + "&Date2=" + date2 + "");
                        break;
                    default:

                }
            });




            //$("#hlnkdaypur").attr("href", "F_14_Pro/RptPurchaseStatus.aspx?Type=Purchase&comcod='"+comcod+"'&Rpt=DaywPur");
            //$("#hlnkdaypur").attr("href", "RptPurchaseStatus.aspx?Type=Purchase&Rpt=DaywPur&comcod="+comcod);
        }



        //$('#ddlReport').change(function() {
        //    var selectedVal = $('#ddlReport option:selected').attr('value');
        //    alert(selectedValue);
        //});

        <%--   $("#ddlReport").change(function () {
               
               var selectedValue = $("#ddlReport").val();
                alert(selectedValue);
                switch (selectedValue) {
                    case 0:
                        window.open('RptPurchaseStatus.aspx?Type=Purchase&Rpt=PurSum&comcod='+comcod'&Date1='+$('#<%=this.txtFDate.ClientID%>').val()+'&Date2='+$('#<%=this.txttodate.ClientID%>').val()'');
                        break;
                    default:
        
                }
                //alert("Selected Text: " + selectedText + " Value: " + selectedValue);
            });--%>


        function ExecuteGraph(bgd, alldata, mainhead) {


            var alldata = bgd;
            console.log(alldata);
            var bgddata = JSON.parse(bgd);
            var mainhead = JSON.parse(mainhead);


            Highcharts.setOptions({
                lang: {
                    decimalPoint: '.',
                    thousandsSep: ' '
                }
            });

            var armainhead = [];
            for (var i = 0; i < mainhead.length; i++) {
                armainhead[i] = mainhead[i]["rptdesc"];
            }

            $('#pursummary').highcharts({
                chart: {
                    type: 'bar'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Purchase Summary (MaterialWise)',
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
                        "data":
                            (function () {
                                // generate an array of random data
                                var data = [],

                                    i;

                                for (var key in bgddata) {
                                    if (bgddata.hasOwnProperty(key)) {
                                        data.push([bgddata[key].rptdesc,
                                        bgddata[key].amt, false
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

                                    <div class="col-md-3">
                                        <div runat="server" id="datepart">
                                            <asp:Label ID="lbldatefrm" runat="server" CssClass="smLbl_to" Text="Date"></asp:Label>
                                            <asp:TextBox ID="txtFDate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtFDate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtFDate" Enabled="true"></cc1:CalendarExtender>


                                            <asp:Label ID="lbldateto" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                            <asp:TextBox ID="txttodate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" ToolTip="(dd-MM-yyyy)"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txttodate" Enabled="true"></cc1:CalendarExtender>

                                        </div>

                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlprojname" Width="200px" runat="server" CssClass="chzn-select inputTxt">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_OnClick">Ok</asp:LinkButton>

                                    </div>
                                    <div class="pull-right col-md-4" runat="server" id="panellnk">
                                        <asp:Label ID="Label1" runat="server" CssClass="smLbl_to" Text="Report Type"></asp:Label>
                                        <asp:DropDownList ID="ddlReport" runat="server" Width="156px" CssClass="ddlPage" ClientIDMode="Static">
                                            <asp:ListItem Value="0">Select Report</asp:ListItem>
                                            <asp:ListItem Value="1">Purchase Summary (Project Wise)</asp:ListItem>
                                            <asp:ListItem Value="2" Selected="True">Summary</asp:ListItem>
                                            <asp:ListItem Value="3">Details</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--  AutoPostBack="true" OnSelectedIndexChanged="ddlReport_SelectedIndexChanged" --%>

                                        <%--<asp:HyperLink ID="hlnkprjgrpcode" runat="server" Target="_blank" ClientIDMode="Static" CssClass="btn btn-outline-light">Purchase Summary (Project Wise)</asp:HyperLink>--%>

                                        <%--<asp:HyperLink ID="hlnkdaypur" runat="server"  Target="_blank" CssClass="btn btn-outline-light"  ClientIDMode="Static">Day Wise Purchase</asp:HyperLink>--%>
                                    </div>





                                </div>

                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="col-md-12">
                                <asp:GridView ID="gvPurmatwise" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" ShowFooter="True" Width="475px"
                                    OnRowDataBound="gvPurmatwise_OnRowDataBound">
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

                                        <asp:TemplateField HeaderText=" ">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Description" Width="110px" ></asp:Label>
                                                 <asp:HyperLink ID="hlbtngvPurmatwiseExcel" runat="server" CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel "></i>
                                                </asp:HyperLink>
                                            </HeaderTemplate>
                                           

                                            <ItemTemplate>

                                                <asp:LinkButton ID="lnkgvWDescgp" runat="server" ForeColor="Black" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc")) 
                                                                           %>'
                                                    Width="165px" OnClick="lnkgvWDescgp_Click"></asp:LinkButton>



                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <FooterTemplate>
                                                <asp:Label ID="lgvtotal" runat="server" Style="text-align: right"
                                                    Text='Total'></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvunit" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit"))%>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>


                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QTY">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>


                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrate" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>


                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBudgetAmtgp" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:HyperLink ID="lgvFtotal" Target="_blank" runat="server" Style="text-align: right"></asp:HyperLink>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Percent(%)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpercnt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtotalper" runat="server" Style="text-align: right" Text="100%"></asp:Label>
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
                                <asp:HyperLink CssClass="col-md-4 col-md-offset-4 btn btn-sm btn-success" Style="float: left" Font-Size="13" NavigateUrl="~/F_32_Mis/PrjDirectCost.aspx" Target="_blank" runat="server">ABP Amount</asp:HyperLink>
                                <asp:Label runat="server" ID="abpamt" Style="color: maroon; margin-left: 10px; margin-top: 10px; font-weight: bold; float: left" Font-Size="13"></asp:Label>

                            </div>
                        </div>
                        <div class="col-md-6">

                            <div id="pursummary" style="width: 500px; height: 500px; margin: 0 auto"></div>
                        </div>
                    </div>


                </div>
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->




        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>

