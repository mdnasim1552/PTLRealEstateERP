<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptDashBoardLandPro.aspx.cs" Inherits="RealERPWEB.F_01_LPA.RptDashBoardLandPro" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>


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
                                <asp:Label ID="lblYear" runat="server" class="GrpHeader" Visible="false" Width="267px">A. YEARLY PURCHASE & PAYMENT</asp:Label>
                                <asp:GridView ID="gvYearLandPur" runat="server" AutoGenerateColumns="False"
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
                                                <asp:Label ID="lblyearid" runat="server" Width="50px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "yearid")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Size="10px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase">
                                            <ItemTemplate>
                                                <asp:Label ID="lbyrlypuramt" runat="server" Width="80px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "puram")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Size="10px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Payment">
                                            <ItemTemplate>
                                                <asp:Label ID="lblyrlypyamntam" runat="server" Width="80px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paymntam")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
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
                                    <asp:Label ID="lblWeek" runat="server" class="GrpHeader btn-block" Visible="false" Width="798px">C. WEEKLY PURCHASE & PAYMENT</asp:Label>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:GridView ID="grvWeekLP" runat="server" AutoGenerateColumns="False"
                                        Font-Size="10px" HorizontalAlign="Left" PageSize="5" ShowFooter="True"
                                        CssClass="table-striped table-hover table-bordered grvContentarea gvTopHeader"  OnRowCreated="grvWeekLP_RowCreated">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSwlwk" runat="server" Font-Bold="True"
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
                                                    <asp:Label ID="lbldayswk" runat="server" Width="55px"
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
                                            <asp:TemplateField HeaderText="Purchase">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpurchase1wk" runat="server" Width="55px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wpamt1")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFpuramt1">-</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFpuramt1T">-</asp:Label>
                                                    </p>
                                                    

                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="10px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Payment">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvpayment1wk" runat="server" Width="55px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wpayamt1")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFpayamt1">-</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFpayamt1T">-</asp:Label>
                                                    </p>
                                                    

                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                            </asp:TemplateField>




                                            <asp:TemplateField HeaderText="Days">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblprodNmId2wk" runat="server" Width="55px"
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
                                            <asp:TemplateField HeaderText="Purchase">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpurchase2wk" runat="server" Width="55px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wpamt2")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFpuramt2">-</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFpuramt2T">-</asp:Label>
                                                    </p>
                                                     

                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="10px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Payment">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvpayment2wk" runat="server" Width="55px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wpayamt2")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFpayamt2">-</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFpayamt2T">-</asp:Label>
                                                    </p>
                                                     

                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                            </asp:TemplateField>




                                            <asp:TemplateField HeaderText="Days">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblprodNmId3wk" runat="server" Width="55px"
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
                                            <asp:TemplateField HeaderText="Purchase">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpurchase3wk" runat="server" Width="55px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wpamt3")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFpuramt3">-</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFpuramt3T">-</asp:Label>
                                                    </p>
                                                    
                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="10px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Payment">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvpayment3wk" runat="server" Width="55px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wpayamt3")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFpayamt3">-</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFpayamt3T">-</asp:Label>
                                                    </p>
                                                    

                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Font-Size="10px" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                            </asp:TemplateField>




                                            <asp:TemplateField HeaderText="Days">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblprodNmId4wk" runat="server" Width="55px"
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
                                            <asp:TemplateField HeaderText="Purchase">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpurchase4wk" runat="server" Width="55px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wpamt4")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFpuramt4">-</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFpuramt4T">-</asp:Label>
                                                    </p>
                                                    

                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="10px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Payment">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvpayment4wk" runat="server" Width="55px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wpayamt4")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFpayamt4">-</asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label runat="server" ID="lblyFpayamt4T">-</asp:Label>
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
                                <div class="col-xs-1 col-md-1 col-lg-1">
                                    <a class="btn btn-primary primaryBtn" href='<%=this.ResolveUrl("~/F_01_LPA/RptLandProcurement.aspx?Type=LandStSum")%>' target="_blank" style="margin: 10px 0 0 5px; line-height: 18px;">NEXT</a>

                                </div>
                               
                            </div>



                        </div>



                    </div>

                    <hr class="hrline" />
                   
                    <div class="row">
                        <div class="col-sm-3 col-md-3 col-lg-3">

                            <asp:Label ID="lblMon" runat="server" class="GrpHeader" Visible="false" Width="267px">B. MONTHLY PURCHASE & PAYMENT</asp:Label>

                             <asp:GridView ID="grvMonthlylp" runat="server" AutoGenerateColumns="False"
                                Font-Size="10px" HorizontalAlign="Left" PageSize="5" ShowFooter="True"
                                CssClass="table-striped table-hover table-bordered grvContentarea">
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
                                            <asp:Label ID="lblymonmon" runat="server" Width="50px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "yearmon")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>

                                            <asp:Label runat="server" ID="lblyTto">Total</asp:Label>

                                        </FooterTemplate>
                                        <HeaderStyle Font-Size="12px" />
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Purchase">
                                        <ItemTemplate>
                                            <asp:Label ID="lbpuramtmon" runat="server" Width="80px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "puram")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label runat="server" ID="lblFpuramtmon"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle Font-Size="12px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payment">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpaymntmon" runat="server" Width="80px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paymntam")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label runat="server" ID="lblFpaymntmon"></asp:Label>
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


                            <asp:Chart ID="Chart1" runat="server" Width="800px" Style="margin-left: -11px;">
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
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Voucher #">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvvnum" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cash/Bank Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvActDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cheque/Ref #">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvActDesc3" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Accounts Head">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCActDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Party/Suppliers/Receivers Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Pay To">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvPaytoRec" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Narration">
                                        <HeaderTemplate>
                                            <table style="width: 47%;">
                                                <tr>
                                                    <td class="style58">
                                                        <asp:Label ID="Label13" runat="server" Font-Bold="True"
                                                            Text="Narration" Width="130px"></asp:Label>
                                                    </td>
                                                    <td class="style60">&nbsp;</td>
                                                    <td>
                                                        <asp:HyperLink ID="hlbtnCBdataExel" runat="server" BackColor="#000066"
                                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                            ForeColor="White" Style="text-align: center" Width="70px">Export Exel</asp:HyperLink>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvNarrationR" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounar")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cash">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvCashAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "casham")).ToString("#,##0;(#,##0); ") %>'
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
                                            <asp:Label ID="txtgvBankAmt" runat="server" BackColor="Transparent" BorderStyle="None"
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
                                            <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Style="text-align: right"
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
                                            <asp:Label ID="lblgvvnumpay" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cash/Bank Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvActDesc0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cheque/Ref #">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvActDesc1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Accounts Head">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCActDesc0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Party/Suppliers/Receivers Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRDesc0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pay To">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvpayto" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Narration">
                                        <HeaderTemplate>
                                            <table style="width: 47%;">
                                                <tr>
                                                    <td class="style58">
                                                        <asp:Label ID="Label14" runat="server" Font-Bold="True"
                                                            Text="Narration" Width="130px"></asp:Label>
                                                    </td>
                                                    <td class="style60">&nbsp;</td>
                                                    <td>
                                                        <asp:HyperLink ID="hlbtnCBPdataExel" runat="server" BackColor="#000066"
                                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                            ForeColor="White" Style="text-align: center" Width="70px">Export Exel</asp:HyperLink>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvNarrationp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounar")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cash">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvCashAmtpay" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "casham")).ToString("#,##0;(#,##0); ") %>'
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
                                            <asp:Label ID="txtgvBankAmt0" runat="server" BackColor="Transparent" BorderStyle="None"
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
                                            <asp:Label ID="lblgvActDesc2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                Width="500px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Opening">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvOpening" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFOpening" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                Style="text-align: right" Width="100px"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Receipt">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvrecam" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "depam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFrecam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                Style="text-align: right" Width="100px"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payment">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpayam" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>'
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
                                            <asp:Label ID="lgvClAmt" runat="server" BackColor="Transparent" BorderStyle="None"
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

            </div>




        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

