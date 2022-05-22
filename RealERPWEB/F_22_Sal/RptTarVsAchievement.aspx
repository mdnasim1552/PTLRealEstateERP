<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptTarVsAchievement.aspx.cs" Inherits="RealERPWEB.F_22_Sal.RptTarVsAchievement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 asitCol3 pading5px">

                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName"
                                            Text="Date:"></asp:Label>

                                        <asp:TextBox ID="txtDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtDate"></cc1:CalendarExtender>

                                    </div>
                                    <div class="col-md-4 asitCol4 pading5px">

                                        <asp:Label ID="lbltodate" runat="server" CssClass="smLbl_to" Text="To:"></asp:Label>

                                        <asp:TextBox ID="txttodate" runat="server" AutoPostBack="True"
                                            CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>

                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn"
                                            OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="SalesTarvsAch" runat="server">
                                <div class="table table-responsive">
                                    <asp:GridView ID="gvSalTvsAch" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvSalTvsAch_RowDataBound" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Capacity Avaialable">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvcapacitystvach" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "capacity")).ToString("#,##0;-#,##0; ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Target As Per Yearly Plan">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvmasbgdstvach" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "masbgd")).ToString("#,##0;-#,##0; ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Break-even">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvbepstvach" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bep")).ToString("#,##0;-#,##0; ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Monthly Target">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlnkgvtsaleamt" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                        Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                                        Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsaleamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="100px">
                                                    </asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Monthly Target As on Today">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtatosaleamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tosaleamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Monthly Achivement">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlnkgvDSAmt" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                        Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                                        Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acsaleam")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="100px">
                                                    </asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Achieved in %">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvperotsal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perotsale")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Graph">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlnkgvgraph" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                        Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))%>'
                                                        Width="100px">
                                                    </asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
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
                            </asp:View>
                            <asp:View ID="CollTarvsAch" runat="server">
                                <div class="table table-responsive">
                                    <asp:GridView ID="gvrcoll" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvrcoll_RowDataBound" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="serialnoidre" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                        Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Capacity Avaialable">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvcapacitystvachcoll" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "capacity")).ToString("#,##0;-#,##0; ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Target As Per Yearly Plan">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvmasbgdstvachcoll" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "masbgd")).ToString("#,##0;-#,##0; ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Break-even">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvbepstvachcoll" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bep")).ToString("#,##0;-#,##0; ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText=" Monthly Target">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlnkgvtcollamt" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                        Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                                        Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcollamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="100px">
                                                    </asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" Monthly Target As on Today">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtatocollamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tastcollamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Achievement">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlnkgvaccollAmt" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                        Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                                        Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "accollam")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="100px">
                                                    </asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Achieved in %">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvperotcoll" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perotcoll")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Graph">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlnkgvgraphcoll" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                        Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))%>'
                                                        Width="100px">
                                                    </asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
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
                            </asp:View>
                            <asp:View ID="ViewHR" runat="server">
                                <div class="table table-responsive">
                                    <asp:GridView ID="gvHremp" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvHremp_RowDataBound" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True">
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
                                                    <asp:Label ID="lgvtoemp" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toemp")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="75px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Salary">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvnetsalary" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netpay")).ToString("#,##0;(#,##0); ") %>'
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
                            </asp:View>
                            <asp:View ID="ViewMasMonVsExe" runat="server">
                                <div class="table table-responsive">
                                    <asp:GridView ID="gvMMPlanVsAch" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        OnRowDataBound="gvMMPlanVsAch_RowDataBound" ShowFooter="True">
                                        <PagerSettings Position="Top" />
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Company Name">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlnkgvcomnamecons" runat="server" BorderColor="#99CCFF"
                                                        BorderStyle="none" Font-Size="11px" Font-Underline="false"
                                                        Style="text-align: right; background-color: Transparent; color: Black;"
                                                        Target="_blank"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                                        Width="100px">
                                                    </asp:HyperLink>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Capacity Avaialable">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvcapacitystvachcon" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "capacity")).ToString("#,##0;-#,##0; ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Target As Per Yearly Plan">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvmasbgdstvachcon" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "masbgd")).ToString("#,##0;-#,##0; ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Break-even">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvbepstvachcon" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bep")).ToString("#,##0;-#,##0; ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Work Target As Per Master Plan (Upto Date)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvmasterplan" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "masplan")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="75px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFmasPlan" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right" Width="75px"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Monthly Work Target">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvmonplan" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "monplan")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="75px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFmonPlan" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="White" Style="text-align: right" Width="75px"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" Monthly Execution">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvExecutionpAC" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "excution")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="75px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFExecutionpAC" runat="server" Font-Bold="True"
                                                        Font-Size="12px" ForeColor="White" Style="text-align: right" Width="75px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Acheivement (%) on Mas. Plan">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvPerMasPlan" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peromasp")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="75px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Acheivement (%) on Monthly Plan">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvPerMonthPlan" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peromonp")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="75px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Floor Wise Progress">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlnkgvflrwiseprogress" runat="server" BorderColor="#99CCFF"
                                                        BorderStyle="none" Font-Size="11px" Font-Underline="false"
                                                        Style="background-color: Transparent; color: Black;" Target="_blank"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                        Width="80px"></asp:HyperLink>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="left" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="System Generated Target">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlnkgvsysgentarget" runat="server" BorderColor="#99CCFF"
                                                        BorderStyle="none" Font-Size="11px" Font-Underline="false"
                                                        Style="background-color: Transparent; color: Black;" Target="_blank"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                        Width="80px"></asp:HyperLink>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="left" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Inflation Effect">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlnkgvineffect" runat="server" BorderColor="#99CCFF"
                                                        BorderStyle="none" Font-Size="11px" Font-Underline="false"
                                                        Style="background-color: Transparent; color: Black;" Target="_blank"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                        Width="80px"></asp:HyperLink>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="left" />
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



                            </asp:View>
                            <asp:View ID="ViewConStatus" runat="server">
                                <div class="table table-responsive">
                                    <asp:GridView ID="gvConTvsAch" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        OnRowDataBound="gvConTvsAch_RowDataBound" ShowFooter="True">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDSlNo0" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Capacity Avaialable">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvcapacitystvachcs" runat="server" BorderColor="#99CCFF"
                                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                        Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "capacity")).ToString("#,##0;-#,##0; ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Target As Per Yearly Plan">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvmasbgdstvachcs" runat="server" BorderColor="#99CCFF"
                                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                        Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "masbgd")).ToString("#,##0;-#,##0; ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Break-even">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvbepstvachcs" runat="server" BorderColor="#99CCFF"
                                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                        Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bep")).ToString("#,##0;-#,##0; ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Monthly Target">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlnkgvmonplancs" runat="server" BorderColor="#99CCFF"
                                                        BorderStyle="none" Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                                        Target="_blank"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "monplan")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="100px">
                                                    </asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Monthly Achivement">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlnkgvmonachcs" runat="server" BorderColor="#99CCFF"
                                                        BorderStyle="none" Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                                        Target="_blank"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "excution")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="100px">
                                                    </asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Achieved in %">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvperomonplan" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peromonp")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Graph">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlnkgvgraphcs" runat="server" BorderColor="#99CCFF"
                                                        BorderStyle="none" Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                                        Target="_blank"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1"))%>'
                                                        Width="100px">
                                                    </asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
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
                            </asp:View>

                            <asp:View ID="ViewLandProcure" runat="server">
                                <div class="table table-responsive">
                                    <asp:GridView ID="gvLPTvsAch" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        OnRowDataBound="gvLPTvsAch_RowDataBound" ShowFooter="True">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDSlNo1" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Capacity Avaialable">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvcapacitystvachlp" runat="server" BorderColor="#99CCFF"
                                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                        Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "capacity")).ToString("#,##0;-#,##0; ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Target As Per Yearly Plan">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvmasbgdstvachlp" runat="server" BorderColor="#99CCFF"
                                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                        Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "masbgd")).ToString("#,##0;-#,##0; ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Break-even">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvbepstvachlp" runat="server" BorderColor="#99CCFF"
                                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                        Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bep")).ToString("#,##0;-#,##0; ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Monthly Target">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlnkgvmonplanlp" runat="server" BorderColor="#99CCFF"
                                                        BorderStyle="none" Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                                        Target="_blank"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "monplan")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="100px">
                                                    </asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Monthly Achivement">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlnkgvmonachlp" runat="server" BorderColor="#99CCFF"
                                                        BorderStyle="none" Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                                        Target="_blank"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "monach")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="100px">
                                                    </asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Achieved in %">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvperomonplanlp" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peromonp")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Graph">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlnkgvgraphlp" runat="server" BorderColor="#99CCFF"
                                                        BorderStyle="none" Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                                        Target="_blank"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))%>'
                                                        Width="100px">
                                                    </asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
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
                            </asp:View>
                            <asp:View ID="ViewPPlan" runat="server">
                                <div class="table table-responsive">
                                    <asp:GridView ID="gvProPlan" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        OnRowDataBound="gvProPlan_RowDataBound">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlpsum2" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvresdesc" runat="server" BorderColor="#99CCFF"
                                                        BorderStyle="none" Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc"))   %>'
                                                        Width="140px"> 
                                            
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Budgeted">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lnkgvbgdamt" runat="server" Font-Underline="false" Style="background-color: Transparent; color: Black; text-align: right"
                                                        Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="75px"></asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Actual">
                                                <ItemTemplate>


                                                    <asp:HyperLink ID="lnkgvacamt" runat="server" Font-Underline="false" Style="background-color: Transparent; color: Black; text-align: right"
                                                        Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="75px"></asp:HyperLink>


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
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

  
</asp:Content>

