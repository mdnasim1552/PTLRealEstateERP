
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkConstruProgress.aspx.cs" Inherits="RealERPWEB.F_32_Mis.LinkConstruProgress" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

        }

    </script>
    <style>
        .grvHeader th{
            font-weight:normal;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <asp:Label ID="lbljavascript" runat="server"></asp:Label>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">

                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-8 pading5px">
                                       <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName">Date</asp:Label>
                                        <asp:Label ID="lblvalDate" runat="server" CssClass="inputTxt inpPixedWidth" Width="100px" TabIndex="10"></asp:Label>
                                      
                                        <asp:Label ID="lblproject" runat="server" CssClass="lblTxt lblName">Project Name:</asp:Label>
                                        <asp:Label ID="lblvalproject" runat="server" CssClass=" inputlblVal" Style="width:250px;"></asp:Label>

                                        <asp:Label ID="lbltk" runat="server" CssClass="lblTxt lblName" Style="font-size:16px;">Taka in Lac </asp:Label>
                                    </div>
                                  
                                    


                                </div>
                               
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <div class="col-md-7">
                            <div class="row">
                            <asp:GridView ID="gvConPro" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" ShowFooter="True" Width="378px" PageSize="20"
                            OnPageIndexChanging="gvConPro_PageIndexChanging"
                            OnRowDataBound="gvConPro_RowDataBound">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Floor Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvflrCode" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrcod")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Floor Description">
                                    <FooterTemplate>

                                        <asp:Label ID="lgvTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                             ForeColor="#000" Style="text-align: right" Width="50px" Text="Total"></asp:Label>



                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvDesc" runat="server" Font-Underline="false" Target="_blank" ForeColor="blue"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                            Width="100px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Work % ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvWorkP" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perwork")).ToString("#,##0.00;(#,##0.00); ")+"%" %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFWorkP" runat="server" Font-Bold="True" Font-Size="12px"
                                             ForeColor="#000" Style="text-align: right" Width="50px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Budget Amt">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvBudgetAmt" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFBgdAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                             ForeColor="#000" Style="text-align: right" Width="60px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Master Plan Amt.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvMasPlan" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mplan")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFMasPlan" runat="server" Font-Bold="True" Font-Size="12px"
                                             ForeColor="#000" Style="text-align: right" Width="60px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Master Plan As Of Today">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvMPlanastoday" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mplanat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFMPlanastoday" runat="server" Font-Bold="True" Font-Size="12px"
                                             ForeColor="#000" Style="text-align: right" Width="60px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Execution Amt.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvExAmt" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "eamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>

                                           
                                        <asp:HyperLink ID="hlnkgvFexAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                             ForeColor="Blue" Target="_blank" Style="text-align: right" Width="60px"></asp:HyperLink>

                                 
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Less Execution">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvlessExAmt" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "leamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:HyperLink ID="hlnkgvFlessexAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                             ForeColor="Blue" Target="_blank" Style="text-align: right" Width="60px"></asp:HyperLink>
                                    </FooterTemplate>


                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Execution % on M.P As Of Today">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvprcent" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prcent")).ToString("#,##0.00;(#,##0.00); ")%>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>

                                        <asp:Label ID="lgvFPercent" runat="server" Font-Bold="True" Font-Size="12px"
                                             ForeColor="#000" Style="text-align: right" Width="50px"></asp:Label>


                                    </FooterTemplate>
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

                        </div>  </div>
                        <div class="col-md-5">
                             <div class="row">
                            <asp:Chart ID="Chart1" runat="server" Width="500px" Height="380px" style="margin-left:-20px;">
                            <Series>
                                <asp:Series ChartArea="ChartArea1" ChartType="Column" Color="green" LegendText="Actual Execution"   
                                    MarkerColor="black" MarkerStyle="None" Name="Series1"  MarkerSize="4">
                                </asp:Series>
                                <asp:Series ChartArea="ChartArea1" ChartType="Column" Color="BlueViolet" 
                                    MarkerColor="black" MarkerStyle="None" Name="Series2" MarkerSize="4">
                                </asp:Series> 
                                  <asp:Series ChartArea="ChartArea1" ChartType="Column" Color="red" 
                                    MarkerColor="black" MarkerStyle="None" Name="Series3" MarkerSize="4">
                                </asp:Series>



                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                    <AxisX MaximumAutoSize="100"  Interval="1" LineColor="YellowGreen">
                                        <MajorGrid Enabled="false" />
                                       
                                    </AxisX>
                      <%--  <Area3DStyle Enable3D="True" />

                              --%>  </asp:ChartArea>
                            </ChartAreas>

                            <Legends>
                                <asp:Legend Docking="Top" Alignment="Center" ></asp:Legend>
                            </Legends>
                        </asp:Chart>
                        </div>
                              </div>
                        
                    </div>
                    <div class="row">
                        <asp:Panel ID="Pnlnote" runat="server" Visible="False">

                            <asp:Label ID="Label15" runat="server" Width="300px" CssClass="btn btn-success primaryBtn" style="text-align:left;" >Note :</asp:Label>
                            <div class="clearfix"></div>
                            <div class="form-group" style="margin:0;">
                                <asp:Label ID="Label7" runat="server" Text="Budgeted Execution in %" CssClass=" smLbl_to"></asp:Label>

                                <asp:Label ID="lPercentonbgd" runat="server" CssClass=" smLbl_to"></asp:Label>
                                <div class=" clearfix"></div>

                            </div>
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" Text="Actual Execution in %" CssClass=" smLbl_to"></asp:Label>

                                <asp:Label ID="lPercentonbgdexe" runat="server" CssClass=" smLbl_to"></asp:Label>
                                <div class=" clearfix"></div>

                            </div>

                        </asp:Panel>
                    </div>
                </div>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

