<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ASITNEW.Master" CodeBehind="SalesInformation.aspx.cs" Inherits="RealERPWEB.F_22_Sal.SalesInformation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="<%=this.ResolveUrl("~/Scripts/highchartwithmap.js")%>"></script>
    <script language="javascript" type="text/javascript">

        var comcod;
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            var s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12;

            var c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12;

            var b1, b2, b3, b4, b5, b6, b7, b8, b9, b10, b11, b12;

            var yc1, yc2, yc3, ys1, ys2, ys3, xaxis0, xaxis1, xaxis2;
        });
        function pageLoaded() {

            try {

                $("input, select").bind("keydown",
                    function (event) {
                        var k1 = new KeyPress();
                        k1.textBoxHandler(event);

                    });

                comcod = <%=this.GetCompCode()%>;


                $("#lbtnrevstatus").attr("href", "../F_23_CR/RptReceivedList04.aspx?Type=AllProDuesCollect&prjcode=&comcod=" + comcod);
                $("#lbtncusdues").attr("href", "../F_23_CR/RptCustomerDues.aspx?Type=Report&comcod=" + comcod);
                $("#lbtnsoldaunsold").attr("href", "RptSaleSoldunsoldUnit.aspx?Type=soldunsold&comcod=" + comcod + "&prjcode=&Date1=");
                $("#lbtnmonsales").attr("href", "../F_17_Acc/RptAccCollVsClearance.aspx?Type=MonSales&comcod=" + comcod);
                $("#lbtnmoncollection").attr("href", "../F_17_Acc/RptAccCollVsClearance.aspx?Type=MonAR&comcod=" + comcod);

            }


            catch (e) {
                alert(e);


            }


        }
    </script>

    <style>
        .gvTopHeader tr:nth-child(1) {
            height: 14px !important;
            font-size: 12px !important;
            font-weight: bold !important;
        }

        .linkItem a {
            font-size: 14px;
            line-height: 18px;
            margin: 4px 10px 0;
        }

        .modal-dialog-mid-width {
            width: 45% !important;
            height: 100% !important;
            margin: auto !important;
            padding: 0 !important;
            max-width: none !important;
        }

        .modal-content-mid-width {
            height: auto !important;
            min-height: 40% !important;
            border-radius: 0 !important;
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

                        <div class="col-md-2">
                            <asp:RadioButtonList ID="rbtList" runat="server" CssClass="form-check-inline" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">Actual</asp:ListItem>
                                <asp:ListItem Selected="True" Value="1">Reconcile</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>


                        <div class="col-md-8">

                            <ul class="nav nav-tabs card-header-tabs" style="margin-top: 20px;">
                                <li class="nav-item">
                                    <a class="nav-link active" data-toggle="tab" href="#yrwek">Yearly & Weekly</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link show " data-toggle="tab" href="#mon">Monthly</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link show" data-toggle="tab" href="#dayorder">Day Wise Sales Details</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link show" data-toggle="tab" href="#dayship">Day Wise Collection Details</a>
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
                                <div class="col-md-3">
                                    <div class="row">
                                        <div class="form-group">

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
                                                            <asp:Label ID="lblprodNmId" runat="server" Width="45px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "yearid")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sales">
                                                        <HeaderTemplate>
                                                            <a href="#" id="modaltggl" data-toggle="modal" data-target="#salModal">Sales</a>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblyAmt" runat="server" Width="80px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "samt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Collection">
                                                        <HeaderTemplate>
                                                            <a href="#" data-toggle="modal" data-target="#colModal">Collection</a>

                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblyCollamt" runat="server" Width="80px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "collamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" HorizontalAlign="Center" />
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
                                        <div class="form-group">

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

                                </div>
                                <div class="col-md-9">


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
                                                            <asp:Label ID="lblprodNmId1" runat="server" Width="55px" Style="text-align: center"
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
                                                            <asp:Label ID="lblyAmt1" runat="server" Width="62px"
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
                                                            <asp:Label ID="lblyCollamt1" runat="server" Width="65px"
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
                                                            <asp:Label ID="lblprodNmId2" runat="server" Width="55px" Style="text-align: center"
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
                                                            <asp:Label ID="lblyAmt2" runat="server" Width="62px"
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
                                                            <asp:Label ID="lblyCollamt2" runat="server" Width="65px"
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
                                                            <asp:Label ID="lblprodNmId3" runat="server" Width="55px" Style="text-align: center"
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
                                                            <asp:Label ID="lblyAmt3" runat="server" Width="60px"
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
                                                            <asp:Label ID="lblyCollamt3" runat="server" Width="65px"
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
                                                            <asp:Label ID="lblprodNmId4" runat="server" Width="55px" Style="text-align: center"
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
                                                            <asp:Label ID="lblyAmt4" runat="server" Width="62px"
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
                                                            <asp:Label ID="lblyCollamt4" runat="server" Width="65px"
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
                                        <asp:Panel ID="pnlbtn" runat="server" Visible="false">
                                            <div class="col-xs-3 col-md-3 col-lg-3"></div>

                                            <div class="col-xs-6 col-md-6 col-lg-6 linkItem">
                                            </div>
                                        </asp:Panel>
                                    </div>



                                </div>



                            </div>

                        </div>

                        <div class="tab-pane fade" id="mon">

                            <div class="row">
                                <div class="col-sm-4 col-md-4 col-lg-4 table-responsive">

                                    <asp:Label ID="lblMon" runat="server" class="GrpHeader" Visible="false" Width="267px">B. MONTHLY SALES & COLLECTION</asp:Label>

                                    <asp:GridView ID="grvMonthlySales" runat="server" AutoGenerateColumns="False"
                                        Font-Size="10px" HorizontalAlign="Left" PageSize="5" ShowFooter="True"
                                        CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="grvMonthlySales_RowDataBound">
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

                                            <asp:TemplateField HeaderText="montdhid" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmonthid" runat="server" Width="50px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ymon")) %>'></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle Font-Size="11px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="11px" Font-Bold="true" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Month">
                                                <ItemTemplate>
                                                    <%--<asp:HyperLink ID="lblprodNmId" runat="server" Width="50px" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "yearmon")) %>'></asp:HyperLink>--%>
                                                    <asp:HyperLink ID="lblprodNmId" runat="server" Width="50px" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "yearmon")) %>'></asp:HyperLink>


                                                    <%--    NavigateUrl="~/F_22_Sal/RptSalSummery.aspx?Type=dSaleVsColl"--%>
                                                    <%-- <asp:Label ID="lblprodNmId" runat="server" Width="50px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "yearmon")) %>'></asp:Label>--%>
                                                </ItemTemplate>
                                                <FooterTemplate>

                                                    <asp:Label runat="server" ID="lblyTto">Total</asp:Label>

                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="11px" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="11px" Font-Bold="true" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Target <br> Sales">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltargetysaleAmt" runat="server" Width="65px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "targtsaleamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="lblytarsaleFAmt"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="11px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="11px" Font-Bold="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Actual <br> Sales">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblyAmt" runat="server" Width="65px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlsalamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="lblyFAmt"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="11px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="11px" Font-Bold="true" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Target <br> Collection">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblytarCollamt" runat="server" Width="65px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tarcollamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="lblyFtarCollamt"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="11px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="11px" Font-Bold="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Actual <br> Collection">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblyCollamt" runat="server" Width="65px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "collamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="lblyFCollamt"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="11px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="11px" Font-Bold="true" />
                                            </asp:TemplateField>




                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                                <div class="col-sm-8 col-md-8 col-lg-8">

                                    <asp:Label ID="lblGrp" runat="server" class="GrpHeader" Visible="false" Width="267px">E. GRAPH</asp:Label>

                                    <%--      <asp:Chart ID="Chart1" runat="server" Width="800px">
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


                                    <div id="barchart" style="width: 710px; height: 282px; margin: 0 auto"></div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="dayorder">

                            <div class="row">
                                <div class="col-xs-12">
                                    <asp:Label ID="lblDetails" runat="server" class="GrpHeader" Visible="false" Width="267px">C. DAY WISE SALES DETAILS</asp:Label>


                                    <div class=" table-responsive">
                                        <asp:GridView ID="gvDayWSale" runat="server" AutoGenerateColumns="False"
                                            ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            OnRowDataBound="gvDayWSale_RowDataBound">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Project Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvDPactdesc" runat="server" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Customer Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvDcuname" runat="server" Font-Size="11px"
                                                            Text='<%#"<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))+"</b>"+"<br>"+
                                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "custadd")) %>'
                                                            Width="145px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description of Item">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvDResDesc" runat="server" Font-Size="11px"
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
                                                        <asp:Label ID="lgUnit" runat="server" Font-Size="11px"
                                                            Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "munit"))
                                                                         %>'
                                                            Width="35px"></asp:Label>


                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Unit Size">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvUSize" runat="server" Font-Size="11px"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px" Style="text-align: right"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Price per SFT">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvUpsft" runat="server" Font-Size="11px"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sftpr")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px" Style="text-align: right"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sales Team">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lgDCper" runat="server" Font-Size="11px"
                                                            Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "conteam"))
                                                                         %>'
                                                            Width="120px"></asp:Label>


                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Budgeted Amt.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvDTAmt" runat="server" Font-Size="11px"
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

                                                        <asp:HyperLink ID="HplgvSAmt" runat="server" Font-Size="11px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "suamt")).ToString("#,##0;(#,##0); ") %>' Target="_blank"></asp:HyperLink>


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
                                                        <asp:Label ID="lgvDSchdate" runat="server" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "schdate")) %>'
                                                            Width="70px" Style="text-align: center"></asp:Label>
                                                    </ItemTemplate>


                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Discount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvDDisAmt" runat="server" Font-Size="11px"
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
                                                        <asp:Label ID="lgDvDisPer" runat="server" Font-Size="11px"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disper")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="30px" Style="text-align: right"></asp:Label>
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
                        </div>
                        <div class="tab-pane fade" id="dayship">

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
                                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
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
                                                        <asp:Label ID="lgcMRDat" runat="server" Style="text-align: center"
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
                                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="12px"
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
                                                        <asp:Label ID="lgvChNo" runat="server" Style="text-align: center"
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
                                                        <asp:Label ID="lgvChDat" runat="server" Style="text-align: center"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqdate")) %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Reconciliation Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvRecDat1" runat="server" Style="text-align: center"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recndt")) %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Entry Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvEntrydate" runat="server" Style="text-align: center"
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
                        <!------text box for graph---->
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

            <div id="salModal" class="modal fade" role="dialog" data-keyboard="false" data-backdrop="static" aria-labelledby="gridModalLabel">
                <div class="modal-dialog modal-dialog-mid-width">
                    <div class="modal-content modal-content-mid-width">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <i class="fa fa-hand-point-right"></i>Sales Groth Information</h4>
                            <button type="button" class="btn btn-xs pull-right" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>

                        </div>
                        <div class="modal-body ">
                            <asp:GridView ID="grvCompareYear" runat="server" AutoGenerateColumns="False"
                                CssClass="table-striped table-hover table-bordered  grvContentarea ml-3" ShowFooter="True">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNocs" runat="server" Font-Bold="True"
                                                Style="text-align: right; font-size: 12px;"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"
                                                CssClass="gridtext"></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle Width="40px" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40px" Font-Size="12" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Month">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HypNmIdcs" runat="server" Width="90px" Target="_blank" ForeColor="Blue" Style="font-size: 12px; padding-left: 5px; text-align: left;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "yearmon")) %>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label runat="server" ID="lblyTtocs">Total</asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle Font-Size="12px" />
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Previous Year">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMonAmtcs" runat="server" Width="90px" Style="text-align: right; font-size: 12px;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "presamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label runat="server" ID="lblFPrevAmtcs"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle Font-Size="12px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Current Year">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMonCollamtcs" runat="server" Width="100px" Style="text-align: right; font-size: 12px;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cursal")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label runat="server" ID="lblypryCollamtcs"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle Font-Size="12px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Growth">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMonCollamtdif" runat="server" Width="90px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "diff")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label runat="server" ID="lblypryDiff"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle Font-Size="12px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" Font-Size="12px" />
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Growth %">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMonCollamtper" runat="server" Width="90px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;-#,##0.00; ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label runat="server" ID="lblypryCollamtcsps"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle Font-Size="12px" HorizontalAlign="Center" Width="90px" />
                                        <ItemStyle HorizontalAlign="Right" Font-Size="12px" />
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

                        <div class="modal-footer">
                            <button class="btn btn-primary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>


            <div id="colModal" class="modal fade" role="dialog" data-keyboard="false" data-backdrop="static" aria-labelledby="gridModalLabel">
                <div class="modal-dialog modal-dialog-mid-width">
                    <div class="modal-content modal-content-mid-width">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <i class="fa fa-hand-point-right"></i>Collection Groth Information</h4>
                            <button type="button" class="btn btn-xs pull-right" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>

                        </div>
                        <div class="modal-body ">
                            <asp:GridView ID="grvCompareYearColl" runat="server" AutoGenerateColumns="False"
                                CssClass="table-striped table-hover table-bordered  grvContentarea ml-3" ShowFooter="True">
                                <RowStyle />

                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNocoll" runat="server" Font-Bold="True"
                                                Style="text-align: right; font-size: 12px;"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                                CssClass="gridtext"></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle Width="30px" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" Font-Size="12px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Month">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HypNmIdcoll" runat="server" Width="90px" Target="_blank" ForeColor="Blue" Style="font-size: 12px; padding-left: 5px; text-align: left;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "yearmon")) %>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <FooterTemplate>

                                            <asp:Label runat="server" ID="lblyTtocs">Total</asp:Label>

                                        </FooterTemplate>
                                        <HeaderStyle Font-Size="12px" />
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Previous Year">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMonAmtcoll" runat="server" Width="90px" Style="text-align: right; font-size: 12px;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "presamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label runat="server" ID="lblFPrevAmtcoll"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle Font-Size="12px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Current Year">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMonCollamtcoll" runat="server" Width="90px" Style="text-align: right; font-size: 12px;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cursal")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label runat="server" ID="lblypryCollamtcoll"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle Font-Size="12px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Growth">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMonCollamtdifcoll" runat="server" Width="100px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "diff")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label runat="server" ID="lblypryDiffcoll"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle Font-Size="12px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" Font-Size="12px" />
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Growth %">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMonCollamtpercoll" runat="server" Width="100px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;-#,##0.00; ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label runat="server" ID="lblypryCollamtPrccoll"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle Font-Size="12px" HorizontalAlign="Center" Width="100px" />
                                        <ItemStyle HorizontalAlign="Right" Font-Size="12px" />
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

                        <div class="modal-footer">
                            <button class="btn btn-primary" data-dismiss="modal">Close</button>
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
                    name: 'Sales',
                    data: [s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12],
                    color: '#1581C1'

                }, {

                    name: 'Collection',
                    //color:red,
                    data: [c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12],
                    color: '#CA6621'
                }]
            });

        }





    </script>
</asp:Content>






