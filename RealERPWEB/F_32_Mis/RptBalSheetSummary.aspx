<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptBalSheetSummary.aspx.cs" Inherits="RealERPWEB.F_32_Mis.RptBalSheetSummary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


      <script src="../Scripts/GoogleChart.js"></script>
     <script src="../Scripts/jsapi.js"></script>   
    <script src="../Scripts/uds_api_contents.js"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            try {
                google.charts.load("current", { packages: ["corechart"] });


                GetToBudGraph1();
                GetToBudGraph2();

            }

            catch (e)
            {
                alert("Error: " + e);

            }

          


        }



        function GetToBudGraph1() {



            google.charts.setOnLoadCallback(drawChart1());
        }
        function GetToBudGraph2() {
            google.charts.setOnLoadCallback(drawChart2());
        }


        function drawChart1() {
            var noncuram = parseFloat($('#<%=this.txtnoncuram.ClientID %>').val());
            var curam = parseFloat($('#<%=this.txtcuram.ClientID %>').val());
          
            var data = google.visualization.arrayToDataTable([
             ['Task', 'Present per Day'],
              ['NON-CURRENT ASSETS', noncuram],
              ['CURRENT ASSETS', curam]

            ]);

            var options = {
                title: "ASSESTS",
                pieHole: 0.4,
            };


            var chart = new google.visualization.PieChart(document.getElementById('donutchart' + 1));
            chart.draw(data, options);
        }


        function drawChart2() {

            var equityam = parseFloat($('#<%=this.txtequityam.ClientID %>').val());
            var noncurlia = parseFloat($('#<%=this.txtnoncurlia.ClientID %>').val());
            var curlia = parseFloat($('#<%=this.txtcurlia.ClientID %>').val());

            var data = google.visualization.arrayToDataTable([
             ['Task', 'Present per Day'],
              ['Equity', equityam],
              ['NON-CURRENT LIABILITIES', noncurlia],
              ['CURRENT LIABILITIES', curlia]

            ]);

            var options = {
                title: "LIABILITIES",
                pieHole: 0.4,
            };


            var chart = new google.visualization.PieChart(document.getElementById('donutchart' + 2));
            chart.draw(data, options);
        }


    </script>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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

            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <%--  <asp:Label ID="Label2" runat="server" CssClass="lblProgressBar"
                                                                    Text="Please Wait.........."></asp:Label>--%>

                        <%--  <div class="loader"></div> --%>
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
                                    <div class="col-md-2 pading5px">
                                        <asp:Label ID="lblDatefrom" runat="server" CssClass="lblTxt lblName">As on Date</asp:Label>
                                        <asp:TextBox ID="txtDatefrom" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="10"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDatefrom_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDatefrom" TodaysDateFormat=""></cc1:CalendarExtender>

                                    </div>
                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                    </div>

                                    <div class="col-md-2 pading5px">
                                        <a href="<%=this.ResolveUrl("~/F_17_Acc/AccFincStatmnt.aspx?Type=Report&comcod=")%>" class="btn btn-sm primaryBtn pull-right btn-success" target="_blank">Show Details</a>


                                    </div>
                                </div>


                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                        <asp:Chart ID="Chart1" runat="server" Width="368px" CssClass="" Height="320px" Style="margin-left: -17px;" PaletteCustomColors="255, 255, 192; 128, 128, 255; Silver; Red; 255, 128, 255; 255, 128, 0; Yellow; Lime; Aqua; Blue; Fuchsia; 192, 0, 0; 192, 64, 0; 192, 192, 0">
                            <Series>

                                <asp:Series ChartArea="ChartArea1" Color="green" IsVisibleInLegend="False" ToolTip="NON-CURRENT ASSETS"
                                    MarkerColor="Black" Name="Series1" MarkerSize="4" Legend="Legend1">
                                </asp:Series>
                                <asp:Series ChartArea="ChartArea1" Color="green" ToolTip="CURRENT ASSETS"
                                    MarkerColor="Black" Name="Series2" MarkerSize="4" IsVisibleInLegend="False" Legend="Legend1">
                                </asp:Series>
                                <asp:Series ChartArea="ChartArea1" Color="#993366" ToolTip="Equity"
                                    MarkerColor="Black" Name="Series3" MarkerSize="4" IsVisibleInLegend="False" Legend="Legend1">
                                </asp:Series>
                                <asp:Series ChartArea="ChartArea1" Color="#993366" ToolTip="NON-CURRENT LIABILITIES"
                                    MarkerColor="Black" Name="Series4" MarkerSize="4" IsVisibleInLegend="False" Legend="Legend1">
                                </asp:Series>
                                <asp:Series ChartArea="ChartArea1" Color="#993366" ToolTip="CURRENT LIABILITIES"
                                    MarkerColor="Black" Name="Series5" MarkerSize="4" IsVisibleInLegend="False" Legend="Legend1">
                                </asp:Series>





                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                    <AxisX MaximumAutoSize="100" Interval="1" LineColor="DarkBlue">
                                        <MajorGrid Enabled="False" LineColor="White" />
                                        <MinorGrid LineColor="White" />


                                    </AxisX>


                                </asp:ChartArea>
                            </ChartAreas>

                            <Legends>
                                <asp:Legend Docking="Top" Alignment="Center" Name="Legend1"></asp:Legend>
                            </Legends>
                            <BorderSkin BorderWidth="10" />

                        </asp:Chart>

                            

                            </div>
                        <div class="col-md-8">
                            
                            

                        <div id="donutchart1" class="col-sm-6 col-md-6 col-lg-6" style="height: 200px"></div>
                        <div id="donutchart2" class="col-sm-6 col-md-6 col-lg-6" style="height: 200px"></div>
                       



                  

                            </div>

                    </div>


                    <asp:GridView ID="gvIncomeSt" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        OnRowDataBound="gvIncomeSt_RowDataBound" OnRowCreated="gvIncomeSt_RowCreated" ShowFooter="True">

                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>

                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="NON-CURRENT ASSETS">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvnoncuram" runat="server" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "noncuram")).ToString("#,##0.00;(#,##.0.00); ") %>' Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="Smaller" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CURRENT ASSETS">
                                <ItemTemplate>
                                    <asp:Label ID="lgvcuram" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="Smaller" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Equity">
                                <ItemTemplate>
                                    <asp:Label ID="lgvequityam" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "equityam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="Smaller" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NON-CURRENT LIABILITIES">
                                <ItemTemplate>
                                    <asp:Label ID="lgvnoncurlia" runat="server" Style="text-align: right;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "noncurlia")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="Smaller" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CURRENT LIABILITIES">
                                <ItemTemplate>
                                    <asp:Label ID="lgvcurlia" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curlia")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="Smaller" />
                            </asp:TemplateField>

                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <HeaderStyle CssClass="grvHeader" />

                    </asp:GridView>


                        <div class="row" style="display:none;">

                            <asp:TextBox ID="txtnoncuram" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtcuram" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtequityam" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtnoncurlia" runat="server"></asp:TextBox>

                            <asp:TextBox ID="txtcurlia" runat="server"></asp:TextBox>
                            


                        </div>
                </div>

               
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
