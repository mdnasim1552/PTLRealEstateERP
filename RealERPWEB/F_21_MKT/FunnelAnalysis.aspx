<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="FunnelAnalysis.aspx.cs" Inherits="RealERPWEB.F_21_MKT.FunnelAnalysis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
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

            <div class="card-fluid container-data  mt-2">
                <div class="row" id="Lvform" runat="server" visible="true">
                    <div class="col-12 col-lg-12">
                        <section class="card card-fluid" style="min-height: 650px">
                            <!-- .card-body -->
                            <header class="card-header">Funnel Analysis 
                                <asp:RadioButtonList runat="server" ID="rbtType" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbtType_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Month Wise</asp:ListItem>
                                    <asp:ListItem Value="1">Source Wise</asp:ListItem>
                                    <asp:ListItem Value="2">Movement Wise</asp:ListItem>
                                </asp:RadioButtonList>
                               
                            </header>
                            <div class="card-body">
                                 <div class="table table-responsive">
                                     <asp:GridView ID="gvFunAnaMonths" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                            CssClass="table-striped table-hover table-bordered">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl #">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Month">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvDescription0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "monthid")) %>'
                                                            Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Query">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvFunAnaQuery" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "query")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Query to Lost">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvFunAnaQueryLost" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qtolost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Query to Lead">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvFunAnaQueryLead" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qtolead")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="middle" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Lead To Lost">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvFunAnaltoLost" runat="server"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ltoLost")).ToString("#,##0.00;(#,##0.00);") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Lead To Qu. Lead">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvFunlAnaltoqualiflead" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ltoqualiflead")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Q. Lead to lost">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvFunlAnaltoqualiflead" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qltolost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Q. Lead to Nego">
                                                    <ItemTemplate>
                                                         <asp:Label ID="gvFunlAnaltoqualiflead" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qltonego")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nego to Lost">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvFunlAnanegolost" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "negolost")).ToString("#,##0.00;(#,##0.00);") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nego to F. Nego">
                                                    <ItemTemplate>
                                                       <asp:Label ID="gvFunlAnannegolost" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "negtofnlnego")).ToString("#,##0.00;(#,##0.00);") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Final Nego to Lead">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvFunlAnannfngtolost" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fngtolost")).ToString("#,##0.00;(#,##0.00);") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Final Nego to Win">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvFunlAnannfngtoWin" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fngtowin")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                            </Columns>

                                        </asp:GridView>
                                 </div>
                            </div>
                        </section>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
