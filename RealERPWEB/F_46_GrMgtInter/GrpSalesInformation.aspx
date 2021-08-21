<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="GrpSalesInformation.aspx.cs" Inherits="RealERPWEB.F_46_GrMgtInter.GrpSalesInformation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });
            var gvDayWSale = $('#<%=this.gvDayWSale.ClientID%>');
            gvDayWSale.Scrollable();


        }
    </script>

    <style>
        .gvTopHeader tr:nth-child(1) {
            height: 14px !important;
            font-size: 12px !important;
            font-weight: bold !important;
        }
    </style>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lbldatefrm" runat="server" CssClass=" lblTxt lblName" Text="Date"></asp:Label>
                                        <asp:TextBox ID="txtDate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDate" Enabled="true"></cc1:CalendarExtender>
                                        .

                                      
                                    </div>
                                    <div class="col-md-1">
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>



                                        </div>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <div class="msgHandSt">


                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                                DisplayAfter="30">
                                                <ProgressTemplate>
                                                    <asp:Label ID="Label2" runat="server" CssClass="lblProgressBar"
                                                        Text="Please Wait.........."></asp:Label>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>

                                        </div>
                                    </div>
                                </div>



                            </div>
                        </fieldset>
                    </div>

                    <div class="row">
                        <div class="col-sm-3 col-md-3 col-lg-3">
                            <div class="col-sm-12 col-md-12 col-lg-12">
                                <asp:Label ID="lblYear" runat="server" class="GrpHeader" Visible="false" Width="267px">A. YEARLY SALES & COLLECTION</asp:Label>
                                <asp:GridView ID="grvYearlySales" runat="server" AutoGenerateColumns="False"
                                    Font-Size="10px" HorizontalAlign="Left" PageSize="5" ShowFooter="True"
                                    CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="grvYearlySales_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"
                                                    CssClass="gridtext"></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle Width="20px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="20px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Year">
                                            <ItemTemplate>
                                                <asp:Label ID="lblprodNmId" runat="server" Width="50px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "yearid")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Size="10px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sales">
                                            <ItemTemplate>
                                                <asp:Label ID="lblyAmt" runat="server" Width="80px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "samt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Size="10px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Collection">
                                            <ItemTemplate>
                                                <asp:Label ID="lblyCollamt" runat="server" Width="80px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "collamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Size="10px" HorizontalAlign="Center" />
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
                            <div class="col-sm-12 col-md-12 col-lg-12">

                                <asp:Chart ID="Chart2" runat="server" Width="250px" Height="150px">
                                    <Series>
                                        <asp:Series ChartArea="ChartArea1" ChartType="StackedColumn" Color="#2fd1f9"
                                            MarkerColor="black" MarkerStyle="None" Name="Series1" MarkerSize="4" >
                                        </asp:Series>
                                        <asp:Series ChartArea="ChartArea1" ChartType="StackedColumn" Color="Pink"
                                            MarkerColor="black" MarkerStyle="None" Name="Series2" MarkerSize="4">
                                        </asp:Series>
                                         <asp:Series ChartArea="ChartArea1" ChartType="StackedColumn" Color="Green"
                                            MarkerColor="black" MarkerStyle="None" Name="Series3" MarkerSize="4" >
                                        </asp:Series>
                                         <asp:Series ChartArea="ChartArea1" ChartType="StackedColumn" Color="Red"
                                            MarkerColor="black" MarkerStyle="None" Name="Series4" MarkerSize="4" >
                                        </asp:Series>
                                         <asp:Series ChartArea="ChartArea1" ChartType="StackedColumn" Color="Blue"
                                            MarkerColor="black" MarkerStyle="None" Name="Series5" MarkerSize="4" >
                                        </asp:Series>
                                    </Series>
                                    


                                    <ChartAreas>
                                        
                                        <asp:ChartArea Name="ChartArea1" >
                                            <AxisX MaximumAutoSize="100" Interval="1"  >
                                            </AxisX>
                                            <AxisY LineColor="YellowGreen" Title="Taka in Crore"   ></AxisY>
                                        </asp:ChartArea>
                                    </ChartAreas>
                                   
                                   
                                   
                                    <Titles>
                                        <asp:Title Name="Amount">
                                        </asp:Title>
                                    </Titles>
                                   
                                   
                                   
                                </asp:Chart>
                                

                            </div>
                        </div>
                        <div class="col-sm-9 col-md-9 col-lg-9">


                            <div class="row">



                                <div class="col-xs-3 col-md-3 col-lg-3">
                                    <asp:Label ID="lblWeek" runat="server" class="GrpHeader btn-block" Visible="false" Width="812px">D. WEEKLY SALES & COLLECTION</asp:Label>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:GridView ID="grvWeekSales" runat="server" AutoGenerateColumns="False"
                                        Font-Size="10px" HorizontalAlign="Left" PageSize="5" ShowFooter="True"
                                        CssClass="table-striped table-hover table-bordered grvContentarea gvTopHeader" OnRowDataBound="grvWeekSales_RowDataBound">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSwlNo1" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"
                                                        CssClass="gridtext"></asp:Label>
                                                </ItemTemplate>
                                                <ControlStyle Width="20px" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="20px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Days">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblprodNmId1" runat="server" Width="55px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wcode1")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lbl1">Week Total</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFT">-</asp:Label>
                                                    </p>

                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sales">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblyAmt1" runat="server" Width="55px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wsamt1")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFAmt1">-</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFAmt1T">-</asp:Label>
                                                    </p>

                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Collection">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblyCollamt1" runat="server" Width="55px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wcamt1")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFCollamt1">-</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFCollamt1T">-</asp:Label>
                                                    </p>

                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="10px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Days">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblprodNmId2" runat="server" Width="55px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wcode2")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lbl2">Week Total</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFT2">Sub-Total:</asp:Label>
                                                    </p>

                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sales">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblyAmt2" runat="server" Width="55px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wsamt2")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFAmt2">-</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFAmt2T">-</asp:Label>
                                                    </p>

                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Collection">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblyCollamt2" runat="server" Width="55px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wcamt2")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFCollamt2">-</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFCollamt2T">-</asp:Label>
                                                    </p>

                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="10px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Days">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblprodNmId3" runat="server" Width="55px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wcode3")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lbl3">Week Total</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFT3">Sub-Total:</asp:Label>
                                                    </p>

                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sales">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblyAmt3" runat="server" Width="55px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wsamt3")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFAmt3">-</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFAmt3T">-</asp:Label>
                                                    </p>

                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Font-Size="10px" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Collection">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblyCollamt3" runat="server" Width="55px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wcamt3")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFCollamt3">-</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFCollamt3T">-</asp:Label>
                                                    </p>

                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="10px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Days">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblprodNmId4" runat="server" Width="55px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wcode4")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lbl4">Week Total</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFT4">Gr Total:</asp:Label>
                                                    </p>

                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sales">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblyAmt4" runat="server" Width="55px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wsamt4")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFAmt4">-</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFAmt4T">-</asp:Label>
                                                    </p>

                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Font-Size="10px" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Collection">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblyCollamt4" runat="server" Width="55px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wcamt4")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFCollamt4">-</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFCollamt4T">-</asp:Label>
                                                    </p>

                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="10px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                            </asp:TemplateField>


                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                                <div class="col-xs-5 col-md-5 col-lg-5">
                                    
                                </div>
                                <div class="col-xs-1 col-md-1 col-lg-1">
                                    <a class="btn btn-primary primaryBtn"  href='<%=this.ResolveUrl("~/GenPage.aspx?Type=15")%>' target="_blank" style="margin: 10px 0 0 5px; line-height: 18px;">NEXT</a>
                                    
                                </div>
                            </div>



                        </div>



                    </div>

                    <hr class="hrline" />
                    <div class="row">
                        <div class="col-sm-3 col-md-3 col-lg-3">

                            <asp:Label ID="lblMon" runat="server" class="GrpHeader" Visible="false" Width="267px">B. MONTHLY SALES & COLLECTION</asp:Label>

                            <asp:GridView ID="grvMonthlySales" runat="server" AutoGenerateColumns="False"
                                Font-Size="10px" HorizontalAlign="Left" PageSize="5" ShowFooter="True"
                                CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="grvMonthlySales_RowDataBound">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Font-Bold="True"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"
                                                CssClass="gridtext"></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle Width="20px" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="20px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Month">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Width="50px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "yearmon")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>

                                            <asp:Label runat="server" ID="lblyTto">Total</asp:Label>

                                        </FooterTemplate>
                                        <HeaderStyle Font-Size="12px" />
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sales">
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Width="80px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlsalamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label runat="server" ID="lblyFAmt"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle Font-Size="12px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Collection">
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Width="80px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "collamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label runat="server" ID="lblyFCollamt"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle Font-Size="12px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                    </asp:TemplateField>


                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>
                        <div class="col-sm-9 col-md-9 col-lg-9">

                            <asp:Label ID="lblGrp" runat="server" class="GrpHeader" Visible="false" Width="267px">E. GRAPH</asp:Label>

                            <asp:Chart ID="Chart1" runat="server" Width="800px">
                                <Series>
                                    <asp:Series ChartArea="ChartArea1" ChartType="Column" Color="#2fd1f9"
                                        MarkerColor="black" MarkerStyle="None" Name="Series1" MarkerSize="4">
                                    </asp:Series>
                                    <asp:Series ChartArea="ChartArea1" ChartType="Column" Color="Pink"
                                        MarkerColor="black" MarkerStyle="None" Name="Series2" MarkerSize="4">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                        <AxisX MaximumAutoSize="100" Interval="1">
                                        </AxisX>
                                    </asp:ChartArea>
                                </ChartAreas>
                                <Legends>
                                    <asp:Legend></asp:Legend>
                                </Legends>
                            </asp:Chart>



                        </div>
                    </div>

                    <hr class="hrline" />
                    <div class="row">
                        <div class="col-xs-12">
                            <asp:Label ID="lblDetails" runat="server" class="GrpHeader" Visible="false" Width="267px">C. DAY WISE SALES DETAILS</asp:Label>


                            <div class=" table-responsive">
                                <asp:GridView ID="gvDayWSale" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Width="770px" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvDayWSale_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDPactdesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Customer Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDcuname" runat="server"
                                                    Text='<%#"<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))+"</b>"+"<br>"+
                                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "custadd")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description of Item">

                                            <ItemTemplate>
                                                <asp:Label ID="lgvDResDesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFditem" runat="server" Text="Total" Font-Bold="True" HorizontalAlign="Left" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right" Width="150px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">

                                            <ItemTemplate>
                                                <asp:Label ID="lgUnit" runat="server"
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "munit"))
                                                                         %>'
                                                    Width="35px"></asp:Label>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit Size">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUSize" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Price per SFT">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUpsft" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sftpr")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sales Team">

                                            <ItemTemplate>
                                                <asp:Label ID="lgDCper" runat="server"
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "conteam"))
                                                                         %>'
                                                    Width="120px"></asp:Label>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Budgeted Amt.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDTAmt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tuamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="75px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFDTAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sold Amt">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="HplgvSAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "suamt")).ToString("#,##0;(#,##0); ") %>' Target="_blank"></asp:HyperLink>


                                               <%-- <asp:Label ID="lgvDSAmt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "suamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="75px" Style="text-align: right"></asp:Label>--%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFDSAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Sold Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDSchdate" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "schdate")) %>'
                                                    Width="65px" Style="text-align: left"></asp:Label>
                                            </ItemTemplate>


                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Discount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDDisAmt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFDDisAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="%">
                                            <ItemTemplate>
                                                <asp:Label ID="lgDvDisPer" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disper")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
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

                        </div>
                    </div>


                    <hr class="hrline" />
                    <div class="row">
                        <div class="col-sm-12 col-md-12 col-lg-12">
                            <asp:Label ID="lblColl" runat="server" class="GrpHeader" Visible="false" Width="267px">C. DAY WISE COLLECTION DETAILS</asp:Label>
                            <div class=" table-responsive">
                                <asp:GridView ID="grvTrnDatWise" runat="server" AutoGenerateColumns="False"
                                    Font-Size="10px" HorizontalAlign="Left" ShowFooter="True" Width="1140px"
                                    CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="grvTrnDatWise_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="Label6" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Group Desc.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcGrpt" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MR No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcMRNo" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="MR Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcMRDat" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrdate1")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcProDesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit Desc">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUnDes" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Collection From">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCollFrm" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "collfrm")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Customer Name ">
                                            <FooterTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lgvFCDTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="Black" Style="text-align: right" Width="80px">Total:</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lgvFCDNetTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="Black" Style="text-align: right" Width="80px">Net Total:</asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCuName" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                                    Width="110px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Cash Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCaAmt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cashamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lgvFCashamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="Black" Style="text-align: right" Width="70px"> </asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cheque Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvChAmt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chqamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>


                                            <FooterTemplate>

                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lgvFChqamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lgvCDNetTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>


                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cheque No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvChNo" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bank Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBaNo" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cheque Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvChDat" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqdate")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="Reconciliation Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRecDat1" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recndt")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entry Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvEntrydate" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "entrydat")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="User Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUserName" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                                    Width="80px"></asp:Label>
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
                        </div>
                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

