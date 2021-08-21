
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AccDashBoard.aspx.cs" Inherits="RealERPWEB.F_18_MAcc.AccDashBoard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <script src="<%=this.ResolveUrl("~/Scripts/highchartwithmap.js")%>"></script>
    
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            var s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12;

            var c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12;
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


            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>

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
                    <div class="card card-fluid">
                        <div class="card-body">
                            <div class="row">

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="lbldatefrm" runat="server" CssClass="control-label" Text="Date"></asp:Label>
                                        <asp:TextBox ID="txtDate" runat="server" AutoCompleteType="Disabled" CssClass="form-control"
                                            ClientIDMode="Static"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDate" Enabled="true"></cc1:CalendarExtender>

                                    </div>

                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lbtnOk" runat="server" Style="margin-top: 20px;" CssClass=" btn btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                    </div>
                                </div>

                                <div class="col-md-9">

                                    <ul class="nav nav-tabs card-header-tabs" style="margin-top: 20px;">
                                        <li class="nav-item">
                                            <a class="nav-link active" data-toggle="tab" href="#yrwek">Yearly & Weekly</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link show " data-toggle="tab" href="#mon">Monthly</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link show" data-toggle="tab" href="#dayorder">Month Wise Receipts</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link show" data-toggle="tab" href="#dayship">Month Wise Payment</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link show" data-toggle="tab" href="#dayshipcash">DETAILS OF CASH & BANK BALANCE</a>
                                        </li>
                                    </ul>
                                </div>



                            </div>
                        </div>
                    </div>
                    <div class="card card-fluid" style="min-height: 350px;">
                        <div class="card-body">
                            <div id="myTabContent" class="tab-content">

                                <div class="tab-pane fade active show" id="yrwek">
                                    <div class="row">
                                        <div class="col-sm-3 col-md-3 col-lg-3">
                                            <div class="col-sm-12 col-md-12 col-lg-12">
                                                <asp:Label ID="lblYear" runat="server" class="GrpHeader" Visible="false" Width="267px">A. YEARLY RECEIPT & PAYMENT</asp:Label>
                                                <asp:GridView ID="grvYearlySales" runat="server" AutoGenerateColumns="False"
                                                    Font-Size="10px" HorizontalAlign="Left" PageSize="5" ShowFooter="True"
                                                    CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="grvYearlySales_RowDataBound">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvSlNoyrwek" runat="server" Font-Bold="True"
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
                                                                <asp:Label ID="lblprodNmIdyrwek" runat="server" Width="50px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "yearid")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Size="10px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Receipt">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblyCollamtyrwek" runat="server" Width="80px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Size="10px" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Payment">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblyAmtyrwek" runat="server" Width="80px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
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
                                                    <asp:Label ID="lblWeek" runat="server" class="GrpHeader btn-block" Visible="false" Width="798px">C. WEEKLY RECEIPT & PAYMENT</asp:Label>
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
                                                                    <asp:Label ID="lblgvSwlNo1A" runat="server" Font-Bold="True"
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
                                                                    <asp:Label ID="lblprodNmId1A" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wcode1")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lbl1">Week Total</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFT">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="Label1">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Receipt">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyCollamt1A" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wramt1")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFCollamt1">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFCollamt1T">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="Label2">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Payment">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyAmt1A" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wpamt1")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFAmt1">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFAmt1T">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="Label3">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                                            </asp:TemplateField>




                                                            <asp:TemplateField HeaderText="Days">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblprodNmId2A" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wcode2")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lbl2">Week Total</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFT2">Sub-Total:</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="Label4">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Receipt">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyCollamt2A" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wramt2")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFCollamt2">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFCollamt2T">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="Label15">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Payment">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyAmt2A" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wpamt2")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFAmt2">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFAmt2T">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="Label5">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                                            </asp:TemplateField>




                                                            <asp:TemplateField HeaderText="Days">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblprodNmId3A" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wcode3")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lbl3">Week Total</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFT3">Sub-Total:</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="Label7">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Receipt">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyCollamt3A" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wramt3")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFCollamt3">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFCollamt3T">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="Label8">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Payment">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyAmt3A" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wpamt3")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFAmt3">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFAmt3T">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="Label9">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <HeaderStyle Font-Size="10px" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                                            </asp:TemplateField>




                                                            <asp:TemplateField HeaderText="Days">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblprodNmId4A" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wcode4")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lbl4">Week Total</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFT4">Gr Total:</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="Label10">Bank</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Receipt">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyCollamt4A" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wramt4")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFCollamt4">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFCollamt4T">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblFbRec">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Payment">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyAmt4A" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wpamt4")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFAmt4">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFAmt4T">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblFbPay">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <HeaderStyle Font-Size="10px" />
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
                                                <%-- <div class="col-xs-1 col-md-1 col-lg-1">
                                    <a class="btn btn-primary primaryBtn" href='<%=this.ResolveUrl("~/GenPage.aspx?Type=21")%>' target="_blank" style="margin: 10px 0 0 5px; line-height: 18px;">NEXT-1</a>

                                </div>--%>
                                                <div class="col-xs-1 col-md-1 col-lg-1">
                                                    <a class="btn btn-primary primaryBtn" href='<%=this.ResolveUrl("~/GenPage.aspx?Type=22")%>' target="_blank" style="margin: 10px 0 0 5px; line-height: 18px;">NEXT</a>

                                                </div>
                                            </div>



                                        </div>



                                    </div>

                                </div>
                                <div class="tab-pane fade" id="mon">
                                    <div class="row">
                                        <div class="col-sm-3 col-md-3 col-lg-3">

                                            <asp:Label ID="lblMon" runat="server" class="GrpHeader" Visible="false" Width="267px">B. MONTHLY RECEIPT & PAYMENT</asp:Label>

                                            <asp:GridView ID="grvMonthlySales" runat="server" AutoGenerateColumns="False"
                                                Font-Size="10px" HorizontalAlign="Left" PageSize="5" ShowFooter="True"
                                                CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="grvMonthlySales_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNomon" runat="server" Font-Bold="True"
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
                                                            <asp:Label ID="lblprodNmIdB" runat="server" Width="50px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "yearmon")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>

                                                            <asp:Label runat="server" ID="lblyTto">Total</asp:Label>

                                                        </FooterTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Receipt">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblyCollamtB" runat="server" Width="80px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblyFCollamt"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle Font-Size="12px" HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Payment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblyAmtB" runat="server" Width="80px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblyFAmt"></asp:Label>
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


                                            <%--    <asp:Chart ID="Chart1" runat="server" Width="800px" Style="margin-left: -11px;">
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
                            </asp:Chart>--%>

                                            <div id="barchart" style="width: 830px; height: 282px; margin: 0 auto"></div>



                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="dayorder">
                                    <div class="row">
                                        <div class="col-sm-12 col-md-12 col-lg-12">
                                            <asp:Label ID="lblReceiptCash" runat="server" Font-Bold="True" class="GrpHeader"
                                                Text="MONTH WISE RECEIPTS" Width="1155px" Visible="False"></asp:Label>
                                            <div class=" clearfix"></div>


                                            <asp:GridView ID="gvcashbook" runat="server" Font-Size="10px" AutoGenerateColumns="False" ShowFooter="True"
                                                Width="931px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNorecep" runat="server" Font-Bold="True" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDateB" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Voucher #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvvnumB" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cash/Bank Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvActDescB" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cheque/Ref #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvActDesc3B" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Accounts Head">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvCActDescB" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Party/Suppliers/Receivers Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvRDescB" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Pay To">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvPaytoRecB" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Narration">
                                                        <HeaderTemplate>
                                                            <table style="width: 47%;">
                                                                <tr>
                                                                    <td class="style58">
                                                                        <asp:Label ID="Label13" runat="server" Font-Bold="True"
                                                                            Text="Narration" Width="90px"></asp:Label>
                                                                    </td>
                                                                    <td class="style60">&nbsp;</td>
                                                                    <td>
                                                                        <asp:HyperLink ID="hlbtnCBdataExel" runat="server" BackColor="#000066"
                                                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                            ForeColor="White" Style="text-align: center" Width="50px">Export</asp:HyperLink>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvNarrationRB" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounar")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cash">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvCashAmtB" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "casham")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvCashAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                Style="text-align: right" Width="70px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bank">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvBankAmtB" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankam")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFBankAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                Style="text-align: right" Width="70px"></asp:Label>
                                                        </FooterTemplate>
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
                                <div class="tab-pane fade" id="dayship">
                                    <div class="row">
                                        <div class="col-sm-12 col-md-12 col-lg-12">

                                            <asp:Label ID="lblPaymentCash" runat="server" class="GrpHeader"
                                                Text="MONTH WISE PAYMENT" Width="1155px" Visible="False"></asp:Label>


                                            <asp:GridView ID="gvcashbookp" runat="server" Font-Size="10px" AutoGenerateColumns="False" ShowFooter="True"
                                                Width="931px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo2C" runat="server" Font-Bold="True" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDatepay" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Voucher #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvvnumpayC" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cash/Bank Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvActDesc0C" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cheque/Ref #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvActDesc1C" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Accounts Head">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvCActDesc0C" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Party/Suppliers/Receivers Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvRDesc0C" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pay To">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvpaytoC" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Narration">
                                                        <HeaderTemplate>
                                                            <table style="width: 47%;">
                                                                <tr>
                                                                    <td class="style58">
                                                                        <asp:Label ID="Label14" runat="server" Font-Bold="True"
                                                                            Text="Narration" Width="90px"></asp:Label>
                                                                    </td>
                                                                    <td class="style60">&nbsp;</td>
                                                                    <td>
                                                                        <asp:HyperLink ID="hlbtnCBPdataExel" runat="server" BackColor="#000066"
                                                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                            ForeColor="White" Style="text-align: center" Width="50px">Export</asp:HyperLink>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvNarrationpC" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounar")) %>'
                                                                Width="140px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cash">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvCashAmtpayC" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "casham")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvCashAmt1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                Style="text-align: right" Width="70px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bank">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvBankAmt0C" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankam")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFBankAmt1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                Style="text-align: right" Width="70px"></asp:Label>
                                                        </FooterTemplate>
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
                                <div class="tab-pane fade" id="dayshipcash">
                                    <div class="row">
                                        <div class="col-sm-12 col-md-12 col-lg-12">

                                            <asp:Label ID="lblDetailsCash" runat="server" Font-Bold="True" class="GrpHeader"
                                                Text="DETAILS OF CASH &amp; BANK BALANCE" Width="1005px"
                                                Visible="False"></asp:Label>


                                            <asp:GridView ID="gvcashbookDB" runat="server" Font-Size="10px" AutoGenerateColumns="False" ShowFooter="True"
                                                Width="973px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Accounts Head">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvActDesc2D" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                                Width="500px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Opening">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvOpeningD" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFOpening" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                Style="text-align: right" Width="100px"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Receipt">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvrecamD" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "depam")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblgvFrecam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                Style="text-align: right" Width="100px"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Payment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvpayamD" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="100px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFpayam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                Style="text-align: right" Width="100px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Closing">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvClAmtD" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsam")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFClAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                Style="text-align: right" Width="100px"></asp:Label>
                                                        </FooterTemplate>
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


                                <asp:TextBox ID="c1" runat="server" Style="display: none;"></asp:TextBox>
                                <asp:TextBox ID="c2" runat="server" Style="display: none;"></asp:TextBox>
                                <asp:TextBox ID="c3" runat="server" Style="display: none;"></asp:TextBox>
                                <asp:TextBox ID="c4" runat="server" Style="display: none;"></asp:TextBox>
                                <asp:TextBox ID="c5" runat="server" Style="display: none;"></asp:TextBox>
                                <asp:TextBox ID="c6" runat="server" Style="display: none;"></asp:TextBox>
                                <asp:TextBox ID="c7" runat="server" Style="display: none;"></asp:TextBox>
                                <asp:TextBox ID="c8" runat="server" Style="display: none;"></asp:TextBox>
                                <asp:TextBox ID="c9" runat="server" Style="display: none;"></asp:TextBox>
                                <asp:TextBox ID="c10" runat="server" Style="display: none;"></asp:TextBox>
                                <asp:TextBox ID="c11" runat="server" Style="display: none;"></asp:TextBox>
                                <asp:TextBox ID="c12" runat="server" Style="display: none;"></asp:TextBox>

                                <asp:TextBox ID="s1" runat="server" Style="display: none;"></asp:TextBox>
                                <asp:TextBox ID="s2" runat="server" Style="display: none;"></asp:TextBox>
                                <asp:TextBox ID="s3" runat="server" Style="display: none;"></asp:TextBox>
                                <asp:TextBox ID="s4" runat="server" Style="display: none;"></asp:TextBox>
                                <asp:TextBox ID="s5" runat="server" Style="display: none;"></asp:TextBox>
                                <asp:TextBox ID="s6" runat="server" Style="display: none;"></asp:TextBox>
                                <asp:TextBox ID="s7" runat="server" Style="display: none;"></asp:TextBox>
                                <asp:TextBox ID="s8" runat="server" Style="display: none;"></asp:TextBox>
                                <asp:TextBox ID="s9" runat="server" Style="display: none;"></asp:TextBox>
                                <asp:TextBox ID="s10" runat="server" Style="display: none;"></asp:TextBox>
                                <asp:TextBox ID="s11" runat="server" Style="display: none;"></asp:TextBox>
                                <asp:TextBox ID="s12" runat="server" Style="display: none;"></asp:TextBox>



                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function ExecuteMyGraph() {

            s1 = parseFloat($('#<%=this.s1.ClientID%>').val());
            s2 = parseFloat($('#<%=this.s2.ClientID%>').val());
            s3 = parseFloat($('#<%=this.s3.ClientID%>').val());
            s4 = parseFloat($('#<%=this.s4.ClientID%>').val());
            s5 = parseFloat($('#<%=this.s5.ClientID%>').val());
            s6 = parseFloat($('#<%=this.s6.ClientID%>').val());
            s7 = parseFloat($('#<%=this.s7.ClientID%>').val());
            s8 = parseFloat($('#<%=this.s8.ClientID%>').val());
            s9 = parseFloat($('#<%=this.s9.ClientID%>').val());
            s10 = parseFloat($('#<%=this.s10.ClientID%>').val());
            s11 = parseFloat($('#<%=this.s11.ClientID%>').val());
            s12 = parseFloat($('#<%=this.s12.ClientID%>').val());




            c1 = parseFloat($('#<%=this.c1.ClientID%>').val());
            c2 = parseFloat($('#<%=this.c2.ClientID%>').val());
            c3 = parseFloat($('#<%=this.c3.ClientID%>').val());
            c4 = parseFloat($('#<%=this.c4.ClientID%>').val());
            c5 = parseFloat($('#<%=this.c5.ClientID%>').val());
            c6 = parseFloat($('#<%=this.c6.ClientID%>').val());
            c7 = parseFloat($('#<%=this.c7.ClientID%>').val());
            c8 = parseFloat($('#<%=this.c8.ClientID%>').val());
            c9 = parseFloat($('#<%=this.c9.ClientID%>').val());
            c10 = parseFloat($('#<%=this.c10.ClientID%>').val());
            c11 = parseFloat($('#<%=this.c11.ClientID%>').val());
            c12 = parseFloat($('#<%=this.c12.ClientID%>').val());






            funMonthlyGraph();


            //    Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        }
        function funMonthlyGraph() {

            Highcharts.setOptions({
                lang: {
                    decimalPoint: ',',
                    thousandsSep: ' '
                }
            });




            $('#barchart').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                //subtitle: {
                //    text: 'Source: '
                //},
                xAxis: {
                    categories: [
                        'Jan',
                        'Feb',
                        'Mar',
                        'Apr',
                        'May',
                        'Jun',
                        'Jul',
                        'Aug',
                        'Sep',
                        'Oct',
                        'Nov',
                        'Dec'
                    ],
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Amount'
                    }
                },


                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:0f}</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true,

                    //pointFormat: "{point.y:, .5f} Lac <br>"
                    //pointFormat: '{point.percentage:.0f}%'



                    //pointFormat: '{series.name}: <b>{point.percentage}%</b>',  pointFormat: '{series.name}: <b>{point.y}%</b>',


                    //<b>{point.y:.1f} mm</b>


                },
                plotOptions: {
                    column: {
                        pointPadding: 0.1,
                        borderWidth: 0


                    }
                },
                series: [{
                    name: 'Receipt',
                    data: [s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12],
                    color: '#96780A'

                }, {

                    name: 'Payment',
                    //color:red,
                    data: [c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12],
                    color: '#B22564'
                }]
            });

        }





    </script>
</asp:Content>


