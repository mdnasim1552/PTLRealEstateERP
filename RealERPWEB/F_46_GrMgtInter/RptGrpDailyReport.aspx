<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptGrpDailyReport.aspx.cs" Inherits="RealERPWEB.F_46_GrMgtInter.RptGrpDailyReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .gvTopHeader tr:nth-child(1) {
            height: 14px !important;
            font-size: 12px !important;
            font-weight: bold !important;
        }
        
    </style>
    <asp:CheckBox ID="chkGrp" runat="server" BackColor="Black" Font-Bold="True" Visible="false"
        Font-Size="12px" Style="color: Black; text-align: center; height: 20px;"
        TabIndex="4" Text="Graph" Width="80px" />

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <asp:Panel ID="Panel2" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-4 pading5px asitCol4">
                                            <asp:Label ID="lblDatefrom" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                            <asp:TextBox ID="txtDate" runat="server" CssClass="inputtextbox "></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtDate" Enabled="true"></cc1:CalendarExtender>


                                            <asp:Label ID="lblDateTo" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                            <asp:TextBox ID="txttodate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txttodate" Enabled="true"></cc1:CalendarExtender>
                                            <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_Click" CssClass="btn btn-primary primaryBtn">Show</asp:LinkButton>

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
                                </asp:Panel>


                            </div>
                        </fieldset>
                    </div>


                    <fieldset>
                        <asp:Label ID="lblToDayAc" runat="server"
                            Text="A. TO-DAY'S ACHIEVEMENT" Visible="False" CssClass="smLbl_to"></asp:Label>
                    </fieldset>
                    <div class="row">
                        <div class=" col-md-8 col-sm-8 col-lg-8">
                            <asp:GridView ID="grvToAch" runat="server" AutoGenerateColumns="False" OnRowDataBound="grvToAch_RowDataBound"
                                ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea gvTopHeader"
                                OnRowCreated="grvToAch_RowCreated">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lkgvcomnameAch" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                                Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                                Width="100px">
                                            </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sales">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtsal" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsal")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px">
                                            </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Coll. from Sales">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtcoll" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcoll")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Receipts">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtrec" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trec")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px">
                                            </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payments">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtpay" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tpay")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Receipts">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtcrec" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcrec")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issued">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtcisu" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcisu")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>
                        <div class=" col-md-4 col-sm-4 col-lg-4">
                            <asp:Panel ID="PanelAch" runat="server" Visible="false">
                                <cc1:BarChart ID="BarChart5" runat="server" CategoriesAxis="1,2,3" ChartTitle="Amount in Crore"
                                    ChartHeight="220" ChartType="Column" ChartWidth="250">
                                </cc1:BarChart>
                            </asp:Panel>
                        </div>
                    </div>


                    <fieldset>
                        <asp:Label ID="lblSales" runat="server" CssClass="smLbl_to" Text="B. SALES STATUS" Visible="False"></asp:Label>
                    </fieldset>
                    <div class="row">
                        <div class=" col-md-8 col-sm-8 col-lg-8">
                            <asp:GridView ID="gvDayWSale" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvDayWSale_RowDataBound"
                                ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea gvTopHeader"
                                OnRowCreated="gvDayWSale_RowCreated">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lkgvcomnamesale" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                                Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                                Width="100px">
                                            </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlnkgvtsaleamt" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                                Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsaleamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px">
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="As of Today">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtosaleamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tosaleamt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlnkgvDSAmt" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                                Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acsaleamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px">
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Today">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtdayamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tdayamt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="%">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvperotsale" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perotsale")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Graph">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlnkgvgraph" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                                Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "graph"))%>'
                                                Width="50px">
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>
                        <div class=" col-md-4 col-sm-4 col-lg-4">
                            <asp:Panel ID="PSales" runat="server" Visible="false">
                                <cc1:BarChart ID="BarChart1" runat="server" CategoriesAxis="1,2,3" ChartTitle="Amount in Crore"
                                    ChartHeight="220" ChartType="Column" ChartWidth="250">
                                </cc1:BarChart>
                            </asp:Panel>
                        </div>
                    </div>

                    <fieldset>
                        <asp:Label ID="lblColl" runat="server" CssClass="smLbl_to" Text="C. COLLECTION AGAINST SALES" Visible="False"></asp:Label>
                    </fieldset>

                    <div class="row">
                        <div class=" col-md-8 col-sm-8 col-lg-8">
                            <asp:GridView ID="gvCollSt" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvCollSt_RowDataBound"
                                ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea gvTopHeader"
                                OnRowCreated="gvCollSt_RowCreated">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lkgvcomnameColl" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                                Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                                Width="100px">
                                            </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlnkgvtcollamt" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                                Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcollamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px">
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="As of Today">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtosaleamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tastcollamt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlnkgvaccollAmt" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                                Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "accollam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px">
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Today">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtdayamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tdaycollamt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="%">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvperotsale" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perotcoll")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Graph">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlnkgvgraph" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                                Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "graph"))%>'
                                                Width="50px">
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>
                        <div class=" col-md-4 col-sm-4 col-lg-4">
                            <asp:Panel ID="PCollection" runat="server" Visible="false">
                                <cc1:BarChart ID="BarChart2" runat="server" CategoriesAxis="1,2,3" ChartTitle="Amount in Crore"
                                    ChartHeight="220" ChartType="Column" ChartWidth="250">
                                </cc1:BarChart>
                            </asp:Panel>
                        </div>
                    </div>

                    <fieldset>
                        <asp:Label ID="lblCollBrk" runat="server" CssClass="smLbl_to" Text="D. COLLECTION BREAK DOWN OF C" Visible="False"></asp:Label>
                    </fieldset>

                    <div class="row">
                        <div class=" col-md-12 col-sm-12 col-lg-12">
                            <asp:GridView ID="gvrcoll" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea gvTopHeader"
                                ShowFooter="True" Width="531px" OnRowDataBound="gvrcoll_RowDataBound"
                                OnRowCreated="gvrcoll_RowCreated">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoidre" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lkgvcomnameCollBrk" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                                Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                                Width="100px">
                                            </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Collection">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtocollection" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acamt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bank Clearance">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvreconamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reconamt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cheque Deposited">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvchqdep" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "depchq")).ToString("#,##0;-#,##0; ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Returned">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvinhrchq" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inhrchq")).ToString("#,##0;-#,##0; ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fresh">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvinhfchq" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inhfchq")).ToString("#,##0;-#,##0; ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Post Dated">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvinhpchq" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inhpchq")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Replacement Cheque">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvrepchq" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "repchq")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Net Collection </Br>(Without Replacement)">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvncollamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ncollamt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
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

                    <fieldset>
                        <asp:Label ID="lblRecPay" runat="server" CssClass="smLbl_to" Text="E. RECEIPTS & PAYMENTS" Visible="False"></asp:Label>
                    </fieldset>

                    <div class="row">
                        <div class=" col-md-8 col-sm-8 col-lg-8">
                            <asp:GridView ID="grvRecPay" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                Width="341px" OnRowDataBound="grvRecPay_RowDataBound" CssClass=" table-striped table-hover table-bordered grvContentarea gvTopHeader"
                                OnRowCreated="grvRecPay_RowCreated">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoidre0" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCompanyRecPay" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                                Width="100px"> 
                                            
                                            
                                            
                                            </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pre. Sales">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvlmrecamt" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="text-align: right; background-color: Transparent" Font-Underline="false"
                                                ForeColor="Black" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lmrecamt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cur. Sales">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcmrecamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cmrecamt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Loan & Others">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvotrecamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "otrecamt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvrecpam" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recpam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Payment">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlnkgvpayam" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="text-align: right; background-color: Transparent" Font-Underline="false"
                                                ForeColor="Black" Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Graph">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlnkgvgraphRec" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                                Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "graph"))%>'
                                                Width="50px">
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>
                        <div class=" col-md-4 col-sm-4 col-lg-4">
                            <asp:Panel ID="PanelRec" runat="server" Visible="false">
                                <cc1:BarChart ID="BarChart3" runat="server" CategoriesAxis="1,2,3" ChartTitle="Amount in Crore"
                                    ChartHeight="220" ChartType="Column" ChartWidth="250">
                                </cc1:BarChart>
                            </asp:Panel>
                        </div>
                    </div>
                    <fieldset>
                        <asp:Label ID="lblAvFund" runat="server" CssClass="smLbl_to" Text="F. AVAILABLE FUND STATUS" Visible="False"></asp:Label>
                    </fieldset>

                    <div class="row">
                        <div class=" col-md-12 col-sm-12 col-lg-12">
                            <asp:GridView ID="grvAvFund" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                Width="341px" OnRowDataBound="grvAvFund_RowDataBound" CssClass=" table-striped table-hover table-bordered grvContentarea gvTopHeader"
                                OnRowCreated="grvAvFund_RowCreated">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoidre0" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCompanyFund" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                                Width="100px"> 
                                            </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fresh">
                                        <ItemTemplate>
                                            <asp:Label ID="hlnkgvainhfchq" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="text-align: right; background-color: Transparent" Font-Underline="false"
                                                ForeColor="Black" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ainhfchq")).ToString("#,##0;-#,##0; ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Return">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvainhrchq" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ainhrchq")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Deposit">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvadepchq" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adepchq")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Replacement">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvarepchq" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "arepchq")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Bank Balance">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="Hypgvclosbal" runat="server" BorderColor="#99CCFF" BorderStyle="Solid" Target="_blank" Font-Underline="false"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closbal")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Loan Limit">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvbankbal" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankbal")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Post Dated">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlnkainhpchq" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="text-align: right; background-color: Transparent" Font-Underline="false"
                                                ForeColor="Black" Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ainhpchq")).ToString("#,##0;-#,##0; ") %>'
                                                Width="70px"></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtavamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tavamt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
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
                    <fieldset>
                        <asp:Label ID="lblChqisu" runat="server" CssClass="smLbl_to" Text="G. PAYMENTS DURING THE PERIOD" Visible="False"></asp:Label>
                    </fieldset>

                    <div class="row">
                        <div class=" col-md-12 col-sm-12 col-lg-12">
                            <asp:GridView ID="gvChqIsu" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea gvTopHeader"
                                Width="616px" OnRowDataBound="gvChqIsu_RowDataBound">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialno" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                Width="25px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCompanyChqIs" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                                Width="100px"> 
                                            </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amt1">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt1" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt1")).ToString("#,##0;(#,##0); ")%>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amt2">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt2" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt2")).ToString("#,##0;(#,##0); ")%>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amt3">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt3" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt3")).ToString("#,##0;(#,##0); ") %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amt4">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt4" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt4")).ToString("#,##0;(#,##0); ") %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amt5">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt5" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt5")).ToString("#,##0;(#,##0); ") %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amt6">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamt6" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt6")).ToString("#,##0;(#,##0); ") %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Total Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
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

                    <fieldset>
                        <asp:Label ID="lblRecPayiss" runat="server" CssClass="smLbl_to" Text="H. CHEQUED RECEIPTS & ISSUED" Visible="False"></asp:Label>
                    </fieldset>

                    <div class="row">
                        <div class=" col-md-8 col-sm-8 col-lg-8">
                            <asp:GridView ID="gvRecPayiss" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" OnRowDataBound="gvRecPayiss_RowDataBound" CssClass=" table-striped table-hover table-bordered grvContentarea gvTopHeader"
                                OnRowCreated="gvRecPayiss_RowCreated">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoidre0" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCompanycih" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                                Width="140px"> 
                                            
                                            
                                            
                                            </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Receipts">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvrecamis" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recpamis")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issued">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlnkgvbalamis" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="text-align: right; background-color: Transparent" Font-Underline="false"
                                                ForeColor="Black" Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payamis")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>
                        <div class=" col-md-4 col-sm-4 col-lg-4">
                            <asp:Panel ID="PanelRecIsu" runat="server" Visible="false">
                                <cc1:BarChart ID="BarChart4" runat="server" CategoriesAxis="1,2,3" ChartTitle="Amount in Crore"
                                    ChartHeight="220" ChartType="Column" ChartWidth="250">
                                </cc1:BarChart>
                            </asp:Panel>
                        </div>
                    </div>

                    <fieldset>
                        <asp:Label ID="lblProcurement" runat="server" CssClass="smLbl_to" Text="I. PROCUREMENT & CONSTRACTION" Visible="False"></asp:Label>
                    </fieldset>

                    <div class="row">
                        <div class=" col-md-12 col-sm-12 col-lg-12">
                            <asp:GridView ID="gvprocure" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                Width="341px" OnRowDataBound="gvprocure_RowDataBound" CssClass=" table-striped table-hover table-bordered grvContentarea gvTopHeader"
                                OnRowCreated="gvprocure_RowCreated">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoidre1" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Comcode" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcomcodepro" runat="server" Font-Bold="True" Style="text-align: right"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDepartpro" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                                Width="140px"> 
                                            
                                            
                                            </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Materials Received">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlnkgvourchasepro" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="text-align: right; background-color: Transparent; color: Black;"
                                                Font-Underline="false" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrramt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="70px" Target="_blank"></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Target">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvmonplan" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "monplan")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFmonPlan" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Execution">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvExecutionpAC" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "excution")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFExecutionpAC" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
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
                    <fieldset>
                        <asp:Label ID="lblStock" runat="server" CssClass="smLbl_to" Text="J. STOCK, UNSOLD, SOLD, RECEIVED & RECEIVABLE" Visible="False"></asp:Label>
                    </fieldset>

                    <div class="row">
                        <div class=" col-md-12 col-sm-12 col-lg-12">
                            <asp:GridView ID="gvpstk" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                Width="341px" OnRowDataBound="gvpstk_RowDataBound" CssClass=" table-striped table-hover table-bordered grvContentarea gvTopHeader">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoidre2" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company Name">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlnkgvcomname" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="text-align: left; background-color: Transparent; color: Black;"
                                                Font-Underline="false" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                                Width="100px">
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Revenue">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtstkamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "revamt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UnSold Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvunsoldamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usoldamt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sold Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvsoldamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "soldamt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Received">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtoramt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recamt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Receivable">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvatodues" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recabamt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
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
                    <fieldset>
                        <asp:Label ID="lblMonProStatus" runat="server" CssClass="smLbl_to" Text="K. PROJECT STATUS (DURING THE PERIOD)" Visible="False"></asp:Label>
                    </fieldset>

                    <div class="row">
                        <div class=" col-md-12 col-sm-12 col-lg-12">
                            <asp:GridView ID="gvmonprost" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvmonprost_RowDataBound"
                                ShowFooter="True" Width="273px" CssClass=" table-striped table-hover table-bordered grvContentarea gvTopHeader">
                                <PagerSettings Position="Top" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo14" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company Name">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlnkgvcomnamemps" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                                Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                                Width="100px"> 
                                            
                                          
                                            
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Cost">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcostmps" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "costamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Collection ">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcollamtmps" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "collamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Net Position">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvnetpositionmps" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <FooterStyle HorizontalAlign="Right" />
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
                    <fieldset>
                        <asp:Label ID="lblHrMgt" runat="server" CssClass="smLbl_to" Text="L. HR MANAGEMENT" Visible="False"></asp:Label>
                    </fieldset>

                    <div class="row">
                        <div class=" col-md-12 col-sm-12 col-lg-12">
                            <asp:GridView ID="gvHremp" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea gvTopHeader"
                                Width="37px" OnRowDataBound="gvHremp_RowDataBound">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlpsum1" runat="server" Font-Bold="True" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company Name">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkgvcomname" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                                Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname"))   %>'
                                                Width="140px"> 
                                            
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Employee">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtoemp" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curempno")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Salary">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvnetsalary" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curpay")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
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
