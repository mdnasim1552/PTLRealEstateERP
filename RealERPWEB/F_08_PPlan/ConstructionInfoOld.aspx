﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ConstructionInfoOld.aspx.cs" Inherits="RealERPWEB.F_08_PPlan.ConstructionInfoOld" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
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
        .linkItem a {
  font-size: 14px;
  line-height: 18px;
  margin: 4px 10px 0;
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
                                    <div class="col-md-3 pading5px ">
                                        <asp:Label ID="lbldatefrm" runat="server" CssClass=" lblTxt lblName" Text="Date"></asp:Label>
                                        <asp:TextBox ID="txtDate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDate" Enabled="true"></cc1:CalendarExtender>
                                        .
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>



                                        </div>
                                      
                                    </div>
                                   
                                    <div class="col-md-7 pading5px pull-right ">
                                        <asp:Panel ID="pnlbtn" runat="server" Visible="false">
                                 
                                    <a   href='<%=this.ResolveUrl("~/F_32_Mis/RptMisProIncomeExe.aspx?Type=MasPVsMonPVsExAllPro")%>' target="_blank" style="margin: 10px 0 0 10px; line-height: 18px;">Plan VS Execution-all</a>
                                    
                                    <a   href='<%=this.ResolveUrl("~/F_32_Mis/RptConstruProgressSum.aspx")%>' target="_blank" style="margin: 10px 0 0 10px; line-height: 18px;">Progress Report</a>
                               
                                  
                               
                                    </asp:Panel>

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


                      <%--  <asp:Chart ID="Chart1" runat="server" Width="800px">
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
                        </asp:Chart>--%>

                           <div id="barchart" style="width: 830px; height: 282px; margin: 0 auto"></div>

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
                    name: 'Target',
                    data: [s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12],
                    color: '#96780A'

                }, {

                    name: 'Execution',
                    //color:red,
                    data: [c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12],
                    color: '#B22564'
                }]
            });
            
        }



         
       
      </script>
</asp:Content>

