<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptOngoingProjects.aspx.cs" Inherits="RealERPWEB.F_38_AI.RptOngoingProjects" %>
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

            <div class="card mt-4">
                <div class="card-header">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="Label1" runat="server">From</asp:Label>
                            <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            <cc1:CalendarExtender runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label2" runat="server">To</asp:Label>
                            <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true"></asp:TextBox>
                            <cc1:CalendarExtender runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                        </div>
                        <div class="col-md-2 mt-4">
                            <div class="form-group-">
                                <asp:LinkButton ID="lnkbtnok" runat="server" OnClick="lnkbtnok_Click" CssClass=" btn btn-primary btn-sm mt20">Ok</asp:LinkButton></li>
                            </div>
                        </div>
                    </div>

                </div>

                <div class="card-body">
                    <div class="table-responsive mt-2">
                        <asp:GridView ID="gv_OngoingProject" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Visible="True" AllowPaging="true" PageSize="15">
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
                                <asp:TemplateField HeaderText="ProjectName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprjnamebatchwise" runat="server" Width="200px"
                                            Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "prjnamebatchwise"))%>'
                                            ForeColor="Black"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Client">
                                    <ItemTemplate>
                                        <asp:Label ID="lblclient" runat="server" Width="200px"
                                            Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "client"))%>'
                                            ForeColor="Black"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Annotor Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblannotortype" runat="server" Width="120px"
                                            Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "annotortype"))%>'
                                            ForeColor="Black"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblunit" runat="server" Width="120px"
                                            Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "unit"))%>'
                                            ForeColor="Black"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblrate" runat="server" Width="80px"
                                            Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>'
                                            ForeColor="Black"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblqtyofwork" runat="server" Width="80px"
                                            Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qtyofwork")).ToString("#,##0;(#,##0); ") %>'
                                            ForeColor="Black"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Expected USD">
                                    <ItemTemplate>
                                        <asp:Label ID="lblexpusd" runat="server" Width="80px"
                                            Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "expusd")).ToString("#,##0;(#,##0); ") %>'
                                            ForeColor="Black"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Valocity PerHour">
                                    <ItemTemplate>
                                        <asp:Label ID="lblannovalueper" runat="server" Width="80px"
                                            Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "annovalueper")).ToString("#,##0;(#,##0); ") %>'
                                            ForeColor="Black"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Possible  Day">
                                    <ItemTemplate>
                                        <asp:Label ID="lblannotperday" runat="server" Width="80px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "annotperday")).ToString("#,##0;(#,##0); ") %>'
                                            ForeColor="Black"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>             
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

