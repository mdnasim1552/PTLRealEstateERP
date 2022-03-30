﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptSalaryReconciliation.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_89_Pay.RptSalaryReconciliation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            $('.chzn-select').chosen({ search_contains: true });
            $(".chosen-select").chosen({
                search_contains: true,
                no_results_text: "Sorry, no match!",
                allow_single_deselect: true
            });

            var gv1 = $('#<%=this.gvSalaryRecon.ClientID %>');
            gv1.Scrollable();
        };

    </script>

    <style>
    </style>




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

            <div class="card card-fluid">
                <div class="card-body" style="min-height:250px;">
                    <div class="row">

                        <div class="col-md-1">
                            <div class="form-group">
                                <label for="staticEmail" class="control-label  lblmargin-top9px">Company</label>

                            </div>

                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlCompany" runat="server"
                                    CssClass="chzn-select form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                </asp:DropDownList>

                            </div>

                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <label for="staticEmail" class="control-label  lblmargin-top9px">Branch</label>

                            </div>

                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlBranch" runat="server"
                                    CssClass="chzn-select form-control" AutoPostBack="true">
                                </asp:DropDownList>

                            </div>

                        </div>

                       


                        <div class="col-md-2">
                            <div class="form-group">
                                <div class="input-group input-group-alt">
                                    <div class="input-group-prepend ">
                                        <span class="input-group-text">Month</span>

                                    </div>
                                    <asp:DropDownList ID="ddlMonth" ClientIDMode="Static" runat="server"
                                        CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>

                        </div>




                        <div class="col-md-1">
                            <div class="form-group">

                                <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-primary ml-2" OnClick="lnkbtnOk_Click">Ok</asp:LinkButton>

                            </div>

                        </div>


                    </div>


                    <div class="row">
                        <asp:GridView ID="gvSalaryRecon" runat="server" PageSize="50"
                            AutoGenerateColumns="False" OnRowDataBound="gvSalaryRecon_RowDataBound" OnPageIndexChanging="gvSalaryRecon_PageIndexChanging"
                            CssClass="table-striped table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Description" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDescription" runat="server" Font-Size="12px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvEmpName" runat="server" Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                            Width="220px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvEmpDesig" runat="server" Font-Size="12px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Replacement">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRepment" runat="server" Font-Size="12px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "replacement")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Join/Resign">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvJRDate" runat="server" Font-Size="12px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "joresigndate")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Cur. Amt.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCurAmt" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curamt")).ToString("#,##0;(#,##0); ")%>'
                                            Font-Size="12px" Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Prev. Amt.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPrevAmt" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preamt")).ToString("#,##0;(#,##0); ") %>'
                                            Font-Size="12px" Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />

                                </asp:TemplateField>

                            </Columns>

                            <FooterStyle CssClass="grvFooterNew" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeaderNew" />
                            <RowStyle CssClass="grvRows" />

                            <%-- <FooterStyle BackColor="#04b7db" ForeColor="#ffffff" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />--%>
                        </asp:GridView>
                    </div>


                </div>


            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
