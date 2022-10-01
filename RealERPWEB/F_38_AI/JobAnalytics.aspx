<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="JobAnalytics.aspx.cs" Inherits="RealERPWEB.F_38_AI.JobAnalytics" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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

            <div class="card mt-5">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12" style="height: 300px; width: 100%">
                        <h2>Demo Projects</h2>
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="col-md-2">
                        <div class="shadow-lg p-3 mb-5 bg-body rounded">
                            <h2 class="text-center">25K</h2>
                            <div class="text-center"><br />
                                <p> Total Number of Complate class insatance</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="shadow-lg p-3 mb-5 bg-body rounded">
                            <h2 class="text-center text-primary">0</h2>
                            <div class="text-center"><br />
                                <p> Number of Complate attribute insatance</p>

                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="shadow-lg p-3 mb-5 bg-body rounded">
                            <h2 class="text-center">0.1<small>hrs</small></h2>
                            <div class="text-center">
                                <p>Out of 770.4hrs  <br /> <b class="text-primary">QA hours spent</b> </p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="shadow-lg p-3 mb-5 bg-body rounded">
                            <h2 class="text-center text-primary">410.8<small>hrs</small></h2>
                            <div class="text-center">
                                <p>Out of 770.4hrs <br /> <b class="text-primary">Annot, hours spent</b> </p>
                            </div>
                        </div>
                    </div>
                      <div class="col-md-2">
                        <div class="shadow-lg p-3 mb-5 bg-body rounded">
                            <h2 class="text-center text-primary">359.4<small>hrs</small></h2>
                            <div class="text-center">
                                <p>Out of 770.4hrs  <br />  <b class="text-primary">Admin hours spent</b> </p>
                            </div>
                        </div>
                    </div>
                      <div class="col-md-2">
                        <div class="shadow-lg p-3 mb-5 bg-body rounded">
                            <h2 class="text-center text-primary">0.00</h2>
                            <div class="text-center">
                                <p>Out of 770.4hrs  <br />  <b class="text-primary">Total Number of Skip</b> </p>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="row">
                    <h3>Users</h3>
                </div>
                <div class="row">
                    <asp:GridView ID="gv_UserAnalytic" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="True" Visible="True" AllowPaging="true" PageSize="15" Width="100%">
                        <Columns>

                            <asp:TemplateField HeaderText="SL # ">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right; font-size: 12px;"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                        ForeColor="Black"></asp:Label>

                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User">
                                <ItemTemplate>
                                    <asp:Label ID="lbluser" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "")) %>'
                                        Width="500px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Role">
                                <ItemTemplate>
                                    <asp:Label ID="lblbatchid" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "")) %>'
                                        Width="250px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Work Hours">
                                <ItemTemplate>
                                    <asp:Label ID="lblworkour" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "")) %>'
                                        Width="250px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                          
                        </Columns>
                        <PagerStyle CssClass="gvPagination" />

                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
