<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptSalaryReconciliation.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_89_Pay.RptSalaryReconciliation" %>

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

            <div class="card card-fluid container-data">
                <div class="card-header mt-3 mb-0 pb-0">
                    <div class="form-group row mb-0">
                        <label for="staticEmail" class="col-1 col-form-label">Company</label>
                        <div class="col-3">
                            <asp:DropDownList ID="ddlCompany" runat="server"
                                CssClass="chzn-select form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <label for="staticEmail" class="col-1 col-form-label">Branch</label>
                        <div class="col-3">
                            <asp:DropDownList ID="ddlBranch" runat="server"
                                CssClass="chzn-select form-control" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <label for="staticEmail" class="col-1 col-form-label">Month</label>
                        <div class="col-2">
                            <asp:DropDownList ID="ddlMonth" ClientIDMode="Static" runat="server"
                                CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="col-1">
                            <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-primary ml-2" OnClick="lnkbtnOk_Click">Ok</asp:LinkButton>

                        </div>
                    </div>
                    <div class="form-group row mb-1">
                        <label for="staticEmail" class="col-1 col-form-label">Page Size</label>

                        <div class="col-2">
                            <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="True"
                                CssClass=" form-control" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>100</asp:ListItem>
                                <asp:ListItem>150</asp:ListItem>
                                <asp:ListItem>200</asp:ListItem>
                                <asp:ListItem>300</asp:ListItem>
                                <asp:ListItem>600</asp:ListItem>
                                <asp:ListItem>1000</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                </div>
                <div class="card-body" style="min-height: 450px;">
                    <div class="row">
                        <div class="table-responsive" runat="server">
                            <asp:GridView ID="gvSalaryRecon" runat="server" PageSize="50"
                                AutoGenerateColumns="False" OnRowDataBound="gvSalaryRecon_RowDataBound" OnPageIndexChanging="gvSalaryRecon_PageIndexChanging"
                                CssClass="table-striped table-hover table-bordered grvContentarea">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center"/>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Description" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDescription" runat="server" Font-Size="12px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center"/>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Employee Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvEmpName" runat="server" Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                Width="220px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center"/>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvEmpDesig" runat="server" Font-Size="12px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center"/>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Replacement">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRepment" runat="server" Font-Size="12px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "replacement")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center"/>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Join/Resign">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvJRDate" runat="server" Font-Size="12px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "joresigndate")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center"/>
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

                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
