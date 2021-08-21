<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptManProjectSum.aspx.cs" Inherits="RealERPWEB.F_32_Mis.RptManProjectSum" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .pading0px {
            padding: 0px !important;
        }
    </style>
    
   
    <script src="../Scripts/GoogleChart.js"></script>
     <script src="../Scripts/jsapi.js"></script>   
    <script src="../Scripts/uds_api_contents.js"></script>
  
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });



        function pageLoaded() {
          
            try
            {
                
                google.charts.load("current", { packages: ["corechart"] });
              
            
                GetToBudGraph1();
                GetToBudGraph2();
                GetToBudGraph3();
                GetToBudGraph4();
                GetToBudGraph5();
            
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

        function GetToBudGraph3() {
            google.charts.setOnLoadCallback(drawChart3());
        }

        function GetToBudGraph4() {
            google.charts.setOnLoadCallback(drawChart4());
        }
        function GetToBudGraph5() {
            google.charts.setOnLoadCallback(drawChart5());
        }

        function drawChart1()
        {
                var tbgdamt = parseFloat($('#<%=this.txttbgdamt.ClientID %>').val());
              var tactamt = parseFloat($('#<%=this.txttacamt.ClientID %>').val());            
                var data = google.visualization.arrayToDataTable([
                 ['Task', 'Present per Day'],
                  ['Budgeted', tbgdamt],
                  ['Actual', tactamt]

                ]);

                var options = {
                    title: "Total Budgeted Amount",
                    pieHole: 0.4,
                };

                
                var chart = new google.visualization.PieChart(document.getElementById('donutchart' + 1));
                chart.draw(data, options);
           }


        function drawChart2() {
            
            var perontw = parseFloat($('#<%=this.txtperontw.ClientID %>').val());
            var perontac = parseFloat($('#<%=this.txtperontac.ClientID %>').val());
     
            var data = google.visualization.arrayToDataTable([
             ['Task', 'Present per Day'],
              ['Budgeted %', perontw],
              ['Actual %', perontac]

            ]);

            var options = {
                title: "Construction Progress",
                pieHole: 0.4,
            };


            var chart = new google.visualization.PieChart(document.getElementById('donutchart' + 2));
            chart.draw(data, options);
        }


       

        function drawChart3() {
         
            var bgdamt = parseFloat($('#<%=this.txtbgdamt.ClientID %>').val());
            var acamt = parseFloat($('#<%=this.txtacamt.ClientID %>').val());

            var data = google.visualization.arrayToDataTable([
             ['Task', 'Present per Day'],
              ['Budgeted ', bgdamt],
              ['Actual', acamt]

            ]);

            var options = {
                title: "Construction Budget",
                pieHole: 0.4,
            };


            var chart = new google.visualization.PieChart(document.getElementById('donutchart' + 3));
            chart.draw(data, options);
        }


        function drawChart4() {
          
            var salbgd = parseFloat($('#<%=this.txtsalbgd.ClientID %>').val());
            var salac = parseFloat($('#<%=this.txtsalactual.ClientID %>').val());
           
            var data = google.visualization.arrayToDataTable([
             ['Task', 'Present per Day'],
              ['Budgeted', salbgd],
              ['Actual', salac]

            ]);

            var options = {
                title: "Sales",
                pieHole: 0.4,
            };


            var chart = new google.visualization.PieChart(document.getElementById('donutchart' + 4));
            chart.draw(data, options);
        }



        function drawChart5() {

            var collbgd = parseFloat($('#<%=this.txtcollbgd.ClientID %>').val());
            var collac = parseFloat($('#<%=this.txtcollac.ClientID %>').val());
            
            var data = google.visualization.arrayToDataTable([
             ['Task', 'Present per Day'],
              ['Budgeted', collbgd],
              ['Actual', collac]

            ]);

            var options = {
                title: "Collection",

               
                pieHole: 0.4,
            };


            var chart = new google.visualization.PieChart(document.getElementById('donutchart' + 5));
            chart.draw(data, options);
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
                                        <a href="<%=this.ResolveUrl("~/F_32_Mis/RptMisMasterBgd.aspx?Type=InvPlan")%>" Class="btn btn-sm primaryBtn pull-right btn-success"  target="_blank">Project Report</a>

                            <asp:LinkButton ID="lnkBtnReport" runat="server" CssClass="btn btn-sm primaryBtn pull-right btn-success" Visible="false" PostBackUrl="F_32_Mis/RptManProjectSum.aspx">Report 1</asp:LinkButton>
                         </div>
                     <div class="row">

                        <div id="donutchart1" class="col-sm-4 col-md-4 col-lg-4" style="height: 200px"></div>
                        <div id="donutchart2" class="col-sm-4 col-md-4 col-lg-4" style="height: 200px"></div>
                        <div id="donutchart3" class="col-sm-4 col-md-4 col-lg-4" style="height: 200px"></div>
                        <div id="donutchart4" class="col-sm-4 col-md-4 col-lg-4" style="height: 200px"></div>
                        <div id="donutchart5" class="col-sm-4 col-md-4 col-lg-4" style="height: 200px"></div>



                    </div>


                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class=" col-md-4  pading5px asitCol4">

                                        <asp:Label ID="lblfrmDate" CssClass="lblTxt lblName" runat="server" Text="As on Date"></asp:Label>
                                        <asp:TextBox ID="txtdate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtdate" TodaysDateFormat=""></cc1:CalendarExtender>

                                        <asp:LinkButton ID="lbtnShow" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnShow_Click" Width="44px">Show</asp:LinkButton>
                                    </div>
                                    <div class="col-md-2">

                                      
                                        
                                    </div>
                                    <div class="clearfix"></div>
                                </div>

                            </div>
                        </fieldset>



                        <div class="table-responsive">

                        <asp:GridView ID="gvprostatus" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            CssClass=" table-striped table-hover table-bordered grvContentarea" Width="785px" OnRowCreated="gvprostatus_RowCreated" OnRowDataBound="gvprostatus_RowDataBound">
                            <RowStyle Height="28px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="serialno" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="20px"></asp:Label>






                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actcode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Actcode" runat="server" Style="text-align: right" 
                                            
                                            
                                            
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                            Width="30px">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvactdesc" runat="server" Style="text-align: left" 
                                            Text='<%# "<B>"+"<span class=headertitle>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "catmdesc")) +"</span>"+ "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "pactdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "catmdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim(): "") 
                                                                         
                                                                    %>'

                                            
                                            
                                            Width="170px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Budgeted">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtfbgdamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                         <asp:Label ID="lgvfbgdamt" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>



                                 <asp:TemplateField HeaderText="Additional">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtadbgdamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                         <asp:Label ID="lgvfadbgdamt" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Total Budgeted">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtbgdamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvBudgetAmt" runat="server" Font-Size="11PX" Style="text-align: right" Target="_blank"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tbgdamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Acutal">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtacamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtacamt" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toacamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvtbgdsep" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Width="1px"></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="pading0px" />

                                    <HeaderStyle CssClass="pading0px" />
                                    <ItemStyle CssClass="pading0px" />
                                    <FooterStyle CssClass="pading0px" />
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText=" Budget %">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFperontw" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="50px"></asp:Label>
                                    </FooterTemplate>

                                    <ItemTemplate>
                                        <asp:HyperLink ID="hylbudgetp" runat="server" Font-Size="11PX" Style="text-align: right" Target="_blank"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perontw")).ToString("#,##0.00;(#,##0.00); ") %>'
                                             Width="50px"></asp:HyperLink>
                                    </ItemTemplate>





                                   <%-- <ItemTemplate>
                                        <asp:Label ID="lgvperontw" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perontw")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>--%>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText=" Actual %">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFperontac" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="50px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvperontac" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peronac")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                
                                   <asp:TemplateField HeaderText="% on Budget">
                                   
                                    
                                     <ItemTemplate>
                                        <asp:Label ID="lgvperonbudget" runat="server" Font-Size="11PX" Style="text-align: right" Target="_blank"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peronbgd")).ToString("#,##0.00;(#,##0.00); ") %>'
                                             Width="60px"></asp:Label>
                                    </ItemTemplate>
                                  
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvtbgpsep" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Width="1px"></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="pading0px" />

                                    <HeaderStyle CssClass="pading0px" />
                                    <ItemStyle CssClass="pading0px" />
                                    <FooterStyle CssClass="pading0px" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Budget">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFcbgdamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>

                                    <ItemTemplate>
                                        <asp:HyperLink ID="hylbudgetCost" runat="server" Font-Size="11PX" Style="text-align: right" Target="_blank"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mplanat")).ToString("#,##0;(#,##0); ") %>'
                                             Width="80px"></asp:HyperLink>
                                    </ItemTemplate>
                                    
                                  <%--   <ItemTemplate>
                                        <asp:Label ID="lgvcbgdamt" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mplanat")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>--%>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>



                                
                                <asp:TemplateField HeaderText="Actual With </br> Inflation ">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFacInfamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvacInfamt" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "texwprincur")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Actual ">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFcactamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvcactamt" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "examt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvinflasep" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Width="1px"></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="pading0px" />

                                    <HeaderStyle CssClass="pading0px" />
                                    <ItemStyle CssClass="pading0px" />
                                    <FooterStyle CssClass="pading0px" />
                                </asp:TemplateField>





<%--                                 <asp:TemplateField HeaderText="% on Budget">
                                   
                                    
                                     <ItemTemplate>
                                        <asp:Label ID="lgvperonbudget" runat="server" Font-Size="11PX" Style="text-align: right" Target="_blank"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peronbgd")).ToString("#,##0.00;(#,##0.00); ") %>'
                                             Width="60px"></asp:Label>
                                    </ItemTemplate>
                                  
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>--%>




                                <asp:TemplateField HeaderText="Present">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFprincur" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    
                                     <ItemTemplate>
                                        <asp:HyperLink ID="hlnkprincur" runat="server" Font-Size="11PX" Style="text-align: right" Target="_blank"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "princur")).ToString("#,##0;(#,##0); ") %>'
                                             Width="80px"></asp:HyperLink>
                                    </ItemTemplate>
                                  
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Forcasted">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFfuincur" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>

                                     <ItemTemplate>
                                        <asp:HyperLink ID="hlnkfuincur" runat="server" Font-Size="11PX" Style="text-align: right" Target="_blank"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fuincur")).ToString("#,##0;(#,##0); ") %>'
                                             Width="80px"></asp:HyperLink>
                                    </ItemTemplate>

                                   <%--<ItemTemplate>
                                        <asp:Label ID="lgvsalamt" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px">

                                        </asp:Label>
                                    </ItemTemplate>--%>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvtbgcsep" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Width="1px"></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="pading0px" />

                                    <HeaderStyle CssClass="pading0px" />
                                    <ItemStyle CssClass="pading0px" />
                                    <FooterStyle CssClass="pading0px" />
                                </asp:TemplateField>








                                <asp:TemplateField HeaderText="Budget">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtosalval" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    
                                     <ItemTemplate>
                                        <asp:HyperLink ID="hylbudgetSales" runat="server" Font-Size="11PX" Style="text-align: right" Target="_blank"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tosalval")).ToString("#,##0;(#,##0); ") %>'
                                             Width="80px"></asp:HyperLink>
                                    </ItemTemplate>
                                    
                                   <%-- <ItemTemplate>
                                        <asp:Label ID="lgvtosalval" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tosalval")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>--%>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Actual">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFsalamtl" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>

                                     <ItemTemplate>
                                        <asp:HyperLink ID="hylActualSales" runat="server" Font-Size="11PX" Style="text-align: right" Target="_blank"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salamt")).ToString("#,##0;(#,##0); ") %>'
                                             Width="80px"></asp:HyperLink>
                                    </ItemTemplate>

                                   <%--<ItemTemplate>
                                        <asp:Label ID="lgvsalamt" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px">

                                        </asp:Label>
                                    </ItemTemplate>--%>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Variance % on Budget">
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lgvvperonbudgets" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peronsal")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvtbgssep" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Width="1px"></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle CssClass="pading0px" />

                                    <HeaderStyle CssClass="pading0px" />
                                    <ItemStyle CssClass="pading0px" />
                                    <FooterStyle CssClass="pading0px" />
                                </asp:TemplateField>

                               


                                <asp:TemplateField HeaderText="Budget">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFcollbgd" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="75px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvcollbgd" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "collbgd")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Actual">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFcollamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="75px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvcollamt" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "collamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                                  <asp:TemplateField HeaderText="Variance % on Budget">
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lgvvperonbudget" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peroncoll")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
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

                        <div style="display:none;">

                            <asp:TextBox ID="txttbgdamt" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txttacamt" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtperontw" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtperontac" runat="server"></asp:TextBox>

                            <asp:TextBox ID="txtbgdamt" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtacamt" runat="server"></asp:TextBox>

                            <asp:TextBox ID="txtsalbgd" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtsalactual" runat="server"></asp:TextBox>

                            <asp:TextBox ID="txtcollbgd" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtcollac" runat="server"></asp:TextBox>


                        </div>

                    </div>

                   
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
