<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="GrpConstructionInfo.aspx.cs" Inherits="RealERPWEB.F_46_GrMgtInter.GrpConstructionInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });
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
                                <asp:Label ID="lblYear" runat="server" class="GrpHeader" Visible="false" Width="267px">A. YEARLY TARGET & EXECUTION</asp:Label>
                                <asp:GridView ID="grvYearlyCon" runat="server" AutoGenerateColumns="False"
                                    Font-Size="10px" HorizontalAlign="Left" PageSize="5" ShowFooter="True"
                                    CssClass="table-striped table-hover table-bordered grvContentarea">
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
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ymonth")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Size="10px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Target">
                                            <ItemTemplate>
                                                <asp:Label ID="lblyAmt" runat="server" Width="80px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "taramt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Size="10px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Execution">
                                            <ItemTemplate>
                                                <asp:Label ID="lblyPayAmt" runat="server" Width="80px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "examt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Size="10px" />
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
                                            MarkerColor="black" MarkerStyle="None" Name="Series1" MarkerSize="4">
                                        </asp:Series>
                                        <asp:Series ChartArea="ChartArea1" ChartType="StackedColumn" Color="Pink"
                                            MarkerColor="black" MarkerStyle="None" Name="Series2" MarkerSize="4">
                                        </asp:Series>
                                        <asp:Series ChartArea="ChartArea1" ChartType="StackedColumn" Color="Green"
                                            MarkerColor="black" MarkerStyle="None" Name="Series3" MarkerSize="4">
                                        </asp:Series>
                                        <asp:Series ChartArea="ChartArea1" ChartType="StackedColumn" Color="Red"
                                            MarkerColor="black" MarkerStyle="None" Name="Series4" MarkerSize="4">
                                        </asp:Series>
                                        <asp:Series ChartArea="ChartArea1" ChartType="StackedColumn" Color="Blue"
                                            MarkerColor="black" MarkerStyle="None" Name="Series5" MarkerSize="4">
                                        </asp:Series>
                                    </Series>



                                    <ChartAreas>

                                        <asp:ChartArea Name="ChartArea1">
                                            <AxisX MaximumAutoSize="100" Interval="1">
                                            </AxisX>
                                            <AxisY LineColor="YellowGreen" Title="Taka in Crore"></AxisY>
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
                                    <asp:Label ID="lblWeek" runat="server" class="GrpHeader btn-block" Visible="false" Width="798px">C. WEEKLY TAGET & EXECUTION</asp:Label>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:GridView ID="grvWeekCon" runat="server" AutoGenerateColumns="False"
                                        Font-Size="10px" HorizontalAlign="Left" PageSize="5" ShowFooter="True"
                                        CssClass="table-striped table-hover table-bordered grvContentarea gvTopHeader" OnRowDataBound="grvWeekCon_RowDataBound">
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
                                            <asp:TemplateField HeaderText="DAYS">
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

                                            <asp:TemplateField HeaderText="Target">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblyAmt1" runat="server" Width="55px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wtaramt1")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
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
                                            <asp:TemplateField HeaderText="Execution">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblyPayAmt1" runat="server" Width="55px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wexamt1")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFPatAmt1">-</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFPatAmt1T">-</asp:Label>
                                                    </p>

                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="DAYS">
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

                                            <asp:TemplateField HeaderText="Target">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblyAmt2" runat="server" Width="55px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wtaramt2")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
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
                                            <asp:TemplateField HeaderText="Execution">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblyPayAmt2" runat="server" Width="55px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wexamt2")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFPatAmt2">-</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFPatAmt2T">-</asp:Label>
                                                    </p>

                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAYS">
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

                                            <asp:TemplateField HeaderText="Target">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblyAmt3" runat="server" Width="55px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wtaramt3")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
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
                                            <asp:TemplateField HeaderText="Execution">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblyPayAmt3" runat="server" Width="55px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wexamt3")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFPatAmt3">-</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFPatAmt3T">-</asp:Label>
                                                    </p>

                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DAYS">
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

                                            <asp:TemplateField HeaderText="Target">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblyAmt4" runat="server" Width="55px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wtaramt4")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
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

                                            <asp:TemplateField HeaderText="Execution">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblyPayAmt4" runat="server" Width="55px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wexamt4")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFPatAmt4">-</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFPatAmt4T">-</asp:Label>
                                                    </p>

                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="10px" />
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
                                    <a class="btn btn-primary primaryBtn"  href='<%=this.ResolveUrl("~/GenPage.aspx?Type=07")%>' target="_blank" style="margin: 10px 0 0 5px; line-height: 18px;">NEXT</a>
                                    
                                </div>
                            </div>



                        </div>



                    </div>
                </div>
                <hr class="hrline" />
                <div class="row">
                    <div class="col-sm-3 col-md-3 col-lg-3">

                        <asp:Label ID="lblMon" runat="server" class="GrpHeader" Visible="false" Width="267px">B. MONTHLY TAGET & EXECUTION</asp:Label>

                        <asp:GridView ID="grvMonthlyCon" runat="server" AutoGenerateColumns="False"
                            Font-Size="10px" HorizontalAlign="Left" PageSize="5" ShowFooter="True"
                            CssClass="table-striped table-hover table-bordered grvContentarea">
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
                                <asp:TemplateField HeaderText="Month">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprodNmId" runat="server" Width="50px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "yearmon")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>

                                        <asp:Label runat="server" ID="lblyTto">Total</asp:Label>

                                    </FooterTemplate>
                                    <HeaderStyle Font-Size="12px" />
                                    <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Target">
                                    <ItemTemplate>
                                        <asp:Label ID="lblyAmt" runat="server" Width="80px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "taramt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label runat="server" ID="lblyFAmt"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle Font-Size="12px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Execution">
                                    <ItemTemplate>
                                        <asp:Label ID="lblytpayamt" runat="server" Width="80px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "examt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label runat="server" ID="lblyFtpayamt"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle Font-Size="12px" />
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

                        <asp:Label ID="lblGrp" runat="server" class="GrpHeader" Visible="false" Width="267px">D. Graph</asp:Label>
                        <%--  <cc1:BarChart ID="BarChart1" runat="server" CategoriesAxis="1,2,3" ChartTitle="" Visible="false"
                                ChartHeight="300" ChartType="Column" ChartWidth="800">
                            </cc1:BarChart>--%>


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
                                    <AxisX MaximumAutoSize="100" Interval="1" LineColor="YellowGreen">
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                            <Legends>
                                <asp:Legend Alignment="Center"></asp:Legend>
                            </Legends>
                        </asp:Chart>



                    </div>
                </div>

                <hr class="hrline" />
                <div class="row">
                    <div class="col-sm-12 col-md-12 col-lg-12">
                        <asp:Label ID="lblDetails" runat="server" class="GrpHeader" Visible="false" Width="823px">D. DAY WISE PURCHASE DETAILS</asp:Label>
                        <asp:GridView ID="GvDayWise" runat="server" AutoGenerateColumns="False"
                            Font-Size="10px" HorizontalAlign="Left" PageSize="5" ShowFooter="True"
                            CssClass="table-striped table-hover table-bordered grvContentarea">
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
                                <asp:TemplateField HeaderText="Bill </Br> Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblmemodat" runat="server" Width="60px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billdate1")) %>'></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle Font-Size="12px" />
                                    <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblbillno1" runat="server" Width="70px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>'></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle Font-Size="12px" />
                                    <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Voucher #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvounum1" runat="server" Width="70px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle Font-Size="12px" />
                                    <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Accounts Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblpactdesc" runat="server" Width="150px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle Font-Size="12px" />
                                    <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Materials Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblrsirdesc" runat="server" Width="150px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle Font-Size="12px" />
                                    <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblssirdesc" runat="server" Width="150px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>

                                        <asp:Label runat="server" ID="lblyTto">Total</asp:Label>

                                    </FooterTemplate>
                                    <HeaderStyle Font-Size="12px" />
                                    <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill </Br> Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblbillamt" runat="server" Width="80px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label runat="server" ID="lblFitmamt"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle Font-Size="12px" />
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
                </div>

                <hr class="hrline" />
                <div class="row">
                    <div class="col-sm-12 col-md-12 col-lg-12">
                        <asp:Label ID="lblPayDet" runat="server" class="GrpHeader" Visible="false" Width="823px">E. DAY WISE PAYMENT DETAILS</asp:Label>
                        <asp:GridView ID="gvDayWisePay" runat="server" AutoGenerateColumns="False"
                            Font-Size="10px" HorizontalAlign="Left" PageSize="5" ShowFooter="True"
                            CssClass="table-striped table-hover table-bordered grvContentarea">
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
                                <asp:TemplateField HeaderText="VOUCHER</BR> Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvoudat" runat="server" Width="60px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat")) %>'></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle Font-Size="12px" />
                                    <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="VOUCHER #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvounum1" runat="server" Width="70px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle Font-Size="12px" />
                                    <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="BILL #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblbillno1" runat="server" Width="70px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>'></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle Font-Size="12px" />
                                    <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Accounts Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblpactdesc" runat="server" Width="150px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle Font-Size="12px" />
                                    <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblssirdesc" runat="server" Width="150px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle Font-Size="12px" />
                                    <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcactdesc" runat="server" Width="150px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>

                                        <asp:Label runat="server" ID="lblFcustdesc1">Total</asp:Label>

                                    </FooterTemplate>
                                    <HeaderStyle Font-Size="12px" />
                                    <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pay </Br> Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblamount" runat="server" Width="80px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label runat="server" ID="lblFamount"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle Font-Size="12px" />
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
                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

