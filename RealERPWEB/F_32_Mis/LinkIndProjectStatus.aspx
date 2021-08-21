<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkIndProjectStatus.aspx.cs" Inherits="RealERPWEB.F_32_Mis.LinkIndProjectStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">




    <script type="text/javascript">
      
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded()
        {
           <%-- var gv = $('#<%=this.dgvAccRec.ClientID %>');
            gv.Scrollable();--%>


          

        }
    </script>




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-12 pading5px">
                                        <asp:Label ID="Label21" runat="server" CssClass="lblTxt lblName">Project Name:</asp:Label>                                    
                                        <asp:Label ID="lblvalprojectname" runat="server" CssClass=" inputlblVal"></asp:Label>

                                        <asp:Label ID="lblstartdate" runat="server" CssClass="lblTxt lblName">Start Date:</asp:Label>                                    
                                        <asp:Label ID="lblvalstartdate" runat="server" CssClass=" inputlblVal" Style="width:80px;"></asp:Label>

                                         <asp:Label ID="lblconsarea" runat="server" CssClass="lblTxt lblName">Const. Area:</asp:Label>                                    
                                        <asp:Label ID="lblvalconsarea" runat="server" CssClass=" inputlblVal"  Style="width:80px;"></asp:Label>

                                          <asp:Label ID="lblstoried" runat="server" CssClass="lblTxt lblName">Storied:</asp:Label>                                    
                                        <asp:Label ID="lblvalstoried" runat="server" CssClass=" inputlblVal"  Style="width:80px;"></asp:Label>
                                       <asp:Label ID="lbltk" runat="server" CssClass="lblTxt lblName" Style="font-size:16px;">Taka in Crore </asp:Label>     
                                    
                                    </div>
                                 
                                   
                                </div>



                                 <div class="form-group">
                                    <div class="col-md-12 pading5px">
                                        <asp:Label ID="lbllandarea" runat="server" CssClass="lblTxt lblName">Land (Katha):</asp:Label>                                    
                                        <asp:Label ID="lblvallandarea" runat="server" CssClass=" inputlblVal"></asp:Label>

                                        <asp:Label ID="lblhandoverdate" runat="server" CssClass="lblTxt lblName">Handover Date:</asp:Label>                                    
                                        <asp:Label ID="lblvalhandoverdate" runat="server" CssClass=" inputlblVal"  Style="width:80px;"></asp:Label>

                                         <asp:Label ID="lblsalablearea" runat="server" CssClass="lblTxt lblName">Saleable Area:</asp:Label>                                    
                                        <asp:Label ID="lblvalsalablearea" runat="server" CssClass=" inputlblVal"  Style="width:80px;"></asp:Label>

                                          <asp:Label ID="lbllocation" runat="server" CssClass="lblTxt lblName">Location:</asp:Label>                                    
                                        <asp:Label ID="lblvallocation" runat="server" CssClass=" inputlblVal"  Style="width:250px;"></asp:Label>
                                    
                                    
                                    </div>
                                 
                                   
                                </div>

                            </div>


                           <div class="row">
                            <asp:Chart ID="Chart1" runat="server" Width="1139px" Height="320px" style="margin-left:-20px;" PaletteCustomColors="255, 255, 192; 128, 128, 255; Silver; Red; 255, 128, 255; 255, 128, 0; Yellow; Lime; Aqua; Blue; Fuchsia; 192, 0, 0; 192, 64, 0; 192, 192, 0" >
                            <Series>
                                <asp:Series ChartArea="ChartArea1" ChartType="Column" Color="BlueViolet"  IsVisibleInLegend="false"  
                                    MarkerColor="black" MarkerStyle="None" Name="Series1"  MarkerSize="4">
                                </asp:Series>
                                <asp:Series ChartArea="ChartArea1" ChartType="Column" Color="BlueViolet" 
                                    MarkerColor="black" MarkerStyle="None" Name="Series2" MarkerSize="4" IsVisibleInLegend="false">
                                </asp:Series> 
                                  <asp:Series ChartArea="ChartArea1" ChartType="Column" Color="BlueViolet" 
                                    MarkerColor="black" MarkerStyle="None" Name="Series3" MarkerSize="4" IsVisibleInLegend="false">

                                </asp:Series>
                                <%--"#9900cc--%>
                                 <asp:Series ChartArea="ChartArea1" ChartType="Column" Color="Gray"
                                    MarkerColor="black" MarkerStyle="None" Name="Series4" MarkerSize="4" IsVisibleInLegend="false">

                                </asp:Series>
                                <asp:Series ChartArea="ChartArea1" ChartType="Column" Color="Gray"
                                    MarkerColor="black" MarkerStyle="None" Name="Series5" MarkerSize="4" IsVisibleInLegend="false">

                                </asp:Series>
                                <asp:Series ChartArea="ChartArea1" ChartType="Column" Color="Gray"
                                    MarkerColor="black" MarkerStyle="None" Name="Series6" MarkerSize="4" IsVisibleInLegend="false">

                                </asp:Series>
                                <asp:Series ChartArea="ChartArea1" ChartType="Column" Color="Gray"
                                    MarkerColor="black" MarkerStyle="None" Name="Series7" MarkerSize="4" IsVisibleInLegend="false">

                                </asp:Series>
                                <asp:Series ChartArea="ChartArea1" ChartType="Column" Color="#993366"
                                    MarkerColor="black" MarkerStyle="None" Name="Series8" MarkerSize="4" IsVisibleInLegend="false">

                                </asp:Series>
                                <asp:Series ChartArea="ChartArea1" ChartType="Column" Color="#993366"
                                    MarkerColor="black" MarkerStyle="None" Name="Series9" MarkerSize="4" IsVisibleInLegend="false">

                                </asp:Series>
                                <asp:Series ChartArea="ChartArea1" ChartType="Column" Color="#993366"
                                    MarkerColor="black" MarkerStyle="None" Name="Series10" MarkerSize="4" IsVisibleInLegend="false">

                                </asp:Series>
                                <asp:Series ChartArea="ChartArea1" ChartType="Column" Color="#009999"
                                    MarkerColor="black" MarkerStyle="None" Name="Series11" MarkerSize="4" IsVisibleInLegend="false">

                                </asp:Series>
                                <asp:Series ChartArea="ChartArea1" ChartType="Column" Color="#009999"
                                    MarkerColor="black" MarkerStyle="None" Name="Series12" MarkerSize="4" IsVisibleInLegend="false">

                                </asp:Series>
                                <asp:Series ChartArea="ChartArea1" ChartType="Column" Color="#009999"
                                    MarkerColor="black" MarkerStyle="None" Name="Series13" MarkerSize="4" IsVisibleInLegend="false">

                                </asp:Series>
                                <asp:Series ChartArea="ChartArea1" ChartType="Column" Color="#009999"
                                    MarkerColor="black" MarkerStyle="None" Name="Series14" MarkerSize="4" IsVisibleInLegend="false">

                                </asp:Series>
                                 <asp:Series ChartArea="ChartArea1" ChartType="Column"  Color=""
                                    MarkerColor="black" MarkerStyle="None" Name="Series15" MarkerSize="4" IsVisibleInLegend="false">

                                </asp:Series>

                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                    <AxisX MaximumAutoSize="100"  Interval="1" LineColor="YellowGreen">
                                        <MajorGrid Enabled="false" LineColor="White" />
                                        <MinorGrid Enabled="false" LineColor="White" />
                                        
                                       
                                    </AxisX>
                      </asp:ChartArea>
                            </ChartAreas>

                            <Legends>
                                <asp:Legend Docking="Top" Alignment="Center" ></asp:Legend>
                            </Legends>
                                <BorderSkin BorderWidth="10" />
                        </asp:Chart>
                        </div>


                  
                            
                       <asp:GridView ID="gv01" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea" AutoGenerateColumns="False" OnRowDataBound="gv01_RowDataBound" Width="1022px" OnRowCreated="gv01_RowCreated">

                        <Columns>
                            

                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvgrpdesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc"))%>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                           
                            <asp:TemplateField HeaderText="Sales">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvBgdsales" Target="_blank" runat="server" Style="text-align: right" 
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdsales")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:HyperLink>
                                </ItemTemplate>

                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Cost">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvBgdCost" Target="_blank" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdcost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:HyperLink>
                                </ItemTemplate>

                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Margin(%)">
                                <ItemTemplate>
                                    <asp:Label ID="lgvBgdmargin" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdmar")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>

                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Completed">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvsalam"  Target="_blank" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:HyperLink>
                                </ItemTemplate>

                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Collection">
                                <ItemTemplate>
                                    <asp:Label ID="lgvcollam" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "collam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>

                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>


                             <asp:TemplateField HeaderText="Dues">
                                <ItemTemplate>
                                    <asp:Label ID="lgvdues" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cdueam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>

                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Over Dues">
                                <ItemTemplate>
                                    <asp:Label ID="lgvpdues" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pdueam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>

                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Budget">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvconsbgd"  Target="_blank"  runat ="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cbgdcost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:HyperLink>
                                </ItemTemplate>

                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Progress">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvconspro" Target="_blank" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cprogress")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:HyperLink>
                                </ItemTemplate>

                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Delay">
                                <ItemTemplate>
                                    <asp:Label ID="lgvconsdelay" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cdelay")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>

                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Actual Cost">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvinvamt" Target="_blank" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "invamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:HyperLink>
                                </ItemTemplate>

                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Liabilities">
                                <ItemTemplate>
                                    <asp:Label ID="lgvliaam" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "liaam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>

                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="R. Inflow">
                                <ItemTemplate>
                                    <asp:Label ID="lgvrinflow" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rinflow")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>

                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            
                             <asp:TemplateField HeaderText="R. Outflow">
                                <ItemTemplate>
                                    <asp:Label ID="lgvroutflow" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "routflow")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>

                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>



                        
                              <asp:TemplateField HeaderText="Block/Generated">
                                <ItemTemplate>
                                    <asp:Label ID="lgvfblock" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fblock")).ToString("#,##0.00;(#,##0.00); ") %>'
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
              


                   

                   

            


                   


                   

                </div>
            </div>




            
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

